namespace Footwear.Services.OrderService
{
    using Footwear.Data;
    using Footwear.Data.Dto;
    using Footwear.Data.Models;
    using Footwear.Data.Models.Enums;
    using Footwear.Services.CartService;
    using Footwear.Services.TokenService;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;

    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _db;
        private readonly ITokenService _tokenService;
        private readonly ICartService _cartService;

        public OrderService(ApplicationDbContext db, ITokenService tokenService, ICartService cartService)
        {
            this._db = db;
            this._tokenService = tokenService;
            this._cartService = cartService;
        }

        public void CreateOrder(string token, OrderViewModel order)
        {
            var user = this._tokenService.GetUserByIdAsync(token).Result;

            var cartId = this._tokenService.GetCartId(token);

            if(order.Payment == "card")
            {
                order.Status = "Pending";
            }
            else if(order.Payment == "cash")
            {
                order.Status = "DeliveryCash";
            }

            var newOrder = new Data.Models.Order()
            {
                Id = Guid.NewGuid().ToString(),
                Status = (Status)Enum.Parse(typeof(Status), order.Status),
                CreatedOn = DateTime.UtcNow,
                Payment = order.Payment,
                Products = this._cartService.GetCartProducts(cartId),
                UserData = new BillingInformation
                {
                    FirstName = order.UserData.FirstName,
                    LastName = order.UserData.LastName,
                    Phone = order.UserData.Phone,
                    Street = order.UserData.Street,
                    City = order.UserData.City,
                    Country = order.UserData.Country,
                    State = order.UserData.State,
                    ZipCode = order.UserData.ZipCode
                }
            };
            //Add order to current user and update database
            user.Orders.Add(newOrder);
            this._db.SaveChanges();
        }

        public string GetLatestAddedOrderId(string token)
        {
            
        }
    }
}
