
namespace Footwear.Controllers
{
    using Footwear.Controllers.ErrorHandler;
    using Footwear.Services.OrderService;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Stripe;
    using Stripe.Checkout;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Threading.Tasks;


    public class PaymentsController : Controller
    {

        private readonly IOrderService _orderService;

        private IConfiguration Configuration { get; }

        public PaymentsController(IConfiguration configuration, IOrderService orderService)
        {
            this._orderService = orderService;
            Configuration = configuration;
            StripeConfiguration.ApiKey = Configuration["ApplicationSettings:Stripe_Secret"].ToString();
        }

        /// <summary>
        /// Create new checkout session to prepare the client to be redirected to stripe.com for 
        /// card payment.This method will generate session URL which contains all the meta data for the session object.
        /// </summary>
        /// <returns></returns>
        [HttpGet("create-checkout-session")]
        public async Task<ActionResult> CreateCheckoutSession()
        {
            string authToken = HttpContext.Items["token"].ToString();

            // Gets the domain of the app, which is used later to generate payment-success and payment-failed URL
            var domain = Configuration["ApplicationSettings:ClientUrl"].ToString();

            // Will get the latest order(with 'isOrdered' property == false) to pay for
            var latestOrder = await this._orderService.GetLatestAddedOrderAsync(authToken);

            // Calculate the total price for all items in cart 
            double totalPrice = this._orderService.GetTotalPrice(latestOrder);

            // Add the delivery price to the total price of products
            totalPrice += await this._orderService.GetDeliveryPriceAsync();

            // Configure session options below, if you want stripe dashboard statistics use stripe price index 
            // See https://stripe.com/docs/api/prices for details
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    Name = "Total amount for all products:",
                    Currency = "usd",

                    // Stripe API expects a number as the given example:
                    // Example 1: 20 USD($) in decimal/double will equal 2000
                    // Example 2: 45.59 USD($) in decimal/double will equal 4559
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

            // Pass the url to the client, so the client can redirect user to the prebuild stripe checkout page
            var generatedUrl = new { Url = session.Url };
            string jsonString = JsonSerializer.Serialize(generatedUrl);

            return Ok(jsonString);
        }

        /// <summary>
        /// Handle a successfull payment.Make order status paid with card. Stripe API will redirect user to
        /// this route '[host]/order/payment-success/[session_id]>'
        /// </summary>
        /// <param name="session_id"></param>
        /// <returns></returns>
        [HttpGet("order/payment-success")]
        public async Task<ActionResult> PaymentSuccess([FromQuery] string session_id)
        {
            if (session_id == null) return BadRequest(PaymentErrors.InvalidSession);

            var sessionService = new SessionService();
            Session session = sessionService.Get(session_id);
            var paymentStatus = session.PaymentStatus;

            if (string.IsNullOrWhiteSpace(paymentStatus)) return BadRequest(PaymentErrors.PaymentDeclined);

            string authToken = HttpContext.Items["token"].ToString();

            // Get latest added order id
            var orderId = await this._orderService.GetLatestAddedOrderIdAsync(authToken);

            // Change payment type to paid
            this._orderService.ModifyPaidOrder(orderId);

            return Ok(new { paymentStatus });
        }
    }
}