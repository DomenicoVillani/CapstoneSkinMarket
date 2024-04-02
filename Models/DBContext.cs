using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CapstoneSkinMarket.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<Games> Games { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Games>()
                .Property(e => e.NomeGioco)
                .IsUnicode(false);

            modelBuilder.Entity<Games>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Games)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.TotalePrezzo)
                .HasPrecision(6, 2);

            modelBuilder.Entity<Orders>()
                .Property(e => e.Note)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .HasMany(e => e.Products)
                .WithMany(e => e.Orders)
                .Map(m => m.ToTable("OrdersProducts").MapLeftKey("OrdineID").MapRightKey("ArticoloID"));

            modelBuilder.Entity<Products>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Products>()
                .Property(e => e.Immagine)
                .IsUnicode(false);

            modelBuilder.Entity<Products>()
                .Property(e => e.Descrizione)
                .IsUnicode(false);

            modelBuilder.Entity<Products>()
                .Property(e => e.Prezzo)
                .HasPrecision(6, 2);

            modelBuilder.Entity<Products>()
                .Property(e => e.Rarita)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Cognome)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.CodFiscale)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Ruolo)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete(false);
        }
    }
}
