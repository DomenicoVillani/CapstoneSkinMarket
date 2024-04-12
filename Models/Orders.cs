namespace CapstoneSkinMarket.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            OrdersProducts = new HashSet<OrdersProducts>();
        }

        [Key]
        public int OrdineID { get; set; }

        public int UserID { get; set; }

        public decimal TotalePrezzo { get; set; }

        [StringLength(255)]
        public string Note { get; set; }

        public DateTime DataOrdine { get; set; }

        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string IndirizzoCasa { get; set; }
        public string IndirizzoFatturazione { get; set; }
        public int PagamentoID { get; set; }

        public virtual Users Users { get; set; }
        public virtual Pagamento Pagamento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdersProducts> OrdersProducts { get; set; }
    }
}
