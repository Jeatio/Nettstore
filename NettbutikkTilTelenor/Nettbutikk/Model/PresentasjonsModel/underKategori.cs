
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nettbutikk.Model.PresentasjonsModel
{
   public class underKategori
    {
        public int id { get; set; }
        public string navn { get; set; }
        public List<visVareInfo> varer { get; set; }

    }
}
