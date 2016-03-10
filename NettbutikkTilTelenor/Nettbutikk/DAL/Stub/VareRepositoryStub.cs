using Nettbutikk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using Nettbutikk.Model.DomeneModel;
using Nettbutikk.Model.PresentasjonsModel;
using System.IO;
namespace Nettbutikk.DAL
{
    public class VareRepositoryStub : DAL.IVareRepository
    {
        public bool OpprettVare(vare innVare, int underkategori, int kategori)
        {
            if (innVare.betegnelse == "")
                return false;
            return true;
        }
        public bool slettVare(int id)
        {
            if (id == 0)
                return false;
            return true;
        }

        public bool oppdaterVare(endreVare innVare)
        {
            if (innVare.idnr == 0)
                return false;
            
            return true;
        }
        public List<visVarerAdmin> listAlleVarer()
        {
            var vareListe = new List<visVarerAdmin>();
            var varen = new visVarerAdmin()
            {
                antall = 2,
                betegnelse = "flott produkt",
                detaljer = "flott produktflott produktflott produkt",
                fremvisningsdetaljer = "flott produkt",
                idnr = 1,
                katnavn = "PC",
                pris = 1000,
                slettet = false,
                underkatnavn = "Skjermer",
                visvarebilde = "../Images/bilde.jpg"
            };
            vareListe.Add(varen);
            vareListe.Add(varen);
            vareListe.Add(varen);
            return vareListe;
        }
        public List<fremvisningsvarer> hentAlleVarer()
        {
            var vareListe = new List<fremvisningsvarer>();
            var varen = new fremvisningsvarer()
            {
                antall = 2,
                betegnelse = "flott produkt",           
                fremvisningsdetaljer = "flott produkt",
                idnr = 1,             
                pris = 1000,         
                visvarebilde = "../Images/bilde.jpg"
            };
            vareListe.Add(varen);
            vareListe.Add(varen);
            vareListe.Add(varen);
            return vareListe;
        }
        public string hentAlleVarer(string sokord)
        {
            if (sokord == "")
                return "";
            return "OK";
        }

        public List<fremvisningsvarer> hentSokVarer(string sokord)
        {
            return null;
        }

        public List<kategori> hentAlleKategorier()
        {
            return null;
        }

        public endreVare finnVareEndre(int id)
        {
            if (id == 0)
            {
                var varen = new endreVare();
                varen.idnr = 0;
                return varen;
            }
            else
            {
                var bildeListe = new List<visBilde>();
                var varen = new endreVare()
                {
                    antall = 2,
                    betegnelse = "flott produkt",
                    detaljer = "flott produktflott produktflott produkt",
                    fremvisningsdetaljer = "flott produkt",
                    idnr = 1,
                    katnavn = "PC",
                    pris = 1000,
                    underkatnavn = "Skjermer",
                    katid = 1,
                    underkatid = 1,
                };
                var bilde = new visBilde()
                {
                    id = 1,
                    src = "test"
                };
                bildeListe.Add(bilde);
                bildeListe.Add(bilde);
                varen.bilde = bildeListe;
                return varen;
            }

        }

        public string slettBildeTilVare(int vareID, int bildeID)
        {
            if (vareID == 0)
                return "";
            
            return "OK";
        }

        public displayVarer seVare(int vareID)
        {
            return null;
        }

        public List<fremvisningsvarer> hentVareIKategori(int katnummer)
        {
            return null;
        }

        public List<fremvisningsvarer> hentVareIUnderKategori(int Underkatnummer)
        {
            return null;
        }

        public bool OpprettBildeTilVare(string[] bilder, int vareNr)
        {
            if (vareNr == 1)
                return false;
            
            return true;
        }

        public List<bilderimappe> ImagesInnhold()
        {
            var bildeListe = new List<bilderimappe>();
            var filn = new List<string>();
            filn.Add("fil");
            filn.Add("fil");
            var bilde = new bilderimappe()
            {
                filer = filn,
                Mappe = "test"
            };
            bildeListe.Add(bilde);
            bildeListe.Add(bilde);
            bildeListe.Add(bilde);
            return bildeListe;
        }

        public bool lastOppBilder(HttpPostedFileBase[] file, string mappe)
        {
            if (mappe == "")
                return false;
            
            return true;
        }

        public bool SlettBilder(string filnavn, string mappenavn)
        {
            if (filnavn == "")
                return false;
            
            return true;
        }

        public void oppdaterKat()
        {
            
        }
    }
}