using Nettbutikk.Model.PresentasjonsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nettbutikk.DAL
{
    public class OrdreRepositoryStub : DAL.IOrdreRepository
    {
        //--------------------------Ordre-------------------
        public bool leggIHandlevogn(int vareid, string kundeEmail)
        {
            if (kundeEmail == "")
                return false;
            return true;

        }

        public List<visordre> visHandlevogn(string kundeEmail)
        {
            return null;
        }

        public bool slettOrdre(int ordrenr, string kundeEpost)
        {
            if (kundeEpost == "")
                return false;
            return true;
        }

        public bool endreAntallVarer(int ant, int ordrenummer, string kundeEpost)
        {
            if (kundeEpost == "")
                return false;
            return true;
        }

        public int RegnPris(string kundeEpost)
        {
            if (kundeEpost == "")
                return -1;
            return 1;

        }

        public bool KjøpVarer(string kundeEpost)
        {
            if (kundeEpost == "")
                return false;
            return true;
        }
        public List<visOrdreAdmin> visAlleKvitteringer()
        {
            var ordreListe = new List<visOrdreAdmin>();
            var ordre = new visOrdreAdmin()
            {
                id = 2,
                pris = 1233,
                fornavn  = "Daniel",
                etternavn = "Rein",
                epostadresse = "Dan@hot.com",
                dato = new DateTime()
            };
             ordreListe.Add(ordre);
             ordreListe.Add(ordre);
             ordreListe.Add(ordre);
             return ordreListe;
        }

        public List<visallekvitteringer> visAlleKvitteringer(string kundeEpost)
        {
            return null;

        }

        public List<viskvittering> visKvittering(string kundeEmail, int kvitteringsID)
        {
            var kvitListe = new List<viskvittering>();
            var kvitt = new viskvittering()
            {
                antall = 2,
                betegnelse = "flott produkt",
                detaljer = "Daniel",
                prisPrEnhet = 1000,
                totpris = 3000,
                dato = new DateTime()
            };
            kvitListe.Add(kvitt);
            kvitListe.Add(kvitt);
            kvitListe.Add(kvitt);
            return kvitListe;
        }
    }
}