using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nettbutikk.Model.PresentasjonsModel
{
    public class betaling
    {
        [Display(Name = "Kortnummer")]
        [Required(ErrorMessage = "skriv inn kortnummer")]
        [RegularExpression(@"[0-9]{16}",ErrorMessage="Feil antall og/eller tegn")]

        public string kortnr { get; set; }

        [Display(Name = "Utløpsdato (MM/åå)")]
        [Required(ErrorMessage = "skriv inn utløpsdato til kortet")]
        [RegularExpression(@"[0-9]{2}\/\d{2}", ErrorMessage = "Skriv inn dato: Eks: 12/15")]
        public string utløpsdato { get; set; }

        [Display(Name = "CVC/CVV")]
        [Required(ErrorMessage = "skriv inn CVC/CVV")]
        [RegularExpression(@"[0-9]{3}", ErrorMessage = "Feil antall og/eller tegn. Se CVC")]
        public string CVC { get; set; }

    }
}