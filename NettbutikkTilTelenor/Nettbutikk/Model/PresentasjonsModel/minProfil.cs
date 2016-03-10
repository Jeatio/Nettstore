using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nettbutikk.Model.PresentasjonsModel
{
    public class minProfil
    {
         [Display(Name = "Fornavn")]    
        public string fornavn { get; set; }
        
        [Display(Name = "Etternavn")] 
        public string etternavn { get; set; }

        [Display(Name = "Adresse")]
        public string adresse { get; set; }

        [Display(Name = "Telefonnummer")]  
        public string telefonnummer { get; set; }

        [Display(Name = "Epostadresse")]   
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string epostadresse { get; set; }
        
        [Display(Name = "Postnr")]
        
        public string postnr { get; set; }

        [Display(Name = "Poststed")]
        public string poststeder { get; set; }

    }
}