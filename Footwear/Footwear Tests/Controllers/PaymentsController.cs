

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
        public void Test123 ()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Items.Add("token", "test");

            this.ConfigurationMock.Setup(x => x["ApplicationSettings:Stripe_Secret"]).Returns("klaslkdasknk_213adlaskd_213123");
            this.ConfigurationMock.Setup(x => x["ApplicationSettings:ClientUrl"]).Returns("localhost.com");

            var testController = new PaymentsController(this.ConfigurationMock.Object, this.OrderServiceMock.Object)
            {
                ControllerContext = new ControllerContext { HttpContext = httpContext }
            };

            var result = testController.CreateCheckoutSession();

            Assert.IsType<Task<ActionResult>>(result.Result);
        }

    }
}
