using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nettbutikk.Model.PresentasjonsModel
{
    public class kategori
    {
        public int kategorinr { get; set; }
        public string kategorinavn { get; set; }
        public List<underKategori> underkat { get; set; }
    }
}