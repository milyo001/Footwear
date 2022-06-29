namespace Footwear_Tests.Models
{
    using Footwear.Data.Models;
    using System.Collections.Generic;
    using Xunit;
    public  class OrderValidators
    {
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
