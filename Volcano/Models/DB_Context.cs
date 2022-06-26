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

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartStatu> CartStatus { get; set; }
        public virtual DbSet<Category_Detail> Category_Detail { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Product_Detail> Product_Detail { get; set; }
        public virtual DbSet<Shipping_Detail> Shipping_Detail { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category_Detail>()
                .HasMany(e => e.Product_Detail)
                .WithRequired(e => e.Category_Detail)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Shipping_Detail)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product_Detail>()
                .HasMany(e => e.Carts)
                .WithRequired(e => e.Product_Detail)
                .WillCascadeOnDelete(false);
        }
    }
}
