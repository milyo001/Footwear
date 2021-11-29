namespace Footwear.Controllers
{
    
    using Footwear.Data;
    using Footwear.Data.Models;
    using Footwear.Services.CartService;
    using Footwear.Services.TokenService;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Stripe;
    using Stripe.Checkout;

    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public IConfiguration Configuration { get; }
        private readonly ICartService _cartService;
        private readonly ITokenService _tokenService;


        public OrderController(ApplicationDbContext db, UserManager<User> userManager, IConfiguration configuration, ICartService cartService, ITokenService tokenService)
        {
            this._cartService = cartService;
            this._tokenService = tokenService;
            this._db = db;
            Configuration = configuration;
            StripeConfiguration.ApiKey = Configuration["ApplicationSettings:Stripe_Secret"].ToString();

        }

        //Retrieve session id from stripe api and redirect to payment successfull page, 
        //make an order and then delete current products stored in the cart!
        [HttpGet("/payment-success")]
        public ActionResult OrderSuccess([FromQuery] string session_id)
        {
            var authCookie = Request.Cookies["token"];
            var cartId = this._tokenService.GetCartId(authCookie);
            var domain = Configuration["ApplicationSettings:ClientUrl"].ToString() + "/order-completed";

            var sessionService = new SessionService();
            Session session = sessionService.Get(session_id);

            var customerService = new CustomerService();
            Customer customer = customerService.Get(session.CustomerId);

            this._cartService.DeleteCartProducts(cartId);
            return Redirect(domain);
        }


        public ActionResult CreateOrder()
        {
            
            return null;
        }
    }
}
