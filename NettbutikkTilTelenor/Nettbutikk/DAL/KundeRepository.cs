using Nettbutikk.Model;
using Nettbutikk.Model.DomeneModel;
using Nettbutikk.Model.PresentasjonsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

//Denne klassen skal holde variable ne som er knyttet til databasen for kunder
namespace Nettbutikk.DAL
{
    public class KundeRepository : DAL.IKundeRepository
    {
        public List<minProfil> hentAlle()
        {
            try
            {
                var db = new NettbutikkDB();
                List<minProfil> alleKunder = db.Kunder.Select(k => new minProfil()
                {
                    fornavn = k.Fornavn,
                    etternavn =k.Etternavn,
                    adresse = k.Adresse,
                    telefonnummer= k.TelefonNummer,
                    postnr = k.Postnr,
                    poststeder = k.Poststed.PersPoststed,
                    epostadresse = k.EPostAdresse
           
                }
                                    ).ToList();
                return alleKunder;
            }catch(Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return null;
            }

        }
        public bool settInnKunde(kunde innKunde)
        {
            try
            {
            var db = new NettbutikkDB();
            var enKunde = db.Kunder.Find(innKunde.epostadresse);
            if (enKunde != null)
                return false;

            var nyKunde = new Kunde()
            {
                Fornavn = innKunde.fornavn,
                Etternavn = innKunde.etternavn,
                Adresse = innKunde.adresse,
                TelefonNummer = innKunde.telefonnummer,
                EPostAdresse = innKunde.epostadresse,
                Postnr = innKunde.postnr,
                Passord = hashPassord(innKunde.passord)
            };

            
                var eksisterendePoststed = db.Poststeder.Find(innKunde.postnr);

                if(eksisterendePoststed == null)
                {
                    var nyPoststed = new Poststed()
                    {
                        PersPoststed = innKunde.poststeder,
                        Postnr = innKunde.postnr
                    };
                    nyKunde.Poststed = nyPoststed;
                    
                }
                
                db.Kunder.Add(nyKunde);
                db.SaveChanges();
                SkrivTilFil.skrivEndringerTilFil( DateTime.Now +" /En kunde med id: "+nyKunde.EPostAdresse +" har blitt Opprettet");
                return true;

            }catch(Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return false;
            }
        }
        public bool endrePassord(string email, endrePassord pass)
        {
            try
            {
                var db = new NettbutikkDB();
                Kunde endreKunde = db.Kunder.Find(email);
                if (hashPassord(pass.gammelPassord) == endreKunde.Passord)
                {
                    endreKunde.Passord = hashPassord(pass.nyttPassord);
                }
                else
                    return false;

                
                db.SaveChanges();
                SkrivTilFil.skrivEndringerTilFil( DateTime.Now +" /En kunde med id: "+ email +" har endret passord");
                return true;
            }catch(Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return false;
            }
        }

        public kunde finnKunde (string email)
        {
            try
            {
            var db = new NettbutikkDB();
            var enKunde = db.Kunder.Find(email);

            if(enKunde == null)
            {
                return null;
            }
            else
            {
                var utKunde = new kunde()
                {
                    fornavn = enKunde.Fornavn,
                    etternavn = enKunde.Etternavn,
                    adresse = enKunde.Adresse,
                    postnr = enKunde.Postnr,
                    poststeder = enKunde.Poststed.PersPoststed,
                    telefonnummer = enKunde.TelefonNummer,
                    epostadresse = enKunde.EPostAdresse,
                    passord = enKunde.Passord
                };
                return utKunde;
            } 
                } catch(Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return null;
            }
        }
        public endreKunde finnKundeEndre(string email)
        {
            try
            {
                var db = new NettbutikkDB();
                var enKunde = db.Kunder.Find(email);

                if (enKunde == null)
                {
                    return null;
                }
                else
                {
                    var utKunde = new endreKunde()
                    {
                        fornavn = enKunde.Fornavn,
                        etternavn = enKunde.Etternavn,
                        adresse = enKunde.Adresse,
                        postnr = enKunde.Postnr,
                        poststeder = enKunde.Poststed.PersPoststed,
                        telefonnummer = enKunde.TelefonNummer,
                        passord = enKunde.Passord
                    };
                    return utKunde;
                }
            }
            catch (Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return null;
            }
        }
        public minProfil finnKundeProfil(string email)
        {
            try
            {

           
            var db = new NettbutikkDB();
            var enKunde = db.Kunder.Find(email);

            if (enKunde == null)
            {
                return null;
            }
            else
            {
                var utKunde = new minProfil()
                {
                    fornavn = enKunde.Fornavn,
                    etternavn = enKunde.Etternavn,
                    adresse = enKunde.Adresse,
                    postnr = enKunde.Postnr,
                    poststeder = enKunde.Poststed.PersPoststed,
                    telefonnummer = enKunde.TelefonNummer,
                    epostadresse = enKunde.EPostAdresse
                };
                return utKunde;
            }
            }catch(Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return null;
            }
        }
        
