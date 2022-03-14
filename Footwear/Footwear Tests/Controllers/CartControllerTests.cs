

namespace Footwear_Tests.Controllers
{
    using Footwear.Controllers;
    using Footwear.Services.CartService;
    using Footwear.Services.TokenService;
    using Footwear.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class CartControllerTests
    {
        public Mock<ICartService> CartServiceMock { get; set; }
        public Mock<ITokenService> TokenServiceMock { get; set; }

        public CartControllerTests()
        {
            this.CartServiceMock = new Mock<ICartService>();
            this.TokenServiceMock = new Mock<ITokenService>();
        }

        [Fact]
        public void Test_GetCartProductsAsync_ReturnType()
        {
            var testController = new CartController(this.TokenServiceMock.Object, this.CartServiceMock.Object);
            var result = testController.GetCartProductsAsync();
            Assert.IsAssignableFrom<Task<IEnumerable<CartProductViewModel>>>(result);
        }

        [Fact]
        public void Test_IncrementCartProductQuantity_ReturnType()
        {
            var testController = new CartController(this.TokenServiceMock.Object, this.CartServiceMock.Object);
            var result = testController.IncrementCartProductQuantity(1);
            Assert.IsAssignableFrom<Task<IActionResult>>(result);
        }

        [Fact]
        public void Test_DecreaseCartProductQuantity_ReturnType()
        {
            var testController = new CartController(this.TokenServiceMock.Object, this.CartServiceMock.Object);
            var result = testController.DecreaseCartProductQuantity(1);
            Assert.IsAssignableFrom<Task<IActionResult>>(result);
        }

        [Fact]
        public void Test_DeleteCartProduct_ReturnType()
        {
            var testController = new CartController(this.TokenServiceMock.Object, this.CartServiceMock.Object);
            var result = testController.DeleteCartProduct(1);
            Assert.IsAssignableFrom<Task<IActionResult>>(result);
        }

        [Fact]
        public void Test_RemoveCartProducts_ReturnType()
        {
            var testController = new CartController(this.TokenServiceMock.Object, this.CartServiceMock.Object);
            var result = testController.RemoveCartProducts();
            Assert.IsAssignableFrom<Task<IActionResult>>(result);
        }
    }
}
