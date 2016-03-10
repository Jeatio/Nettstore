using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nettbutikk.Model.PresentasjonsModel
{
    public class displayVarer
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
    }
}