namespace Footwear.Data
{
    using Footwear.Data.Models;
    using Footwear.Data.Seeders;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Hosting;

    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<AppData> AppData { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Cart> Cart { get; set; }

        public DbSet<CartProduct> CartProducts { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<ProductImage> ProductsImage { get; set; }

        private IWebHostEnvironment CurrentEnvironment { get; }

        public ApplicationDbContext(DbContextOptions options, IWebHostEnvironment env)
            : base(options)
        {
            this.CurrentEnvironment = env;
            if (env.IsDevelopment())
            {
                this.Database.EnsureCreated();
            }

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            if (this.CurrentEnvironment.IsDevelopment())
            {
                builder.Seed();
            }
            if(this.CurrentEnvironment.IsProduction() || this.CurrentEnvironment.IsStaging())
            {
                SeedProductionData.Seed(this);
            }
            
        }

    }
}
