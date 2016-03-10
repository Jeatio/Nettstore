using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nettbutikk.Model.PresentasjonsModel
{
    public class faqPresentasjon
    {
        public int id { get; set; }
        public string sporsmal { get; set; }
        public string svar { get; set; }
        public string kategori { get; set; }
        public bool fremvisning { get; set; }
        public string bilde { get; set; }

        public int antallKlikk { get; set; }

    }
}
