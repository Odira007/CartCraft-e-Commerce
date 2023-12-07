using CartCraft.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartCraft.Infrastructure.DbContexts
{
    public class CartCraftDbContext : DbContext
    {
        public CartCraftDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresss { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Seller> Sellers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Image>()
            //    .HasOne(x => x.Product)
            //    .WithMany(x => x.Images)
            //    .HasForeignKey(x => x.ProductId)
            //    .IsRequired();
        }
    }
}
