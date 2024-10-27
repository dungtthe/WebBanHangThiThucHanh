using Microsoft.EntityFrameworkCore;
using System;
using System.Xml;

namespace WebBanHang.Models
{
    public class WebDbContext : DbContext
    {
        //DI
        public WebDbContext(DbContextOptions<WebDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(e => e.Images)
                .HasDefaultValue("[\"no_img.png\"]");

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<AppUser>AppUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
