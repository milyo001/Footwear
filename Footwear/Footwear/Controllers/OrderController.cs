namespace Footwear.Controllers
{
    using Footwear.Services.OrderService;
    using Footwear.Services.TokenService;
    using Footwear.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Stripe;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        private readonly IOrderService _orderService;
        private readonly ITokenService _tokenService;
        private string AuthToken => HttpContext.Items["Authorization"].ToString();


        public OrderController(IConfiguration configuration, IOrderService orderService, ITokenService tokenService)
        {
            this._orderService = orderService;
            this._tokenService = tokenService;
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

            var cardPayment = order.Payment == "card";
            await this._orderService.CreateOrderAsync(this.AuthToken, order);

            return Ok(new { cardPayment });
        }

        /// <summary>
        /// Get all the information about the delivery. Send the view model to the client.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getDeliveryInfo")]
        public async Task<ActionResult<DeliveryInfoViewModel>> GetDeliveryData()
        {
            var result = await this._orderService.GetDeliveryDataAsync();
            return result;
        }

        /// <summary>
        /// Get past and pending orders(all) for the currently logged in user.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getAllOrders")]
        public async Task<ActionResult<IEnumerable<OrderViewModel>>> GetAllOrders()
        {
            var userId = this._tokenService.GetUserId(this.AuthToken);
            IEnumerable<OrderViewModel> orders = await this._orderService.GetOrdersViewModelAsync(userId);
            return Ok(orders);
        }
    }
}
