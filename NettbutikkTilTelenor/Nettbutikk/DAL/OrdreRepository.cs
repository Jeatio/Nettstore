using Nettbutikk.Model.PresentasjonsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nettbutikk.DAL
{
    public class OrdreRepository : DAL.IOrdreRepository
    {
        //--------------------------Ordre-------------------
        public bool leggIHandlevogn(int vareid, string kundeEmail)
        {
            try
            {
                var db = new NettbutikkDB();
                Vare eksistVare = db.Varer.Find(vareid);
                Kunde eksistKunde = db.Kunder.Find(kundeEmail);
                if (eksistVare == null || eksistKunde == null)
                {
                    return false;
                }
                foreach (var vareordre in eksistKunde.Ordre)
                {
                    if (vareordre.Vare.VNr == vareid && vareordre.ErBetalt == false)
                    {
                        vareordre.Antall += 1;
                        vareordre.OrdreDato = DateTime.Now;
                        db.SaveChanges();
                        return true;
                    }
                }
                var nyOrdre = new Ordre()
                {
                    OrdreDato = DateTime.Now,
                    ErBetalt = false,
                    ErLevert = false,
                    Kunde = eksistKunde,
                    Vare = eksistVare,
                    PrisPrEnhet = eksistVare.Pris,
                    detaljer = "Ikke betalt",
                    Antall = 1
                };

                db.Ordrer.Add(nyOrdre);
                eksistKunde.Ordre.Add(nyOrdre);
                db.SaveChanges();
                return true;
            }
            catch (Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return false;
            }

        }

        public List<visordre> visHandlevogn(string kundeEmail)
        {
            try
            {
                var db = new NettbutikkDB();
                Kunde eksistKunde = db.Kunder.Find(kundeEmail);
                List<visordre> liste = new List<visordre>();
                if (eksistKunde == null)
                {
                    return null;
                }
                foreach (var kundeordre in eksistKunde.Ordre)
                {
                    if (kundeordre.ErBetalt == false)
                    {
                        visordre handlekurv = new visordre()
                        {
                            varenr = kundeordre.Vare.VNr,
                            ordrenr = kundeordre.OrdreNr,
                            antall = kundeordre.Antall,
                            dato = kundeordre.OrdreDato,
                            detaljer = kundeordre.detaljer,
                            prisPrEnhet = kundeordre.PrisPrEnhet,
                            betegnelse = kundeordre.Vare.Betegnelse

                        };
                        if (kundeordre.Vare.Bilde != null)
                            handlekurv.bestiltvarebilde = kundeordre.Vare.Bilde.First().src;

                        liste.Add(handlekurv);
                    }
                }

                return liste;

            }
            catch (Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return null;
            }
        }

        public bool slettOrdre(int ordrenr, string kundeEpost)
        {
            try
            {
                var db = new NettbutikkDB();
                Kunde eksistKunde = db.Kunder.Find(kundeEpost);
                Ordre eksistOrdre = db.Ordrer.Find(ordrenr);
                if (eksistOrdre == null || eksistKunde == null)
                    return false;
                if (eksistOrdre.Kunde.EPostAdresse != eksistKunde.EPostAdresse)
                    return false;

                db.Ordrer.Remove(eksistOrdre);
                db.SaveChanges();
                return true;
            }
            catch (Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return false;
            }
        }

        public bool endreAntallVarer(int ant, int ordrenummer, string kundeEpost)
        {
            try
            {
                var db = new NettbutikkDB();
                Kunde eksistKunde = db.Kunder.Find(kundeEpost);
                Ordre eksistOrdre = db.Ordrer.Find(ordrenummer);
                if (eksistOrdre == null || eksistKunde == null)
                    return false;

                if (eksistOrdre.Kunde.EPostAdresse != eksistKunde.EPostAdresse)
                    return false;

                eksistOrdre.Antall += ant;

                if (eksistOrdre.Antall < 1)
                    eksistOrdre.Antall = 1;

                db.SaveChanges();
                return true;
            }
            catch (Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return false;
            }
        }

        public int RegnPris(string kundeEpost)
        {
            try
            {
                int totpris = 0;
                var db = new NettbutikkDB();
                Kunde eksistKunde = db.Kunder.Find(kundeEpost);
                if (eksistKunde == null)
                    return 0;
                foreach (var kundeordre in eksistKunde.Ordre)
                {
                    if (kundeordre.ErBetalt == false)
                    {
                        totpris += (kundeordre.Antall * kundeordre.PrisPrEnhet);
                    }
                }
                return totpris;
            }
            catch (Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return 0;
            }

        }

        public bool KjøpVarer(string kundeEpost)
        {
            try
            {
                int betalt = 0;
                var db = new NettbutikkDB();
                Kunde eksistKunde = db.Kunder.Find(kundeEpost);
                List<Ordre> liste = new List<Ordre>();
                if (eksistKunde == null)
                {
                    return false;
                }
                var nyKvittering = new Kvittering()
                {
                    Dato = DateTime.Now
                };
                foreach (var kundeordre in eksistKunde.Ordre)
                {
                    if (kundeordre.ErBetalt == false)
                    {
                        var vare = db.Varer.Find(kundeordre.Vare.VNr);
                        if (vare.Antall == 0)
                        {
                            kundeordre.detaljer = "Tomt for varen på lager, 2-3 ukers leveringstid";
                        }
                        else if ((vare.Antall - kundeordre.Antall) < 0)
                        {
                            kundeordre.detaljer = "Tomt for varen på lager, 2-3 ukers leveringstid";
                            vare.Antall = 0;
                        }
                        else
                        {
                            vare.Antall -= kundeordre.Antall;
                            kundeordre.detaljer = "Klargjort for utkjøring";
                        }
                        betalt += (kundeordre.Antall * kundeordre.PrisPrEnhet);
                        kundeordre.ErBetalt = true;
                        kundeordre.Kvittering = nyKvittering;
                        liste.Add(kundeordre);
                    }

                }

                if (liste.Count < 1)
                    return false;

                nyKvittering.Ordre = liste;
                nyKvittering.Pris = betalt;
                nyKvittering.Kunde = eksistKunde;
                db.Kvitteringer.Add(nyKvittering);
                eksistKunde.Kvittering.Add(nyKvittering);
                db.SaveChanges();

                return true;
            }
            catch (Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return false;
            }
        }
        public List<visOrdreAdmin> visAlleKvitteringer()
        {
            try
            {
                var db = new NettbutikkDB();
                List<visOrdreAdmin> alleOrdre = db.Kvitteringer.Select(k => new visOrdreAdmin()
                {
                    epostadresse = k.Kunde.EPostAdresse ,
                    fornavn = k.Kunde.Fornavn,
                    etternavn = k.Kunde.Etternavn,
                    dato = k.Dato,
                    pris = k.Pris,
                    id = k.Id
                }).ToList();
                return alleOrdre;

            }catch(Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return null;
            }
        }

        public List<visallekvitteringer> visAlleKvitteringer(string kundeEpost)
        {
            try
            {
                var db = new NettbutikkDB();
                Kunde eksistKunde = db.Kunder.Find(kundeEpost);
                List<visallekvitteringer> liste = new List<visallekvitteringer>();
                if (eksistKunde == null)
                {
                    return null;
                }
                foreach (var kundekvitteringer in eksistKunde.Kvittering)
                {
                    visallekvitteringer kvitteringer = new visallekvitteringer()
                    {
                        kvitteringsid = kundekvitteringer.Id,
                        dato = kundekvitteringer.Dato,
                        pris = kundekvitteringer.Pris
                    };

                    liste.Add(kvitteringer);
                }

                return liste;

            }
            catch (Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return null;
            }

        }

        public List<viskvittering> visKvittering(string kundeEmail, int kvitteringsID)
        {
            try
            {
                var db = new NettbutikkDB();
                Kunde eksistKunde = db.Kunder.Find(kundeEmail);
                Kvittering kviter = db.Kvitteringer.Find(kvitteringsID);
                List<viskvittering> liste = new List<viskvittering>();
                if (eksistKunde == null)
                {
                    return null;
                }
                Kvittering kvitt = eksistKunde.Kvittering.First(s => s.Id == kvitteringsID);
                foreach (var kviteringsordre in kvitt.Ordre)
                {

                    viskvittering betaltordre = new viskvittering()
                    {
                        antall = kviteringsordre.Antall,
                        dato = kviteringsordre.OrdreDato,
                        prisPrEnhet = kviteringsordre.PrisPrEnhet,
                        betegnelse = kviteringsordre.Vare.Betegnelse,
                        detaljer = kviteringsordre.detaljer,
                        totpris = kvitt.Pris
                    };

                    liste.Add(betaltordre);
                }

                return liste;

            }
            catch (Exception feil)
            {
                SkrivTilFil.skrivFeilTilFil(feil + "");
                return null;
            }
        }
    }
}