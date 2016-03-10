using Nettbutikk.Model.PresentasjonsModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nettbutikk.Model.PresentasjonsModel
{
   public class endreVare
    {
        public int idnr { get; set; }

        [Display(Name = "Betegnelse")]
        public string betegnelse { get; set; }

        [Display(Name = "Pris")]
        public int pris { get; set; }

        [Display(Name = "Antall")]
        public int antall { get; set; }

        [Display(Name = "Detaljer")]
        public string detaljer { get; set; }

        [Display(Name = "Bilde")]
        public List<visBilde> bilde { get; set; }

        [Display(Name = "Kort Detaljer")]
        public string fremvisningsdetaljer { get; set; }

        [Display(Name = "Kategorinavn")]
        public string katnavn { get; set; }
        public int katid { get; set; }

        [Display(Name = "UnderKategori navn")]
        public string underkatnavn { get; set; }
        public int underkatid { get; set; }
    }
}
