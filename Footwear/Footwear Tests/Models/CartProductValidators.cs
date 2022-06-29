namespace Footwear_Tests.Models
{
    using Footwear.Data.Models;
    using Xunit;
    
    public class CartProductValidators
    {
        [Fact]
        public void Test_CartProduct_Model_If_Working_AsExpected()
        {
            var cartProduct = new CartProduct
            {
                Id = 1,
                Name = "Adidas 11",
                Price = 100,
                Details = "Adidas 11 pro sneakers lol",
                ImageUrl = "https://www.adidas.com/us/adidas-11-pro-sneakers/BQ0366_010_RS.jpg",
                Size = 42,
                Quantity = 1,
                CartId = 1,
                ProductId = 1,
                Gender = Footwear.Data.Models.Enums.Gender.Men,
                IsOrdered = true,
                OrderId = "asd123koaskdlk",
                ProductType = Footwear.Data.Models.Enums.ProductType.DailyUse
            };

            var errors = DataAnnotationsValidators.ValidateModel(cartProduct);
            Assert.True(errors.Count == 0);
        }

        [Fact]
        public void Test_CartProduct_Name_Property_Throw_Error_When_Null()
        {
            var cartProduct = new CartProduct
            {
                Id = 1,
                Name = null,
                Price = 100,
                Details = "Adidas 11 pro sneakers lol",
                ImageUrl = "https://www.adidas.com/us/adidas-11-pro-sneakers/BQ0366_010_RS.jpg",
                Size = 42,
                Quantity = 1,
                CartId = 1,
                ProductId = 1,
                Gender = Footwear.Data.Models.Enums.Gender.Men,
                IsOrdered = true,
                OrderId = "asd123koaskdlk",
                ProductType = Footwear.Data.Models.Enums.ProductType.DailyUse
            };

            var errors = DataAnnotationsValidators.ValidateModel(cartProduct);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_CartProduct_Details_Property_Throw_Error_When_Null()
        {
            var cartProduct = new CartProduct
            {
                Id = 1,
                Name = "Abdulas",
                Price = 100,
                Details = null,
                ImageUrl = "https://www.adidas.com/us/adidas-11-pro-sneakers/BQ0366_010_RS.jpg",
                Size = 42,
                Quantity = 1,
                CartId = 1,
                ProductId = 1,
                Gender = Footwear.Data.Models.Enums.Gender.Men,
                IsOrdered = true,
                OrderId = "asd123koaskdlk",
                ProductType = Footwear.Data.Models.Enums.ProductType.DailyUse
            };

            var errors = DataAnnotationsValidators.ValidateModel(cartProduct);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_CartProduct_Details_Property_Throw_Error_When_Value_Is_Zero_Or_Negative()
        {
            var cartProduct = new CartProduct
            {
                Id = 1,
                Name = "Abdulas",
                Price = -0.4,
                Details = "Asddaldalskd",
                ImageUrl = "https://www.adidas.com/us/adidas-11-pro-sneakers/BQ0366_010_RS.jpg",
                Size = 42,
                Quantity = 1,
                CartId = 1,
                ProductId = 1,
                Gender = Footwear.Data.Models.Enums.Gender.Men,
                IsOrdered = true,
                OrderId = "asd123koaskdlk",
                ProductType = Footwear.Data.Models.Enums.ProductType.DailyUse
            };

            var errors = DataAnnotationsValidators.ValidateModel(cartProduct);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_CartProduct_Details_Property_Throw_Error_When_Value_Is_Too_Big()
        {
            var cartProduct = new CartProduct
            {
                Id = 1,
                Name = "Abdulas",
                Price = 44444444444444,
                Details = "Asddaldalskd",
                ImageUrl = "https://www.adidas.com/us/adidas-11-pro-sneakers/BQ0366_010_RS.jpg",
                Size = 42,
                Quantity = 1,
                CartId = 1,
                ProductId = 1,
                Gender = Footwear.Data.Models.Enums.Gender.Men,
                IsOrdered = true,
                OrderId = "asd123koaskdlk",
                ProductType = Footwear.Data.Models.Enums.ProductType.DailyUse
            };

            var errors = DataAnnotationsValidators.ValidateModel(cartProduct);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_CartProduct_Details_Property_Throw_Error_When_Value_Is_Too_Long()
        {
            var cartProduct = new CartProduct
            {
                Id = 1,
                Name = "Abdulas",
                Price = 100,
                Details = "loremloremloremloremloremloremloremloremloremloremloremloremloremloremloremlorem" +
                "loremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremlorem" +
                "loremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremlorem" +
                "loremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremlorem" +
                "loremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremlorem" +
                "loremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremlorem" +
                "loremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremlorem" +
                "loremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremlorem" +
                "loremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremlorem" +
                "loremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremlorem" +
                "loremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremlorem" +
                "loremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremlorem" +
                "loremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremlorem",
                ImageUrl = "https://www.adidas.com/us/adidas-11-pro-sneakers/BQ0366_010_RS.jpg",
                Size = 42,
                Quantity = 1,
                CartId = 1,
                ProductId = 1,
                Gender = Footwear.Data.Models.Enums.Gender.Men,
                IsOrdered = true,
                OrderId = "asd123koaskdlk",
                ProductType = Footwear.Data.Models.Enums.ProductType.DailyUse
            };

            var errors = DataAnnotationsValidators.ValidateModel(cartProduct);
            Assert.True(errors.Count == 1);
        }        
    }
}
