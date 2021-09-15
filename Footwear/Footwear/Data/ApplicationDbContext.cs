namespace Footwear.Data
{
    using Microsoft.EntityFrameworkCore;
    using Footwear.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext
    {
        

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<Cart> Cart { get; set; }

        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<ProductImage> ProductsImage { get; set; }

        public ApplicationDbContext(DbContextOptions options)
            :base(options)
        {

        }

        
    }
}
