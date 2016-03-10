using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nettbutikk.Model.PresentasjonsModel
{
    public class ansatt
    {
        public int idnr { get; set; }
        [Display(Name="Brukernavn")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Required(ErrorMessage = "Skriv som epost: eksempel@hd.no")]
        public string brukernavn { get; set; }
        public bool eradmin { get; set; }
        [Display(Name="Fornavn")]
        [Required(ErrorMessage = "Fornavn må oppgis")]
        [RegularExpression(@"[a-åA-Å ]*", ErrorMessage = "Fornavn inneholder ulovlige tegn")]
        public string fornavn { get; set; }
        [Display(Name = "Etternavn")]
        [Required(ErrorMessage = "Etternavn må oppgis")]
        [RegularExpression(@"[a-åA-Å ]*", ErrorMessage = "Etternavn inneholder ulovlige tegn")]
        public string etternavn { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Passord")]
        [Required(ErrorMessage = "Passord må oppgis")]
        public string passord { get; set; }
       
    }
}
