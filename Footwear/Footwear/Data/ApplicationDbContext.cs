﻿namespace Footwear.Data
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

        public DbSet<Address> Addresses { get; set; }

#pragma warning disable CS0114 // Member hides inherited member;
        public DbSet<User> Users { get; set; }

#pragma warning restore CS0114 // Member hides inherited member;

        public DbSet<ProductImage> ProductsImage { get; set; }

        private IWebHostEnvironment CurrentEnvironment { get; }

        public ApplicationDbContext(DbContextOptions options, IWebHostEnvironment env)
            : base(options)
        {
            this.CurrentEnvironment = env;

            if (!env.IsDevelopment())
            {
                SeedProductionData.Seed(this);
            }
            else
            {
                this.Database.EnsureCreated();
            }
           
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<AppData>()
                .HasNoKey();

            if (this.CurrentEnvironment.IsDevelopment())
            {
                builder.Seed();
            }
        }

    }
}
