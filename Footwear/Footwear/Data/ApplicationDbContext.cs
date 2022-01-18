namespace Footwear.Data
{
    using Footwear.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<AppData> AppData { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Cart> Cart { get; set; }

        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<User> Users { get; set; }

        //Store encoded JWT tokens,where you can store user id and other information as claims,communicate with HTTP
        //between client and server by passing an token id istead of the token to prevent user from decoding the token
        public DbSet<Token> Tokens { get; set; }

        public DbSet<Address> Addresses { get; set; }
        //public DbSet<Token> Tokens { get; set; }

        public DbSet<ProductImage> ProductsImage { get; set; }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Uncomment the line below if you are starting new project, TODO: automate the process
            //builder.Seed();
        }

    }
}
