using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nettbutikk.Model.PresentasjonsModel
{
    public class faqSettInn
    {
        [Required]
        [RegularExpression("^[a-zæøåA-ZÆØÅ.0-9 ?\\-]{5,100}$")]
        public string sporsmal { get; set; }
        [Required]
        [RegularExpression("^[a-zæøåA-ZÆØÅ. \\-]{2,30}$")]
        public string fornavn { get; set; }

        [Required(ErrorMessage = "epost må oppgis")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string epostadresse { get; set; }
    }
}
