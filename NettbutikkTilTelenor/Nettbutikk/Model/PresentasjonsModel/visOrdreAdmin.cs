using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nettbutikk.Model.PresentasjonsModel
{
   public class visOrdreAdmin
    {
       [Display(Name = "Epostadresse")]
       [DataType(DataType.EmailAddress)]
       [EmailAddress]
        public string epostadresse { get; set; }
       [Display(Name = "Fornavn")] 
        public string fornavn { get; set; }
       [Display(Name = "Etternavn")] 
        public string etternavn { get; set; }
       [Display(Name = "Dato")]
        public DateTime dato { get; set; }
        [Display(Name = "Beløp")]
        public int pris { get; set; }
        public int id { get; set; }

    }
}
