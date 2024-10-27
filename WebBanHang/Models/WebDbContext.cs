using Microsoft.EntityFrameworkCore;
using System;

namespace WebBanHang.Models
{
    public class WebDbContext : DbContext
    {
        //DI
        public WebDbContext(DbContextOptions<WebDbContext> options) : base(options)
        {

        }
        public DbSet<AppUser>AppUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
