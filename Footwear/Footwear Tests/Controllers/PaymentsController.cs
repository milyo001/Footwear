

namespace Footwear_Tests.Controllers
{
    using Footwear.Controllers;
    using Footwear.Data.Models;
    using Footwear.Services.CartService;
    using Footwear.Services.OrderService;
    using Footwear.Services.TokenService;
    using Footwear.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Moq;
    using Footwear.Controllers;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;
    using Footwear_Tests.Mocks;

    public class PaymentsControllerTests
    {
        private Mock<IOrderService> OrderServiceMock { get; set; }
        private  Mock<IConfiguration> ConfigurationMock { get; }


        public PaymentsControllerTests()
        {
            this.OrderServiceMock = new Mock<IOrderService>();
            this.ConfigurationMock = new Mock<IConfiguration>();
        }

        [Fact]
        public void Test_If_CreateCheckoutSession_Is_Working_Correctly()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Items.Add("token", "test");

            // This is a public test secret API key used only for testing 
            this.ConfigurationMock.Setup(x => x["ApplicationSettings:Stripe_Secret"]).Returns(MockData.TestStripeApiKey);

            this.ConfigurationMock.Setup(x => x["ApplicationSettings:ClientUrl"]).Returns("https://footwear.com");

            this.OrderServiceMock.Setup(x => x.GetTotalPrice(It.IsAny<Order>())).Returns(250.55);

            var testController = new PaymentsController(this.ConfigurationMock.Object, this.OrderServiceMock.Object)
            {
                ControllerContext = new ControllerContext { HttpContext = httpContext }
            };

            var result = testController.CreateCheckoutSession();

            Assert.IsType<OkObjectResult>(result.Result);
        }

    }
}
