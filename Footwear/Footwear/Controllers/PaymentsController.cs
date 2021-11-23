using System.Collections.Generic;
using System.Text.Json;
using Footwear.Data.Dto;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;

namespace server.Controllers
{
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
        public ActionResult CreateCheckoutSession([FromBody] CartProductViewModel[] items)
        {
            if(items == null || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid product data!" });
            }

            var domain = Configuration["ApplicationSettings:ClientUrl"].ToString();
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    // Provide the exact Price ID (e.g. pr_1234) of the product you want to sell
                    Price = "price_1JwvAfEzlmwAD2nGJEh9JxZv",
                    Quantity = 1,
                  },
                },
                PaymentMethodTypes = new List<string>
                {
                  "card",
                },
                Mode = "payment",
                SuccessUrl = domain + "/payment-success",
                CancelUrl = domain + "/payment-cancel",
            };
            var service = new SessionService();
            Session session = service.Create(options);
            Response.StatusCode = 200;
            Response.Headers.Add("Location", session.Url);
            var weatherForecast = new
            {
                Url = session.Url
            };

            string jsonString = JsonSerializer.Serialize(weatherForecast);

            return Ok(jsonString);
        }
    }
}