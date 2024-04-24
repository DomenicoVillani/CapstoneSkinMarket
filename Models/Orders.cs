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

        [StringLength(255, ErrorMessage = "Le note non possono superare i 255 caratteri.")]
        public string Note { get; set; }

        public DateTime DataOrdine { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio.")]
        [StringLength(255, ErrorMessage = "Il nome non può superare i 255 caratteri.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Il cognome è obbligatorio.")]
        [StringLength(255, ErrorMessage = "Il cognome non può superare i 255 caratteri.")]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "L'indirizzo di casa è obbligatorio.")]
        [StringLength(255, ErrorMessage = "L'indirizzo di casa non può superare i 255 caratteri.")]
        public string IndirizzoCasa { get; set; }

        [Required(ErrorMessage = "L'indirizzo di fatturazione è obbligatorio.")]
        [StringLength(255, ErrorMessage = "L'indirizzo di fatturazione non può superare i 255 caratteri.")]
        public string IndirizzoFatturazione { get; set; }
        public int PagamentoID { get; set; }

        public virtual Users Users { get; set; }
        public virtual Pagamento Pagamento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdersProducts> OrdersProducts { get; set; }
    }
}
