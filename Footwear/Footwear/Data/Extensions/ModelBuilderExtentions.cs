﻿namespace Footwear.Data
{
    using Footwear.Data.Models;
    using Footwear.Data.Models.Enums;
    using Microsoft.EntityFrameworkCore;

    //An extention of ModelBuilder class. "this" keyword tells the compiler that this particular Extension Method should be added to objects of type ModelBuilder
    public static class ModelBuilderExtentions
    {
        ///<summary>
        ///Seed the database with data
        ///</summary>
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Nice Ultimate Edition",
                    Price = 185.88,
                    Details = "Perfect for running all day and night.Designed with love!",
                    ProductImage = new ProductImage 
                    {
                        Id = 1,
                        ImageUrl = "https://cdn.pixabay.com/photo/2016/11/21/15/54/countryside-1846093_1280.jpg"
                    },
                    Gender = Gender.Men,
                    ProductType = ProductType.Hiking
                },
                new Product
                {
                    Id = 2,
                    Name = "Abibas Pure Joy",
                    Price = 124.66,
                    Details = "For Ultimate Performace.",
                    ProductImage = new ProductImage
                    {
                        Id = 2,
                        ImageUrl = "https://cdn.pixabay.com/photo/2017/07/30/15/49/adidas-2554690_1280.jpg"
                    },
                    Gender = Gender.Men,
                    ProductType = ProductType.Running
                },
                new Product
                {
                    Id = 3,
                    Name = "Abibas Razerblade 10x",
                    Price = 299.99,
                    Details = "Limited edition, perfect for baseketball and hiking!",
                    ProductImage = new ProductImage
                    {
                        Id = 3,
                        ImageUrl = "https://cdn.pixabay.com/photo/2017/07/13/02/53/shoe-2498994_1280.jpg"
                    },
                    Gender = Gender.Men,
                    ProductType = ProductType.Climbing
                },
                new Product
                {
                    Id = 4,
                    Name = "Nice Air 2020 Urban White",
                    Price = 325.99,
                    Details = "Comfort and durable all day sneaker.",
                    ProductImage = new ProductImage
                    {
                        Id = 4,
                        ImageUrl = "https://cdn.pixabay.com/photo/2020/05/03/19/09/nike-5126389_1280.jpg"
                    },
                    Gender = Gender.Men,
                    ProductType = ProductType.DailyUse
                },
                new Product
                {
                    Id = 5,
                    Name = "Nice RedDragon",
                    Price = 255.99,
                    Details = "Model, still reliable tho.",
                    ProductImage = new ProductImage
                    {
                        Id = 5,
                        ImageUrl = "https://cdn.pixabay.com/photo/2016/04/12/14/08/shoe-1324431_1280.jpg"
                    },
                    Gender = Gender.Woman,
                    ProductType = ProductType.Hiking
                },
                new Product
                {
                    Id = 6,
                    Name = "Pumaa Lady 5.0",
                    Price = 188.99,
                    Details = "Great choise for ladies.",
                    ProductImage = new ProductImage
                    {
                        Id = 6,
                        ImageUrl = "https://cdn.pixabay.com/photo/2018/09/30/21/30/puma-3714734_1280.jpg"
                    },
                    Gender = Gender.Woman,
                    ProductType = ProductType.DailyUse
                },
                new Product
                {
                    Id = 7,
                    Name = "Balvethica",
                    Price = 110.15,
                    Details = "Good for running.",
                    ProductImage = new ProductImage
                    {
                        Id = 7,
                        ImageUrl = "https://images.footlocker.com/is/image/FLEU/315240315002_01?wid=763&hei=538&fmt=png-alpha"
                    },
                    Gender = Gender.Kids,
                    ProductType = ProductType.Running
                },
                new Product
                {
                    Id = 8,
                    Name = "VLC MAX ",
                    Price = 49.99,
                    Details = "Legendary as the media player.",
                    ProductImage = new ProductImage
                    {
                        Id = 8,
                        ImageUrl = "https://media.gq.com/photos/5d93aa2c636d4800084025ae/4:3/w_1600%2Cc_limit/sneakers.jpg"
                    },
                    Gender = Gender.Kids,
                    ProductType = ProductType.Climbing
                },
                new Product
                {
                    Id = 9,
                    Name = "Legion Smart",
                    Price = 35.99,
                    Details = "Definitely not made in China.",
                    ProductImage = new ProductImage
                    {
                        Id = 9,
                        ImageUrl = "https://m.media-amazon.com/images/I/711BXI98hKL._AC_SR255,340_.jpg"
                    },
                    Gender = Gender.Woman,
                    ProductType = ProductType.DailyUse
                },
                new Product
                {
                    Id = 10,
                    Name = "Keeddo",
                    Price = 75.22,
                    Details = "A mexican style sneakers for you kids.",
                    ProductImage = new ProductImage
                    {
                        Id = 10,
                        ImageUrl = "https://pyxis.nymag.com/v1/imgs/72a/06b/e8ae5bc2097f7531dfdc690095c55e319c-25-kids-sneakers-1.2x.rsquare.w600.jpg"
                    },
                    Gender = Gender.Kids,
                    ProductType = ProductType.Running
                },
                new Product
                {
                    Id = 11,
                    Name = "Legion V12",
                    Price = 35.99,
                    Details = "Only for serious hikers.",
                    ProductImage = new ProductImage
                    {
                        Id = 11,
                        ImageUrl = "https://assets.hermes.com/is/image/hermesproduct/avatar-sneaker--201463ZH01-front-1-300-0-1000-1000.jpg"
                    },
                    Gender = Gender.Men,
                    ProductType = ProductType.Hiking
                },
                new Product
                {
                    Id = 12,
                    Name = "Abdulas",
                    Price = 155.49,
                    Details = "Perfect for the gym and park walk.",
                    ProductImage = new ProductImage
                    {
                        Id = 12,
                        ImageUrl = "https://footwearnews.com/wp-content/uploads/2019/03/m20324_01_standard-e1551720111734.jpg?w=700&h=437&crop=1"
                    },
                    Gender = Gender.Men,
                    ProductType = ProductType.DailyUse
                }
                );

        }


    }
}