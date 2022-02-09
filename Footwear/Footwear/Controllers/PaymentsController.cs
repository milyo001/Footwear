
namespace server.Controllers
{
    using Footwear.ViewModels;
    using Footwear.Services.OrderService;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Stripe;
    using Stripe.Checkout;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Threading.Tasks;


    [EnableCors("SiteCorsPolicy")]
    public class PaymentsController : Controller
    {

        private readonly IOrderService _orderService;

        public IConfiguration Configuration { get; }

        public PaymentsController(IConfiguration configuration, IOrderService orderService)
        {
            this._orderService = orderService;
            Configuration = configuration;
            StripeConfiguration.ApiKey = Configuration["ApplicationSettings:Stripe_Secret"].ToString();
        }

        [HttpGet("get-checkout-session")]
        public async Task<ActionResult> GetCheckoutSession()
        {
            string authToken = HttpContext.Items["token"].ToString();
            var latestOrder = await this._orderService.GetLatestAddedOrderAsync(authToken);   
            double totalPrice = this._orderService.GetTotalPrice(latestOrder);
            //Add delivery price to the total
            totalPrice += await this._orderService.GetDeliveryPriceAsync();
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
        public async Task<ActionResult> OrderSuccess([FromQuery] string session_id)
        {
            if (session_id == null)
            {
                return BadRequest("Session id invalid");
            }

            var sessionService = new SessionService();
            Session session = sessionService.Get(session_id);
            var paymentStatus = session.PaymentStatus;

            if (string.IsNullOrWhiteSpace(paymentStatus))
            {
                return BadRequest("Payment not successful! Card declined!");
            }

            string authToken = HttpContext.Items["token"].ToString();
            //Get latest added order and change the payment status to paid
            var orderId = await this._orderService.GetLatestAddedOrderIdAsync(authToken);
            this._orderService.ModifyPaidOrder(orderId);

            return Ok(new { paymentStatus });
        }
    }
}