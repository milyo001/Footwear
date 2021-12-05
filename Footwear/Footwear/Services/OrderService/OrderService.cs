namespace Footwear.Services.OrderService
{
    using Footwear.Data;
    using Footwear.Data.Dto;
    using Footwear.Data.Models;
    using Footwear.Data.Models.Enums;
    using Footwear.Services.CartService;
    using Footwear.Services.TokenService;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

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

        public void CreateOrder(string token, OrderViewModel orderView)
        {
            var user = this._tokenService.GetUserByIdAsync(token).Result;

            var cartId = this._tokenService.GetCartId(token);

            if(orderView.Payment == "card")
            {
                orderView.Status = "Pending";
            }
            else if(orderView.Payment == "cash")
            {
                orderView.Status = "DeliveryCash";
            }

            var order = new Data.Models.Order()
            {
                Id = Guid.NewGuid().ToString(),
                Status = (Status)Enum.Parse(typeof(Status), orderView.Status),
                CreatedOn = DateTime.UtcNow,
                Payment = orderView.Payment,
                Products = this._cartService.GetCartProducts(cartId),
                UserData = new BillingInformation
                {
                    FirstName = orderView.UserData.FirstName,
                    LastName = orderView.UserData.LastName,
                    Phone = orderView.UserData.Phone,
                    Street = orderView.UserData.Street,
                    City = orderView.UserData.City,
                    Country = orderView.UserData.Country,
                    State = orderView.UserData.State,
                    ZipCode = orderView.UserData.ZipCode
                }
            };
            //Add order to current user and update database
            user.Orders.Add(order);
            this._db.SaveChanges();
        }

        public async Task<string> GetLatestAddedOrderIdAsync(string token)
        {
            var user = await this._tokenService.GetUserByIdAsync(token);
            var latestOrderId = user.Orders.OrderByDescending(o => o.CreatedOn).FirstOrDefault().Id;
            return latestOrderId;
        }
    }
}
