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
    using Stripe.Checkout;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public IConfiguration Configuration { get; }
        private readonly ICartService _cartService;
        private readonly ITokenService _tokenService;
        private readonly IOrderService _orderService;


        public OrderController(ApplicationDbContext db, UserManager<User> userManager, IConfiguration configuration, ICartService cartService, ITokenService tokenService, IOrderService orderService)
        {
            this._cartService = cartService;
            this._tokenService = tokenService;
            this._orderService = orderService;
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

        [Route("create-order")]
        public ActionResult CreateOrder([FromBody] OrderViewModel order)
        {
            //Check if data is invalid or model was not bound successfully
            if (order == null || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid product data!" });
            }

            var authCookie = Request.Cookies["token"];

            //Check if payment is with card so the client can handle card payment session
            var cardPayment = order.Payment == "card" ? true : false;
            this._orderService.CreateOrder(authCookie, order);
            
            
            return Ok( new { cardPayment });
        }
    }
}
