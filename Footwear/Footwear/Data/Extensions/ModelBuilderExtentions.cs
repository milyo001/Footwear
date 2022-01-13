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
                new Product
                {
                    Name = "Nice Ultimate Edition",
                    Price = 185.88,
                    Details = "Perfect for running all day and night.Designed with love!",
                    ImageId = 1,
                    Gender = Gender.Men,
                    ProductType = ProductType.Hiking
                },
                new Product
                {
                    Name = "Abibas Pure Joy",
                    Price = 124.66,
                    Details = "For Ultimate Performace.",
                    ImageId = 2,
                    Gender = Gender.Men,
                    ProductType = ProductType.Running
                },
                new Product
                {
                    Name = "Abibas Razerblade 10x",
                    Price = 299.99,
                    Details = "Limited edition, perfect for baseketball and hiking!",
                    ImageId = 3,
                    Gender = Gender.Men,
                    ProductType = ProductType.Climbing
                },
                new Product
                {
                    Name = "Nice Air 2020 Urban White",
                    Price = 325.99,
                    Details = "Comfort and durable all day sneaker.",
                    ImageId = 4,
                    Gender = Gender.Men,
                    ProductType = ProductType.DailyUse
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
                new Product
                {
                    Name = "Pumaa Lady 5.0",
                    Price = 188.99,
                    Details = "Great choise for ladies.",
                    ImageId = 6,
                    Gender = Gender.Woman,
                    ProductType = ProductType.DailyUse
                },
                new Product
                {
                    Name = "Balvethica",
                    Price = 110.15,
                    Details = "Good for running.",
                    ImageId = 7,
                    Gender = Gender.Kids,
                    ProductType = ProductType.Running
                },
                new Product
                {
                    Name = "VLC MAX ",
                    Price = 49.99,
                    Details = "Legendary as the media player.",
                    ImageId = 8,
                    Gender = Gender.Kids,
                    ProductType = ProductType.Climbing
                },
                new Product
                {
                    Name = "Legion Smart",
                    Price = 35.99,
                    Details = "Definitely not made in China.",
                    ImageId = 9,
                    Gender = Gender.Woman,
                    ProductType = ProductType.DailyUse
                },
                new Product
                {
                    Name = "Keeddo",
                    Price = 75.22,
                    Details = "A mexican style sneakers for you kids.",
                    ImageId = 10,
                    Gender = Gender.Kids,
                    ProductType = ProductType.Running
                },
                new Product
                {
                    Name = "Legion V12",
                    Price = 35.99,
                    Details = "Only for serious hikers.",
                    ImageId = 11,
                    Gender = Gender.Men,
                    ProductType = ProductType.Hiking
                },
                new Product
                {
                    Name = "Abdulas",
                    Price = 155.49,
                    Details = "Perfect for the gym and park walk.",
                    ImageId = 12,
                    Gender = Gender.Men,
                    ProductType = ProductType.DailyUse
                }
                );

            //            Id Name    Price Details ImageId Size    Gender ProductType
            //14  Abdulas 155.49  Perfect for the gym and park walk.  12  NULL    0   3
        }


    }
}
