namespace Footwear_Tests.Models
{
    using Footwear.Data.Models;
    using System.Collections.Generic;
    using Xunit;

    public class ModelsTests
    {
        // Cart model

        [Fact]
        public void Test_Cart_Model_If_Working_AsExpected()
        {
            var cart = new Cart
            {
                Id = 1,
                UserId = "sadwqae23918kjkclaskdj",
                CartProducts = new List<CartProduct>()
            };

            var errors = DataAnnotationsValidators.ValidateModel(cart);
            Assert.True(errors.Count == 0);
        }

        
        [Fact]
        public void Test_Cart_Model_When_UserId_IsNull()
        {
            var cart = new Cart
            {
                Id = 1,
                UserId = null,
            };

            var errors = DataAnnotationsValidators.ValidateModel(cart);
            Assert.True(errors.Count == 1);
        }

        // CartProduct model

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

        // Order model
        
        [Fact]
        public void Test_Order_Model_Works_As_Expected()
        {
            var order = new Order
            {
                Id = "sad213lxkdlk912ika",
                UserId = "2askdj92qekzlxk",
                UserData = new BillingInformation(),
                Status = Footwear.Data.Models.Enums.Status.DeliveryPaid,
                CreatedOn = System.DateTime.Today,
                Payment = "card",
                Products = new List<CartProduct>()
            };

            var errors = DataAnnotationsValidators.ValidateModel(order);
            Assert.True(errors.Count == 0);
        }

        [Fact]
        public void Test_Order_Model_Payment_Property_Throw_Error_When_Value_Is_Null()
        {
            var order = new Order
            {
                Id = "sad213lxkdlk912ika",
                UserId = "2askdj92qekzlxk",
                UserData = new BillingInformation(),
                Status = Footwear.Data.Models.Enums.Status.DeliveryPaid,
                CreatedOn = System.DateTime.Today,
                Payment = null,
                Products = new List<CartProduct>()
            };

            var errors = DataAnnotationsValidators.ValidateModel(order);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_Order_Model_UserId_Property_Throw_Error_When_Value_Is_Null()
        {
            var order = new Order
            {
                Id = "sad213lxkdlk912ika",
                UserId = null,
                UserData = new BillingInformation(),
                Status = Footwear.Data.Models.Enums.Status.DeliveryPaid,
                CreatedOn = System.DateTime.Today,
                Payment = "card",
                Products = new List<CartProduct>()
            };

            var errors = DataAnnotationsValidators.ValidateModel(order);
            Assert.True(errors.Count == 1);
        }


        [Fact]
        public void Test_Order_Model_UserData_Property_Throw_Error_When_Value_Is_Null()
        {
            var order = new Order
            {
                Id = "sad213lxkdlk912ika",
                UserId = "sadoi129u3kzjxkd",
                UserData = null,
                Status = Footwear.Data.Models.Enums.Status.DeliveryPaid,
                CreatedOn = System.DateTime.Today,
                Payment = "card",
                Products = new List<CartProduct>()
            };

            var errors = DataAnnotationsValidators.ValidateModel(order);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_Order_Model_Products_Property_Throw_Error_When_Value_Is_Null()
        {
            var order = new Order
            {
                Id = "sad213lxkdlk912ika",
                UserId = "sadoi129u3kzjxkd",
                UserData = new BillingInformation(),
                Status = Footwear.Data.Models.Enums.Status.DeliveryPaid,
                CreatedOn = System.DateTime.Today,
                Payment = "card",
                Products = null
            };

            var errors = DataAnnotationsValidators.ValidateModel(order);
            Assert.True(errors.Count == 1);
        }
    }
}
