namespace Footwear.Data
{
    using Microsoft.EntityFrameworkCore;
    using Footwear.Data.Models;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Product> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProductImage> ProductsImage { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) 
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
