using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nettbutikk.DAL;
using Nettbutikk.Model;
using Nettbutikk.Model.PresentasjonsModel;

namespace Nettbutikk.BLL
{
    public class OrdreLogikk : BLL.IOrdreLogikk
    {
        private IOrdreRepository _repository;

        public OrdreLogikk ()
        {
            _repository = new OrdreRepository();
        }
        public OrdreLogikk (IOrdreRepository stub)
        {
            _repository = stub;
        }
        public bool leggIHandlevogn(int vareid, string kundeEmail)
        {

            return _repository.leggIHandlevogn(vareid, kundeEmail);
        }

        public List<visordre> visHandlevogn(string kundeEmail)
        {

            return _repository.visHandlevogn(kundeEmail);
        }

        public bool slettOrdre(int ordrenr, string kundeEpost)
        {

            return _repository.slettOrdre(ordrenr, kundeEpost);
        }

        public bool endreAntallVarer(int ant, int ordrenummer, string kundeEpost)
        {

            return _repository.endreAntallVarer(ant, ordrenummer, kundeEpost);
        }
        
        public int RegnPris(string kundeEpost)
        {

            return _repository.RegnPris(kundeEpost);

        }

        public bool KjøpVarer(string kundeEpost)
        {

            return _repository.KjøpVarer(kundeEpost);
        }

        public List<visallekvitteringer> visAlleKvitteringer(string kundeEpost)
        {

            return _repository.visAlleKvitteringer(kundeEpost);

        }
        public List<visOrdreAdmin> visAlleKvitteringer()
        {

            return _repository.visAlleKvitteringer();
        }

        public List<viskvittering> visKvittering(string kundeEmail, int kvitteringsID)
        {

            return _repository.visKvittering(kundeEmail, kvitteringsID);
        }
    }
}
