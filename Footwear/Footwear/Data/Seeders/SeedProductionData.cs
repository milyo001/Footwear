
namespace Footwear.Data.Seeders
{
    using Footwear.Data.Models;
    using Footwear.Data.Models.Enums;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A class used for seeding test data in staging/production/hosting enviroment
    /// </summary>
    public static class SeedProductionData
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.ProductsImage.Any())
            {
                SeedImages(context);
            }
            if (!context.Products.Any())
            {
                SeedProducts(context);
            }
            if (!context.AppData.Any())
            {
                SeedAppData(context);
            }
        }


        private static void SeedImages(ApplicationDbContext context)
        {

        }
        private static void SeedProducts(ApplicationDbContext context)
        {
            var products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Nice Ultimate Edition",
                    Price = 185.88,
                    Details = "Perfect for running all day and night.Designed with love!",
                    ImageId = 1,
                    Gender = Gender.Men,
                    ProductType = ProductType.Hiking
                },
                new Product
                {
                    Id = 2,
                    Name = "Abibas Pure Joy",
                    Price = 124.66,
                    Details = "For Ultimate Performace.",
                    ImageId = 2,
                    Gender = Gender.Men,
                    ProductType = ProductType.Running
                },
                new Product
                {
                    Id = 3,
                    Name = "Abibas Razerblade 10x",
                    Price = 299.99,
                    Details = "Limited edition, perfect for baseketball and hiking!",
                    ImageId = 3,
                    Gender = Gender.Men,
                    ProductType = ProductType.Climbing
                },
                new Product
                {
                    Id = 4,
                    Name = "Nice Air 2020 Urban White",
                    Price = 325.99,
                    Details = "Comfort and durable all day sneaker.",
                    ImageId = 4,
                    Gender = Gender.Men,
                    ProductType = ProductType.DailyUse
                },
                new Product
                {
                    Id = 5,
                    Name = "Nice RedDragon",
                    Price = 255.99,
                    Details = "Model, still reliable tho.",
                    ImageId = 5,
                    Gender = Gender.Woman,
                    ProductType = ProductType.Hiking
                },
                new Product
                {
                    Id = 6,
                    Name = "Pumaa Lady 5.0",
                    Price = 188.99,
                    Details = "Great choise for ladies.",
                    ImageId = 6,
                    Gender = Gender.Woman,
                    ProductType = ProductType.DailyUse
                },
                new Product
                {
                    Id = 7,
                    Name = "Balvethica",
                    Price = 110.15,
                    Details = "Good for running.",
                    ImageId = 7,
                    Gender = Gender.Kids,
                    ProductType = ProductType.Running
                },
                new Product
                {
                    Id = 8,
                    Name = "VLC MAX ",
                    Price = 49.99,
                    Details = "Legendary as the media player.",
                    ImageId = 8,
                    Gender = Gender.Kids,
                    ProductType = ProductType.Climbing
                },
                new Product
                {
                    Id = 9,
                    Name = "Legion Smart",
                    Price = 35.99,
                    Details = "Definitely not made in China.",
                    ImageId = 9,
                    Gender = Gender.Woman,
                    ProductType = ProductType.DailyUse
                },
                new Product
                {
                    Id = 10,
                    Name = "Keeddo",
                    Price = 75.22,
                    Details = "A mexican style sneakers for you kids.",
                    ImageId = 10,
                    Gender = Gender.Kids,
                    ProductType = ProductType.Running
                },
                new Product
                {
                    Id = 11,
                    Name = "Legion V12",
                    Price = 35.99,
                    Details = "Only for serious hikers.",
                    ImageId = 11,
                    Gender = Gender.Men,
                    ProductType = ProductType.Hiking
                },
                new Product
                {
                    Id = 12,
                    Name = "Abdulas",
                    Price = 155.49,
                    Details = "Perfect for the gym and park walk.",
                    ImageId = 12,
                    Gender = Gender.Men,
                    ProductType = ProductType.DailyUse
                }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
        private static void SeedAppData(ApplicationDbContext context)
        {

        }
    }
}
