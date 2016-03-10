using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Nettbutikk.Model.PresentasjonsModel
{
    public class vare
    {
        [Display (Name = "Betegnelse")]
        [Required(ErrorMessage="Varenavn må oppgis")]
        public string betegnelse { get; set; }
        
        [Display(Name = "Pris")]
        [Required(ErrorMessage="Pris må settes")]
        public int pris { get; set; }
        
        [Display(Name = "Antall")]
        [Required(ErrorMessage="Antall må velges")]
        public int antall { get; set; }

        [Display(Name = "Detaljer")]
        [Required(ErrorMessage = "Skriv detaljer")]
        public string detaljer { get; set; }
        
        [Display(Name = "Kort Detaljer")]
        [Required(ErrorMessage = "Skriv detaljer")]
        [RegularExpression(@"^[a-åA-Å0-9 \s\S]{1,30}$", ErrorMessage = "Detaljer kan ikke være mer enn 30 bokstaver")]
        public string fremvisningsdetaljer { get; set; }


        [Display(Name = "Kategorinavn")]
        [Required(ErrorMessage = "Kategorinavn må oppgis")]
        public string katnavn { get; set; }

        [Display(Name = "UnderKategori navn")]
        [Required(ErrorMessage = "Underkategorinavn må oppgis")]
        public string underkatnavn { get; set; }

       
    }

}