using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nettbutikk.DAL;
using Nettbutikk.Model.DomeneModel;
using Nettbutikk.Model.PresentasjonsModel;

namespace Nettbutikk.BLL
{
    public class KundeLogikk : BLL.IKundeLogikk
    {
        private IKundeRepository _repository;
        
        public KundeLogikk ()
        {
            _repository = new KundeRepository();
        }
        public KundeLogikk (IKundeRepository stub)
        {
            _repository = stub;
        }
        public List<minProfil> hentAlle()
        {

            List<minProfil> alleKunder = _repository.hentAlle();
            return alleKunder;

        }
        public bool settInnKunde(kunde innKunde)
        {

            return _repository.settInnKunde(innKunde);
        }
        public bool endrePassord(string email, endrePassord pass)
        {

            return _repository.endrePassord(email, pass);
        }

        public kunde finnKunde(string email)
        {

            return _repository.finnKunde(email);
        }
        public endreKunde finnKundeEndre(string email)
        {

            return _repository.finnKundeEndre(email);
        }
        public minProfil finnKundeProfil(string email)
        {

            return _repository.finnKundeProfil(email);
        }

        public bool slettKunde(string email)
        {

            return _repository.slettKunde(email);
        }
        public bool endreKunde(string email, endreKunde innKunde)
        {

            return _repository.endreKunde(email, innKunde);

        }

        public string hashPassord(string funktohash)
        {

            return _repository.hashPassord(funktohash);
        }


        public kunde loggInnOK(string email, string passord)
        {

            return _repository.loggInnOK(email, passord);
        }
         public ansatt erAdmin (string navn, string passord)
        {

            return _repository.erAdmin(navn, passord);
        }

    }
}
