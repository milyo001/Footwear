namespace Footwear.Controllers
{
    
    using Footwear.Data;
    using Footwear.Data.Models;
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

        private UserManager<User> _userManager;


        public OrderController(ApplicationDbContext db, UserManager<User> userManager, IConfiguration configuration)
        {
            this._db = db;
            this._userManager = userManager;
            Configuration = configuration;
            StripeConfiguration.ApiKey = Configuration["ApplicationSettings:Stripe_Secret"].ToString();
        }


        [HttpGet("/payment-success")]
        public ActionResult OrderSuccess([FromQuery] string session_id)
        {
            var sessionService = new SessionService();
            Session session = sessionService.Get(session_id);

            var customerService = new CustomerService();
            Customer customer = customerService.Get(session.CustomerId);

            return Ok(new { succeeded = true });
        }

    }
}
