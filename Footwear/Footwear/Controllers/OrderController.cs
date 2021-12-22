namespace Footwear.Controllers
{
    
    using Footwear.Data;
    using Footwear.Data.Dto;
    using Footwear.Data.Models;
    using Footwear.Services.OrderService;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Stripe;

    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        private readonly IOrderService _orderService;


        public OrderController(ApplicationDbContext db, UserManager<User> userManager, IConfiguration configuration,  IOrderService orderService)
        {
            this._orderService = orderService;
            Configuration = configuration;
            StripeConfiguration.ApiKey = Configuration["ApplicationSettings:Stripe_Secret"].ToString();

        }

        [Route("create-order")]
        public ActionResult CreateOrder([FromBody] OrderViewModel order)
        {
            //Check if data is invalid or model was not bound successfully
            if (order == null || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid product data!" });
            }
            //Send the auth cookie to get userId and cartId
            var authCookie = Request.Cookies["token"];

            //Check if payment is with card so the client can handle card payment session
            var cardPayment = order.Payment == "card" ? true : false;
            this._orderService.CreateOrder(authCookie, order);
            
            return Ok( new { cardPayment });
        }
    }
}
