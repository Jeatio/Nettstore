using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nettbutikk.Model.PresentasjonsModel
{
    public class bilde
    {
        [Display(Name = "Bilde")]
        [Required(ErrorMessage = "skriv inn bildestreng")]

        public string src { get; set; }
        
        [Display(Name = "Vare")]
        [Required(ErrorMessage = "skriv inn hvilken vare det er bilde av")]

        public int vare{ get; set; }
    }
}