        public bool slettKunde(string email)
        {
            try
            { 

                var db = new NettbutikkDB();
                Kunde slettKunde = db.Kunder.Find(email);
                int ordreLengde = slettKunde.Ordre.Count();
                int kvitteringsLengde = slettKunde.Kvittering.Count();

                //system.invaligOperatingException ved bruk av foreach
                for (int j = 0; j < ordreLengde; j++)
                {
                    Ordre t = db.Ordrer.FirstOrDefault(k => k.Kunde.EPostAdresse == slettKunde.EPostAdresse);
                    db.Ordrer.Remove(t);
                    db.SaveChanges();             
                }
                for (int l = 0; l < kvitteringsLengde; l++)
                {
                    Kvittering kvit = db.Kvitteringer.FirstOrDefault(p => p.Kunde.EPostAdresse == slettKunde.EPostAdresse);
                    db.Kvitteringer.Remove(kvit);
                    db.SaveChanges();
                }

                
               
               
                db.Kunder.Remove(slettKunde);
                db.SaveChanges();
                SkrivTilFil.skrivEndringerTilFil(DateTime.Now + " /En kunde med id: " +email + " har blitt slettet");
                return true;
            }catch(Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return false;
            }
        }
        public bool endreKunde (string email, endreKunde innKunde)
        {
            var db = new NettbutikkDB();
            try
            {
                Kunde endreKunde = db.Kunder.Find(email);
                endreKunde.Fornavn = innKunde.fornavn;
                endreKunde.Etternavn = innKunde.etternavn;
                endreKunde.Adresse = innKunde.adresse;
                endreKunde.TelefonNummer = innKunde.telefonnummer;
                if(endreKunde.Postnr != innKunde.postnr)
                {
                    Poststed eksisterPoststed = db.Poststeder.Find(innKunde.postnr);
                    if(eksisterPoststed == null)
                    {
                        var nyPoststed = new Poststed()
                        {
                            Postnr = innKunde.postnr,
                            PersPoststed = innKunde.poststeder
                        };
                        db.Poststeder.Add(nyPoststed);
                        endreKunde.Postnr = innKunde.postnr;
                    }else
                    {
                        endreKunde.Postnr = innKunde.postnr;
                    }
                };

                
                db.SaveChanges();

                string melding = " /Kunde med id " + email + " har endret: ";
                if (endreKunde.Fornavn != innKunde.fornavn)
                    melding += "fornavn, ";
                if (endreKunde.Etternavn != innKunde.etternavn)
                    melding += "etternavn, ";
                if (endreKunde.Adresse != innKunde.adresse)
                    melding += "adresse, ";
                if (endreKunde.TelefonNummer != innKunde.telefonnummer)
                    melding += "telefonnummer, ";
                if (endreKunde.Postnr != innKunde.postnr)
                    melding += "postnr, ";
                if (endreKunde.Poststed.PersPoststed != innKunde.poststeder)
                    melding += "poststed, ";
                SkrivTilFil.skrivEndringerTilFil(DateTime.Now + melding);
                return true;
            }catch(Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return false;
            }
           
        }
        
        public string hashPassord(string funktohash)
        {
            using (System.Security.Cryptography.SHA512 sha512 =
            System.Security.Cryptography.SHA512.Create())
            {
                byte[] retVal = sha512.ComputeHash(Encoding.Unicode.GetBytes(funktohash));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
        public ansatt erAdmin (string navn, string passord)
        {
            try
            {
                var db = new NettbutikkDB();
                Ansatt admin = db.Ansatte.FirstOrDefault(n => n.Brukernavn == navn);
                if(admin == null)
                {
                    return null;
                }
                if(hashPassord(passord).Equals(admin.Passord) && admin.Brukernavn.Equals(navn))
                {
                    var utAdmin = new ansatt()
                    {
                        brukernavn=admin.Brukernavn,
                        eradmin = admin.ErAdmin,
                        fornavn = admin.Fornavn,
                        etternavn=admin.Etternavn,
                        passord = admin.Passord
                    };
                    return utAdmin;
                }
                return null;
            }catch(Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return null;
            }
        }
       

        public kunde loggInnOK(string email, string passord)
        {
            try
            {
                var db = new NettbutikkDB();
                Kunde kunde = db.Kunder.Find(email);

                if(kunde == null)
                {
                    return null;
                }

                if(hashPassord(passord).Equals(kunde.Passord) && kunde.EPostAdresse.Equals(email)) 
                {
                    var utkunde = new kunde()
                    {
                        fornavn= kunde.Fornavn,
                        etternavn = kunde.Etternavn,
                        epostadresse = kunde.EPostAdresse,
                        adresse = kunde.Adresse,
                        telefonnummer = kunde.TelefonNummer,
                        poststeder = kunde.Poststed.PersPoststed,
                        postnr = kunde.Postnr
                    };
                    return utkunde;
                }
                return null;
            }catch(Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return null;
            }
        }
        
    }
}