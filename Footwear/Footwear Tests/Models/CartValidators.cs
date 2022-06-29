
namespace Footwear_Tests.Models
{
    using Footwear.Data.Models;
    using System.Collections.Generic;
    using Xunit;

    public class CartValidators
    {
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
    }
}
