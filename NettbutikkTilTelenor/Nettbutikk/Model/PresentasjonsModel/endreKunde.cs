using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nettbutikk.Model.PresentasjonsModel
{
    public class endreKunde
    {
        [Display(Name = "Fornavn")]
        [Required(ErrorMessage = "Fornavn må oppgis")]
        [RegularExpression(@"[a-åA-Å ]*", ErrorMessage = "Fornavn inneholder ulovlige tegn")]
        public string fornavn { get; set; }

        [Display(Name = "Etternavn")]
        [Required(ErrorMessage = "Etternavn må oppgis")]
        [RegularExpression(@"[a-åA-Å ]*", ErrorMessage = "Etternavn inneholder ulovlige tegn")]

        public string etternavn { get; set; }

        [Display(Name = "Adresse")]
        [Required(ErrorMessage = "Adressen må oppgis")]
        [RegularExpression(@"[a-åA-Å0-9 ]*", ErrorMessage = "Adressen inneholder ulovlige tegn")]

        public string adresse { get; set; }

        [Display(Name = "Telefonnummer")]
        [Required(ErrorMessage = "telefonnummer må oppgis")]
        [RegularExpression(@"[0-9]{8,}", ErrorMessage = "Telfonnummer må være minst 8 siffer")]
        
        public string telefonnummer { get; set; }

        [Display(Name = "Postnr")]
        [Required(ErrorMessage = "Postnr må oppgis")]
        [RegularExpression(@"[0-9]{4}", ErrorMessage = "Postnr må være 4 siffer")]
        public string postnr { get; set; }

        [Display(Name = "Poststed")]
        [Required(ErrorMessage = "Poststed må oppgis")]
        [RegularExpression(@"[a-åA-Å ]*", ErrorMessage = "Poststed inneholder ulovlige tegn")]
        public string poststeder { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Passord")]
        [Required(ErrorMessage = "Passord må oppgis")]
        public string passord { get; set; }

    }
}