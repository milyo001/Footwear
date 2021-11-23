using System.Collections.Generic;
using System.Text.Json;
using Footwear.Data.Dto;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace server.Controllers
{
    [EnableCors("SiteCorsPolicy")]

    public class PaymentsController : Controller
    {
        public PaymentsController()
        {
            StripeConfiguration.ApiKey = "sk_test_51JvSm1EzlmwAD2nGZhL8vfUSiDteGqAYl0iKaPDbix9v9rZcfzjcOm9Kh0GgUMsXSNurIyW6T6br9dbuAlWkO74e008QjJw2YC";
        }

        

        [HttpPost("create-checkout-session")]
        public ActionResult CreateCheckoutSession([FromBody] CartProductViewModel[] items)
        {
            if(items == null)
            {
                return BadRequest(new { message = "Invalid product data!" });
            }

            var domain = "https://localhost:44365";
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