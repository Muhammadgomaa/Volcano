using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Volcano.Models
{
    public partial class DB_Context : DbContext
    {
        public DB_Context()
            : base("name=DB_Context")
        {
        }

        public virtual DbSet<Category_Detail> Category_Detail { get; set; }
        public virtual DbSet<Invoice_Detail> Invoice_Detail { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Product_Detail> Product_Detail { get; set; }
        public virtual DbSet<Refund_Detail> Refund_Detail { get; set; }
        public virtual DbSet<Shipping_Detail> Shipping_Detail { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
                .HasMany(e => e.Refund_Detail)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Shipping_Detail)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product_Detail>()
                .HasMany(e => e.Invoice_Detail)
                .WithRequired(e => e.Product_Detail)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shipping_Detail>()
                .HasMany(e => e.Invoice_Detail)
                .WithRequired(e => e.Shipping_Detail)
                .WillCascadeOnDelete(false);
        }
    }
}
