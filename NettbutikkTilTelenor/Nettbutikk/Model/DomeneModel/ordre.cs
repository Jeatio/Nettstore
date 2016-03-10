using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nettbutikk.Model.DomeneModel
{
    public class ordre
    {
        public int ordrenr { get; set; }
        public DateTime dato { get; set; }
        public int antall { get; set; }
        public int prisPrEnhet { get; set; }
        public bool erLevert { get; set; }
        public bool erBetalt { get; set; }
        public string detaljer { get; set; }
        public string kundeEpost { get; set; }
        public int vareid { get; set; }
       
        
        
    }
}