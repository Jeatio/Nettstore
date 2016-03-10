using Nettbutikk.Model;
using Nettbutikk.Model.DomeneModel;
using Nettbutikk.Model.PresentasjonsModel;
using Nettbutikk.DAL;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nettbutikk.BLL
{
   public class VareLogikk : BLL.IVareLogikk
    {
       private IVareRepository _repository;
       public VareLogikk ()
       {
           _repository = new VareRepository();
       }
       public VareLogikk (IVareRepository stub)
       {
           _repository = stub;
       }
       public bool slettVare(int id)
       {
           
           return _repository.slettVare(id);
       }
        public bool OpprettVare(vare innVare, int underkategori, int kategori)
        {

            return _repository.OpprettVare(innVare, underkategori, kategori);
        }
        public bool oppdaterVare(endreVare innVare)
        {

            return _repository.oppdaterVare(innVare);
        }
        public List<fremvisningsvarer> hentAlleVarer()
        {

            return _repository.hentAlleVarer();
        }
        public List<visVarerAdmin> listAlleVarer()
        {

            return _repository.listAlleVarer();
        }
        public string hentAlleVarer(string sokord)
        {

            return _repository.hentAlleVarer(sokord);
        }

        public List<fremvisningsvarer> hentSokVarer(string sokord)
        {

            return _repository.hentSokVarer(sokord);
        }

        public List<kategori> hentAlleKategorier()
        {

            return _repository.hentAlleKategorier();
        }

        public displayVarer seVare(int vareID)
        {

            return _repository.seVare(vareID);
        }

        public List<fremvisningsvarer> hentVareIKategori(int katnummer)
        {

            return _repository.hentVareIKategori(katnummer);
        }

        public List<fremvisningsvarer> hentVareIUnderKategori(int Underkatnummer)
        {

            return _repository.hentVareIUnderKategori(Underkatnummer);
        }

        public bool OpprettBildeTilVare(string[] innbilde, int vareNr)
        {

            return _repository.OpprettBildeTilVare(innbilde, vareNr);
        }
        public List<bilderimappe> ImagesInnhold()
        {

            return _repository.ImagesInnhold();
        }
        public bool lastOppBilder(HttpPostedFileBase[] file, string mappe)
        {

            return _repository.lastOppBilder(file, mappe);
        }
        public bool SlettBilder(string filnavn, string mappenavn)
        {

            return _repository.SlettBilder(filnavn, mappenavn);
        }
       public endreVare finnVareEndre(int id)
        {

            return _repository.finnVareEndre(id);
        }

       public string slettBildeTilVare(int vareID, int bildeID)
       {

           return _repository.slettBildeTilVare(vareID, bildeID);
       }
    }
}
