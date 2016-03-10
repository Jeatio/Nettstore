using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nettbutikk.Model.PresentasjonsModel
{
    public class visallekvitteringer
    {

        public int kvitteringsid { get; set; }
        [Display(Name = "Dato")]
        public DateTime dato { get; set; }

        [Display(Name = "Beløp")]
        public int pris { get; set; }

    }
}