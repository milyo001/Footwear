namespace Footwear_Tests.Models
{
    using Footwear.Data.Models;
    using System.Collections.Generic;
    using Xunit;

    public class ModelsTests
    {
        

        // Billing information

        [Fact]
        public void Test_With_Correct_Data_BillingInfo_Model()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = "Salmon Sushi Street 22",
                Country = "Bolivia",
                ZipCode = "1000",
                FirstName = "Miro",
                LastName = "Ilyovski",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 0);
        }


        [Fact]
        public void Test_BillingInfo_Model_When_State_IsNull()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = null,
                City = "Sofia",
                Street = "Salmon Sushi Street 22",
                Country = "Bolivia",
                ZipCode = "1000",
                FirstName = "Miro",
                LastName = "Ilyovski",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_BillingInfo_Model_When_State_Is_More_Than_50_Chars()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "SofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofia" +
                "SofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofia",
                City = "Sofia",
                Street = "Salmon Sushi Street 22",
                Country = "Bolivia",
                ZipCode = "1000",
                FirstName = "Miro",
                LastName = "Ilyovski",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_BillingInfo_Model_When_City_IsNull()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = null,
                Street = "Salmon Sushi Street 22",
                Country = "Bolivia",
                ZipCode = "1000",
                FirstName = "Miro",
                LastName = "Ilyovski",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_BillingInfo_Model_When_City_Is_More_Than_50_Chars()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "SofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofia" +
                "SofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofiaSofia",
                Street = "Salmon Sushi Street 22",
                Country = "Bolivia",
                ZipCode = "1000",
                FirstName = "Miro",
                LastName = "Ilyovski",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_BillingInfo_Model_When_Street_IsNull()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = null,
                Country = "Bolivia",
                ZipCode = "1000",
                FirstName = "Miro",
                LastName = "Ilyovski",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_BillingInfo_Model_When_Street_Is_More_Than_50_Chars()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = "Salmon Sushi Street 22Salmon Sushi Street 22Salmon Sushi Street " +
                "22Salmon Sushi Street 22Salmon Sushi Street 22Salmon Sushi Street 22Salmon Sushi Street 22" +
                "Salmon Sushi Street 22Salmon Sushi Street 22Salmon Sushi Street 22Salmon Sushi Street 22Salmon Sushi Street 22Salmon Sushi Street 22",
                Country = "Bolivia",
                ZipCode = "1000",
                FirstName = "Miro",
                LastName = "Ilyovski",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_BillingInfo_Model_When_Country_IsNull()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = "Bolivia",
                Country = null,
                ZipCode = "1000",
                FirstName = "Miro",
                LastName = "Ilyovski",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_BillingInfo_Model_When_Country_Is_More_Than_50_Chars()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = "Boncho Str 11",
                Country = "BulgariaBulgariaBulgariaBulgariaBulgariaBulgariaBulgariaBulgariaBulgariaBu" +
                "lgariaBulgariaBulgariaBulgariaBulgariaBulgariaBulgaria",
                ZipCode = "1000",
                FirstName = "Miro",
                LastName = "Ilyovski",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }


        [Fact]
        public void Test_BillingInfo_Model_When_ZipCode_IsNull()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = "Bolivia",
                Country = "bsad",
                ZipCode = null,
                FirstName = "Miro",
                LastName = "Ilyovski",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_BillingInfo_Model_When_ZipCode_Is_More_Than_20_Chars()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = "Boncho Str 11",
                Country = "Bulgaria",
                ZipCode = "1000000000000000000000000000000000000000000000000",
                FirstName = "Miro",
                LastName = "Ilyovski",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }


        [Fact]
        public void Test_BillingInfo_Model_When_FirstName_IsNull()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = "Bolivia",
                Country = "bsad",
                ZipCode = "1000",
                FirstName = null,
                LastName = "Ilyovski",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_BillingInfo_Model_When_FirstName_Is_More_Than_100_Chars()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = "Boncho Str 11",
                Country = "Bulgaria",
                ZipCode = "1000",
                FirstName = "Mirooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo" +
                "oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo",
                LastName = "Ilyovski",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_BillingInfo_Model_When_LastName_IsNull()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = "Bolivia",
                Country = "bsad",
                ZipCode = "1000",
                FirstName = "sadsad",
                LastName = null,
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_BillingInfo_Model_When_LastName_Is_More_Than_100_Chars()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = "Boncho Str 11",
                Country = "Bulgaria",
                ZipCode = "1000",
                FirstName = "Miroo",
                LastName = "Ilyovskiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii" +
                "iiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii",
                Phone = "08942213132"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_BillingInfo_Model_When_Phone_IsNull()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = "Bolivia",
                Country = "bsad",
                ZipCode = "1000",
                FirstName = "Miro",
                LastName = "Ilyovski",
                Phone = null
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }

        [Fact]
        public void Test_BillingInfo_Model_When_Phone_Is_NotOnlyNumbers()
        {
            var billingInformation = new BillingInformation
            {
                Id = 1,
                State = "Sofia",
                City = "Sofia",
                Street = "Boncho Str 11",
                Country = "Bulgaria",
                ZipCode = "1000",
                FirstName = "Miro",
                LastName = "Ilyovski",
                Phone = "asdasdasdsda"
            };

            var errors = DataAnnotationsValidators.ValidateModel(billingInformation);
            Assert.True(errors.Count == 1);
        }

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
