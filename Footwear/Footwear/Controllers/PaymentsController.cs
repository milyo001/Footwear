
namespace server.Controllers
{
    using System.Collections.Generic;
    using System.Text.Json;
    using Footwear.Data.Dto;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Stripe;
    using Stripe.Checkout;


    [EnableCors("SiteCorsPolicy")]
    public class PaymentsController : Controller
    {

        public IConfiguration Configuration { get; }

        public PaymentsController(IConfiguration configuration)
        {
            Configuration = configuration;
            StripeConfiguration.ApiKey = Configuration["ApplicationSettings:Stripe_Secret"].ToString();
        }

        

        [HttpPost("create-checkout-session")]
        public ActionResult CreateCheckoutSession([FromBody] OrderViewModel order)
        {

            decimal totalPrice = 66666;
            //foreach (var item in order.Products)
            //{
            //    for (int i = 0; i < item.Quantity; i++)
            //    {
            //        totalPrice += (decimal)item.Price;
            //    }
            //}

            var domain = Configuration["ApplicationSettings:ClientUrl"].ToString();

            //The total price to charge, if you want stripe dashboard statistics use stripe price Id 
            //See https://stripe.com/docs/api/prices for details
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    Name = "Total amount for all products:",
                    Currency = "usd",
                    //Sripe api expects a number as the given example: 20$ in decimal/double = 2000 or
                    //45.50$ in decimal/double = 4550
                    Amount = (long)(totalPrice * 100),
                    Quantity = 1,
                  },
                },
                Metadata = new Dictionary<string, string>
                {
                    { "PaymentStatus", "Succeeded" }
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

            //Passing the url to the client to redirect user to the prebuild checkout page
            var generatedUrl = new
            {
                Url = session.Url
            };
            string jsonString = JsonSerializer.Serialize(generatedUrl);

            return Ok(jsonString);
        }

        [HttpGet("order/payment-success")]
        public ActionResult OrderSuccess([FromQuery] string session_id)
        {
            var sessionService = new SessionService();
            Session session = sessionService.Get(session_id);

           var paymentStatus =  session.Metadata["PaymentStatus"];
           
            var customerService = new CustomerService();
            Customer customer = customerService.Get(session.CustomerId);

            return Ok(new { paymentStatus });
        }
    }
}