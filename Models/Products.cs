namespace CapstoneSkinMarket.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products()
        {
            Orders = new HashSet<Orders>();
        }

        [Key]
        public int ArticoloID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Immagine { get; set; }

        [Required]
        [StringLength(255)]
        public string Descrizione { get; set; }

        public decimal Prezzo { get; set; }

        [Required]
        [StringLength(20)]
        public string Rarita { get; set; }

        public int GiocoID { get; set; }

        public virtual Games Games { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
