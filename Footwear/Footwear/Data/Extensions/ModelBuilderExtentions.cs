namespace Footwear.Data
{
    using Microsoft.EntityFrameworkCore;
    using Footwear.Data.Models

    //An extention of ModelBuilder class. "this" keyword tells the compiler that this particular Extension Method should be added to objects of type ModelBuilder
    public static class ModelBuilderExtentions
    {
        //A method for seeding some products into the database
        public static void SeedProducts(this ModelBuilder  builder)
        {
            builder.Entity<Product>().HasData();
        }
    }
}
