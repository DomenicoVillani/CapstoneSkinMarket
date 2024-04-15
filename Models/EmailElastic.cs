using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneSkinMarket.Models
{
    public class EmailElastic
    {
        public string NomeCognome { get; set; }
        public string Oggetto { get; set; }
        public string ContenutoEmail { get; set; }
        public string EmailMittente { get; set; }

    }
}