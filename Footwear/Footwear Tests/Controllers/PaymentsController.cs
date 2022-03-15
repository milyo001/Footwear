

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
    using Stripe.Checkout;
    using Stripe;

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

            this.ConfigurationMock.Setup(x => x["ApplicationSettings:ClientUrl"]).Returns(MockData.TestClientUrl);

            this.OrderServiceMock.Setup(x => x.GetTotalPrice(It.IsAny<Footwear.Data.Models.Order>())).Returns(250.55);

            var testController = new PaymentsController(this.ConfigurationMock.Object, this.OrderServiceMock.Object)
            {
                ControllerContext = new ControllerContext { HttpContext = httpContext }
            };

            var result = testController.CreateCheckoutSession();

            Assert.IsType<OkObjectResult>(result.Result);
        }


        [Fact]
        public void Test_CreateCheckOutSession_Return_Value()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Items.Add("token", "test");

            // This is a public test secret API key used only for testing 
            this.ConfigurationMock.Setup(x => x["ApplicationSettings:Stripe_Secret"]).Returns(MockData.TestStripeApiKey);

            this.ConfigurationMock.Setup(x => x["ApplicationSettings:ClientUrl"]).Returns(MockData.TestClientUrl);

            this.OrderServiceMock.Setup(x => x.GetTotalPrice(It.IsAny<Footwear.Data.Models.Order>())).Returns(250.55);

            var testController = new PaymentsController(this.ConfigurationMock.Object, this.OrderServiceMock.Object)
            {
                ControllerContext = new ControllerContext { HttpContext = httpContext }
            };

            var result = testController.CreateCheckoutSession();

            Assert.IsAssignableFrom<Task<ActionResult>>(result);
        }

        [Fact]
        public void Test_If_PaymentSuccess_Is_Working_Correctly()
        {
            StripeConfiguration.ApiKey = MockData.TestStripeApiKey;

            // Create fake session
            var domain = MockData.TestClientUrl;
            var order = new Footwear.Data.Models.Order();
            double totalPrice = 15.99;

            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    Name = "Total amount for all products:",
                    Currency = "usd",
                    Amount = (long)(totalPrice * 100),
                    Quantity = 1,
                  },
                },
                PaymentMethodTypes = new List<string>
                {
                  "card",
                },
                Mode = "payment",
                SuccessUrl = domain + "/payment-success/?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = domain + "/payment-cancel",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            // Mock Objects
            var sessionServiceMock = new Mock<SessionService>();
            var sessionGetOptions = new SessionGetOptions();
            var requestOptions = new Stripe.RequestOptions();

            var httpContext = new DefaultHttpContext();
            httpContext.Items.Add("token", "test");

            // This is a public test secret API key used only for testing 
            this.ConfigurationMock.Setup(x => x["ApplicationSettings:Stripe_Secret"]).Returns(MockData.TestStripeApiKey);

            var testController = new PaymentsController(this.ConfigurationMock.Object, this.OrderServiceMock.Object)
            {
                ControllerContext = new ControllerContext { HttpContext = httpContext }
            };
            var result = testController.PaymentSuccess(session.Id);

            Assert.IsType<OkObjectResult>(result.Result);
        }


        [Fact]
        public void Test_If_PaymentSuccess_Is_Returning_BadRequest_When_Session_Id_Is_Null()
        {
            // This is a public test secret API key used only for testing 
            this.ConfigurationMock.Setup(x => x["ApplicationSettings:Stripe_Secret"]).Returns(MockData.TestStripeApiKey);

            var testController = new PaymentsController(this.ConfigurationMock.Object, this.OrderServiceMock.Object){ };
            var result = testController.PaymentSuccess(null);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        
    }
}
