using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneSkinMarket.Models
{
    public class Cart
    {
        public Products Products { get; set; }
        public Users Users { get; set; }
        public int Quantita { get; set; }
        public int UserID { get; set; }
    }
}