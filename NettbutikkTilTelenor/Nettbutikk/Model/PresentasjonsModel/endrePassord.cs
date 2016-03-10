using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nettbutikk.Model.PresentasjonsModel
{
    public class endrePassord
    {
        [DataType(DataType.Password)]
        [Display(Name = "Gammelt Passord")]
        [Required(ErrorMessage = "Passord må oppgis")]
        public string gammelPassord { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nytt Passord")]
        [Required(ErrorMessage = "Passord må oppgis")]
        public string nyttPassord { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bekreft Passord")]
        [Required(ErrorMessage = "Passord må oppgis")]
        [Compare("nyttPassord")]
        public string bekreftPassord { get; set; }

    }
}