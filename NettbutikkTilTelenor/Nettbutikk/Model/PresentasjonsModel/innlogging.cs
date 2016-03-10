using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nettbutikk.Model.PresentasjonsModel
{
    public class innlogging
    {
        [Display(Name = "Epostadresse")]
        [Required(ErrorMessage = "epost må oppgis")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string epostadresse { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Passord")]
        [Required(ErrorMessage = "Passord må oppgis")]
        public string passord { get; set; }
    }
}