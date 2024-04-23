namespace CapstoneSkinMarket.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            Orders = new HashSet<Orders>();
        }

        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Username è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Username non può superare i 50 caratteri.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il nome non può superare i 50 caratteri.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Il cognome è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il cognome non può superare i 50 caratteri.")]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "L'email è obbligatoria.")]
        [EmailAddress(ErrorMessage = "L'email non è valida.")]
        [StringLength(255, ErrorMessage = "l'email non può superare i 255 caratteri.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La password è obbligatoria.")]
        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "La password deve essere lunga almeno 3 caratteri e massimo 255.")]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DataNascita { get; set; }

        [Required(ErrorMessage = "Codice fiscale obbligatorio.")]
        [RegularExpression(@"^[A-Z]{6}[0-9]{2}[A-Z][0-9]{2}[A-Z][0-9]{3}[A-Z]$", ErrorMessage = "Il codice fiscale non è valido.")]
        public string CodFiscale { get; set; }

        [Required(ErrorMessage = "Numero di telefono obbligatorio.")]
        [Phone(ErrorMessage = "Il numero di telefono non è valido.")]
        [StringLength(15, MinimumLength = 9, ErrorMessage = "Il numero di telefono deve essere lungo almeno 9 cifre e massimo 15.")]
        public string Telefono { get; set; }

        [Required]
        [StringLength(10)]
        public string Ruolo { get; set; } = "User";

        [Required]
        [StringLength(100)]
        public string ProPic { get; set; } = "PicDefault.png";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
