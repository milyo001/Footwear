namespace Footwear.Controllers
{
    using Footwear.Services.OrderService;
    using Footwear.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Stripe;
    using System.Threading.Tasks;

    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        private readonly IOrderService _orderService;
        public OrderController(IConfiguration configuration, IOrderService orderService)
        {
            this._orderService = orderService;
            Configuration = configuration;
            StripeConfiguration.ApiKey = Configuration["ApplicationSettings:Stripe_Secret"].ToString();
        }

        /// <summary>
        /// Creates an order and returns cardPayment property to validate if payment is with a card,
        /// so the client can handle the card payment session.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [Route("create-order")]
        public async Task<ActionResult> CreateOrder([FromBody] OrderViewModel order)
        {
            if (order == null || !ModelState.IsValid) return BadRequest(new { message = "Invalid product data!" });

            string authToken = HttpContext.Items["token"].ToString();
            var cardPayment = order.Payment == "card";
            await this._orderService.CreateOrderAsync(authToken, order);

            return Ok(new { cardPayment });
        }

        /// <summary>
        /// Get all the information about the delivery. Send the view model to the client.
        /// </summary>
        /// <returns></returns>
        [Route("getDeliveryInfo")]
        public async Task<ActionResult<DeliveryInfoViewModel>> GetDeliveryData()
        {
            var result = await this._orderService.GetDeliveryDataAsync();
            return result;
        }
    }
}
