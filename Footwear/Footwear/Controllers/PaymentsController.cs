using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace server.Controllers
{
    public class PaymentsController : Controller
    {
        public PaymentsController()
        {
            StripeConfiguration.ApiKey = "pk_test_51JvSm1EzlmwAD2nGbVLotJuNcdiUfqLFluPQ4g6evXT1wdlEI299uJsovNldhvcDM4zrUw17UJBxthwovQm4ZFZA00ZP95L1y6";
        }

        [HttpPost("create-checkout-session")]
        public ActionResult CreateCheckoutSession()
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                  "card",
                },
                LineItems = new List<SessionLineItemOptions>
        {
          new SessionLineItemOptions
          {
            PriceData = new SessionLineItemPriceDataOptions
            {
              UnitAmount = 2000,
              Currency = "usd",
              ProductData = new SessionLineItemPriceDataProductDataOptions
              {
                Name = "T-shirt",
              },

            },
            Quantity = 1,
          },
        },
                Mode = "payment",
                SuccessUrl = "https://example.com/success",
                CancelUrl = "https://example.com/cancel",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
    }
}