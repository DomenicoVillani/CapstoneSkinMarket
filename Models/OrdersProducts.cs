using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CapstoneSkinMarket.Models
{
    [Table("OrdersProducts")]
    public class OrdersProducts
    {
        public int ArticoloID { get; set; }
        public int OrdineID { get; set; }

        public virtual Products Prodotti { get; set; }
        public virtual Orders Ordini { get; set; }
    }
}