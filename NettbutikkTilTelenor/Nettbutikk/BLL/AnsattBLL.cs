using Nettbutikk.DAL;
using Nettbutikk.Model.PresentasjonsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nettbutikk.BLL
{
    public class AnsattLogikk : BLL.IAnsattLogikk
    {
        private IAnsattRepository _repository;

        public AnsattLogikk()
        {
            _repository = new AnsattRepository();
        }
        public AnsattLogikk(IAnsattRepository stub)
        {
            _repository = stub;
        }
        public bool settInnAnsatt(ansatt innAnsatt)
        {
            return _repository.settInnAnsatt(innAnsatt);
        }
        public bool slettAnsatt(int id)
        {
            return _repository.slettAnsatt(id);
        }
        public List<ansatt> listAnsatte()
        {
            return _repository.listAnsatte();
        }
    }
}
