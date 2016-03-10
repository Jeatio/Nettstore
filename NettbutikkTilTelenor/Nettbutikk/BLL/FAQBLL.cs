using Nettbutikk.DAL;
using Nettbutikk.Model.PresentasjonsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nettbutikk.BLL
{
    public class FAQBLL
    {
        public List<faqPresentasjon> FrontSporsmal()
        {
            var _repository = new FAQRepository();
            return _repository.FrontSporsmal();
        }
        public bool settInnSporsmal(faqSettInn innFAQ)
        {
            var _repository = new FAQRepository();
            return _repository.settInnSporsmal(innFAQ);
        }
        public List<faqPresentasjon> AlleSporsmal()
        {
            var _repository = new FAQRepository();
            return _repository.AlleSporsmal();
        }
        public List<faqPresentasjon> SporsmalIKategori(int id)
        {
            var _repository = new FAQRepository();
            return _repository.SporsmalIKategori(id);
        }
        public List<faqPresentasjon> ListKategorier()
        {
            var _repository = new FAQRepository();
            return _repository.ListKategorier();
        }
        public bool oppdaterAntallKlikk (int id, faqPresentasjon innFAQ)
        {
            var _repository = new FAQRepository();
            return _repository.oppdaterAntallKlikk(id, innFAQ);
        }
    }
}
