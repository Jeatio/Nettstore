using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nettbutikk.Model.PresentasjonsModel
{
    public class visVareInfo
    {
        [Display(Name = "Betegnelse")]
        public string betegnelse { get; set; }

        [Display(Name = "Pris")]
        public int pris { get; set; }

        [Display(Name = "Antall")]
        public int antall { get; set; }

        [Display(Name = "Detaljer")]
        public string detaljer { get; set; }

        [Display(Name = "Kort Detaljer")]
        public string fremvisningsdetaljer { get; set; }

        [Display(Name = "Kategori")]    
        public int kategori { get; set; }

        [Display(Name = "Katnavn")]

        public string katnavn { get; set; }

        [Display(Name = "Underkategori")]

        public int underkategori { get; set; }

        [Display(Name = "UnderKatnavn")]
        public string underkatnavn { get; set; }
    }
}