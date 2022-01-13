namespace Footwear.Data
{
    using Footwear.Data.Models;
    using Footwear.Data.Models.Enums;
    using Microsoft.EntityFrameworkCore;

    //An extention of ModelBuilder class. "this" keyword tells the compiler that this particular Extension Method should be added to objects of type ModelBuilder
    public static class ModelBuilderExtentions
    {
        //A method for seeding some products into the database
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Product>().HasData(
                new Product { Name = "Nice Ultimate Edition", Price = 185.88, Details = "Perfect for running all day and night.Designed with love!", ImageId = 1, Gender = Gender.Men, ProductType = ProductType.Hiking },

                new Product { Name = "Abibas Pure Joy", Price = 124.66, Details = "For Ultimate Performace.", ImageId = 2, Gender = Gender.Men, ProductType = ProductType.Running },

                new Product { Name = "Abibas Razerblade 10x", Price = 299.99, Details = "Limited edition, perfect for baseketball and hiking!", ImageId = 3, Gender = Gender.Men, ProductType = ProductType.Climbing },

                new Product { Name = "Nice Air 2020 Urban White", Price = 325.99, Details = "Comfort and durable all day sneaker.", ImageId = 4, Gender = Gender.Men, ProductType = ProductType.DailyUse },

                new Product
                {
                    Name = "Nice RedDragon",
                    Price = 255.99,
                    Details = "Model, still reliable tho.",
                    ImageId = 5,
                    Gender = Gender.Woman,
                    ProductType = ProductType.Hiking
                },

                new Product
                {
                    Name = "Nice RedDragon",
                    Price = 255.99,
                    Details = "Model, still reliable tho.",
                    ImageId = 5,
                    Gender = Gender.Woman,
                    ProductType = ProductType.Hiking
                },
                );

            //Id	Name	Price	Details	ImageId	Size	Gender	ProductType
            //7   Nice RedDragon  255.99  2018 Model, still reliable tho. 5   NULL    1   1
        }


    }
}
