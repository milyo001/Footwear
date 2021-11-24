namespace Footwear.Controllers
{
    
    using Footwear.Data;
    using Footwear.Data.Models;
    using Footwear.Services.CartService;
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


        public OrderController(ApplicationDbContext db, UserManager<User> userManager, IConfiguration configuration, ICartService cartService)
        {
            this._db = db;
            Configuration = configuration;
            this._cartService = cartService;
            StripeConfiguration.ApiKey = Configuration["ApplicationSettings:Stripe_Secret"].ToString();

        }

        //Retrieve session id from stripe api and redirect to payment successfull page
        [HttpGet("/payment-success")]
        public ActionResult OrderSuccess([FromQuery] string session_id)
        {
            var domain = Configuration["ApplicationSettings:ClientUrl"].ToString() + "/order-completed";
            var sessionService = new SessionService();
            Session session = sessionService.Get(session_id);
            //TO DO REMOVE cart items
            var customerService = new CustomerService();
            Customer customer = customerService.Get(session.CustomerId);

            return Redirect(domain);
        }

    }
}
