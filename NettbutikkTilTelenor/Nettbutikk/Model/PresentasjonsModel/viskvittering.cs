using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nettbutikk.Model.PresentasjonsModel
{
    public class viskvittering
    {
        [Display(Name = "Dato")]
        public DateTime dato { get; set; }
        [Display(Name = "Betegnelse")]
        public string betegnelse { get; set; }
        [Display(Name = "Antall")]
        public int antall { get; set; }
        [Display(Name = "Pris Pr Enhet")]
        public int prisPrEnhet { get; set; }
        [Display(Name = "Total Pris")]
        public int totpris { get; set; }
        [Display(Name = "Status")]
        public string detaljer { get; set; }
       
    }
}