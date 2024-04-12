using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CapstoneSkinMarket.Models
{
    [Table("Pagamento")]
    public class Pagamento
    {
        [Key]
        public int PagamentoID { get; set; }
        public string TipoPagamento { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}