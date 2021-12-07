namespace Footwear.Controllers
{
    
    using Footwear.Data;
    using Footwear.Data.Dto;
    using Footwear.Data.Models;
    using Footwear.Services.CartService;
    using Footwear.Services.OrderService;
    using Footwear.Services.TokenService;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Stripe;
    using System.Threading.Tasks;

    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public IConfiguration Configuration { get; }
        private readonly IOrderService _orderService;


        public OrderController(ApplicationDbContext db, UserManager<User> userManager, IConfiguration configuration,  IOrderService orderService)
        {
            this._orderService = orderService;
            this._db = db;
            Configuration = configuration;
            StripeConfiguration.ApiKey = Configuration["ApplicationSettings:Stripe_Secret"].ToString();

        }

        //Retrieve session id from stripe api and redirect to payment successfull page, 
        //make an order and then delete current products stored in the cart!

        //public ActionResult OrderSuccess([FromQuery] string session_id)
        //{
        //    var authCookie = Request.Cookies["token"];
        //    var cartId = this._tokenService.GetCartId(authCookie);
        //    var domain = Configuration["ApplicationSettings:ClientUrl"].ToString() + "/order-completed";

        //    var sessionService = new SessionService();
        //    Session session = sessionService.Get(session_id);

        //    var customerService = new CustomerService();
        //    Customer customer = customerService.Get(session.CustomerId);

        //    this._cartService.DeleteCartProducts(cartId);
        //    return Redirect(domain);
        //}

        [Route("create-order")]
        public async Task<ActionResult> CreateOrderAsync([FromBody] OrderViewModel order)
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
            var orderId = await this._orderService.GetLatestAddedOrderIdAsync(authCookie);
            
            return Ok( new { cardPayment, orderId  });
        }
    }
}
