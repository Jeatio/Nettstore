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
    public class KundeRepositoryStub : DAL.IKundeRepository
    {
        public List<minProfil> hentAlle()
        {
            var kundeListe = new List<minProfil>();
            var k = new minProfil()
            {
                fornavn = "Per",
                etternavn = "Olsen",
                epostadresse = "per@ya.no",
                adresse= "Osloveien 82",
                postnr = "1087",
                poststeder = "Oslo",
                telefonnummer = "12345678"          
            };
            kundeListe.Add(k);
            kundeListe.Add(k);
            kundeListe.Add(k);
            return kundeListe;
        }
        public bool settInnKunde(kunde innKunde)
        {
            if (innKunde.fornavn == "")
                return false;
            return true;
        }
        public bool endrePassord(string email, endrePassord pass)
        {
            if (email == "")
                return false;
            return true;
        }

        public kunde finnKunde(string email)
        {
            return null;
        }
        public endreKunde finnKundeEndre(string email)
        {
            if (email == "")
            {
                var kunde = new endreKunde();
                kunde.fornavn = "";
                return kunde;
            }
            else
            {
                var kunde = new endreKunde()
                {

                    fornavn = "Per",
                    etternavn = "Olsen",
                    adresse = "Osloveien 82",
                    postnr = "1234",
                    poststeder = "Oslo",
                    telefonnummer = "12345678",
                    passord = "123"
                };
                return kunde;
            }
        }
        public minProfil finnKundeProfil(string email)
        {
            if (email == "")
            {
                var kunde = new minProfil();
                kunde.epostadresse = "";
                return kunde;
            }
            else
            {
                var kunde = new minProfil()
                {

                    fornavn = "Per",
                    etternavn = "Olsen",
                    adresse = "Osloveien 82",
                    postnr = "1234",
                    poststeder = "Oslo",
                    telefonnummer = "12345678",
                    epostadresse = "riktig"
                };
                return kunde;
            }
        }
        public string hashPassord(string funktohash)
        {
            if (funktohash == "")
                return "";
            return "OK";
        }

        public bool slettKunde(string email)
        {
            if (email == "")
                return false;
            return true;
        }
        public bool endreKunde(string email, endreKunde innKunde)
        {
            if (email == "")
                return false;
            return true;

        }


        public ansatt erAdmin(string navn, string passord)
        {
            var ansatt = new ansatt();
            if (navn == "")
                return ansatt;
            ansatt.fornavn = "Per";
            return ansatt;
        }


        public kunde loggInnOK(string email, string passord)
        {
            return null;
        }

    }
}