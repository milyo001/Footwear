namespace Footwear_Tests.Controllers
{
    using Footwear.Controllers;
    using Footwear.Services.CartService;
    using Footwear.Services.ProductService;
    using Footwear.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class ProductControllerTests
    {
        public Mock<IProductService> ProductServiceMock { get; set; }
        public Mock<ICartService> CartServiceMock { get; set; }

        public ProductControllerTests()
        {
            this.ProductServiceMock = new Mock<IProductService>();
            this.CartServiceMock = new Mock<ICartService>();
        }

        [Fact]
        public void Test_GetAllProducts_ReturnType()
        {
            var testController = new ProductController(this.CartServiceMock.Object, this.ProductServiceMock.Object);
            var result = testController.GetAllProducts();

            Assert.IsAssignableFrom<Task<ActionResult<IEnumerable<ProductDto>>>>(result);
        }

        [Fact]
        public void Test_GetProductDtoById_ReturnType()
        {
            var testController = new ProductController(this.CartServiceMock.Object, this.ProductServiceMock.Object);
            var result = testController.GetProductDtoById(1);

            Assert.IsAssignableFrom<Task<ActionResult<ProductDto>>>(result);
        }
    }
}
