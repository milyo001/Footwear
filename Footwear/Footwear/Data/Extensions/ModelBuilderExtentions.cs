namespace Footwear.Data
{
    using Footwear.Data.Models;
    using Footwear.Data.Models.Enums;
    using Microsoft.EntityFrameworkCore;

    // An extention of ModelBuilder
    public static class ModelBuilderExtentions
    {
        ///<summary>
        /// Seed the database with data
        ///</summary>
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<ProductImage>().HasData(
            new ProductImage
            {
                Id = 1,
                ImageUrl = "https://cdn.pixabay.com/photo/2016/11/21/15/54/countryside-1846093_1280.jpg"
            },
             new ProductImage
             {
                 Id = 2,
                 ImageUrl = "https://cdn.pixabay.com/photo/2017/07/30/15/49/adidas-2554690_1280.jpg"
             },
             new ProductImage
             {
                 Id = 3,
                 ImageUrl = "https://cdn.pixabay.com/photo/2017/07/13/02/53/shoe-2498994_1280.jpg"
             },
             new ProductImage
             {
                 Id = 4,
                 ImageUrl = "https://cdn.pixabay.com/photo/2020/05/03/19/09/nike-5126389_1280.jpg"
             },
             new ProductImage
             {
                 Id = 5,
                 ImageUrl = "https://cdn.pixabay.com/photo/2016/04/12/14/08/shoe-1324431_1280.jpg"
             },
             new ProductImage
             {
                 Id = 6,
                 ImageUrl = "https://cdn.pixabay.com/photo/2018/09/30/21/30/puma-3714734_1280.jpg"
             },
             new ProductImage
             {
                 Id = 7,
                 ImageUrl = "https://images.footlocker.com/is/image/FLEU/315240315002_01?wid=763&hei=538&fmt=png-alpha"
             },
             new ProductImage
             {
                 Id = 8,
                 ImageUrl = "https://static.nike.com/a/images/t_PDP_1280_v1/f_auto,q_auto:eco/fb5fdd9f-a9d0-4bcc-8258-d4fbf6751de0/jordan-ma2-shoe-dmkgC9.png"
             },
             new ProductImage
             {
                 Id = 9,
                 ImageUrl = "https://m.media-amazon.com/images/I/711BXI98hKL._AC_SR255,340_.jpg"
             },
             new ProductImage
             {
                 Id = 10,
                 ImageUrl = "https://pyxis.nymag.com/v1/imgs/72a/06b/e8ae5bc2097f7531dfdc690095c55e319c-25-kids-sneakers-1.2x.rsquare.w600.jpg"
             },
             new ProductImage
             {
                 Id = 11,
                 ImageUrl = "https://assets.hermes.com/is/image/hermesproduct/avatar-sneaker--201463ZH01-front-1-300-0-1000-1000.jpg"
             },
             new ProductImage
             {
                 Id = 12,
                 ImageUrl = "https://footwearnews.com/wp-content/uploads/2019/03/m20324_01_standard-e1551720111734.jpg?w=700&h=437&crop=1"
             });

            builder.Entity<Product>().HasData(
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
                );

            builder.Entity<AppData>().HasData(
                new AppData
                {
                    DeliveryPrice = 5,
                    MinDelivery = 1,
                    MaxDelivery = 3
                });
        }


    }
}
