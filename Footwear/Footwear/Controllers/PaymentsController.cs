
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
        public ActionResult CreateCheckoutSession([FromBody] CartProductViewModel[] items)
        {
            //Check if data is valid or model was bound successfully
            if(items == null || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid product data!" });
            }

            //The total price to charge, if you want stripe dashboard statistics use stripe price Id 
            //See https://stripe.com/docs/api/prices for details
            decimal totalPrice = 0;
            foreach (var item in items)
            {
                for (int i = 0; i < item.Quantity; i++)
                {
                    totalPrice += (decimal)item.Price;
                }
            }

            var domain = Configuration["ApplicationSettings:ClientUrl"].ToString();
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

            //Passing the url to the client to redirect user to the prebuild checkout page
            var generatedUrl = new
            {
                Url = session.Url
            };
            string jsonString = JsonSerializer.Serialize(generatedUrl);

            return Ok(jsonString);
        }
    }
}