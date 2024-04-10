namespace CapstoneSkinMarket.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("OrdersProducts")]
    public partial class OrdersProducts
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ArticoloID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrdineID { get; set; }

        [Column(Order = 2)]
        public int Quantita { get; set; }

        public virtual Products Products { get; set; }
        public virtual Orders Orders { get; set; }

    }
}