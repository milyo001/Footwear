namespace Footwear.Services.OrderService
{
    using Footwear.Data;
    using Footwear.Data.Dto;
    using Footwear.Data.Models;
    using Footwear.Data.Models.Enums;
    using Footwear.Services.CartService;
    using Footwear.Services.TokenService;
    using Microsoft.EntityFrameworkCore;
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

        public void CreateOrder(string token, OrderViewModel orderViewModel)
        {
            //Get the current logged in user
            var user = this._tokenService.GetUserByIdAsync(token).Result;
            //Get the user cart
            var cartId = this._tokenService.GetCartId(token);
            //Check the payment type and set the data directly in the view model
            if(orderViewModel.Payment == "card")
            {
                orderViewModel.Status = "Pending";
            }
            else if(orderViewModel.Payment == "cash")
            {
                orderViewModel.Status = "DeliveryCash";
            }

            var order = new Data.Models.Order()
            {
                Id = Guid.NewGuid().ToString(),
                Status = (Status)Enum.Parse(typeof(Status), orderViewModel.Status),
                CreatedOn = DateTime.UtcNow,
                Payment = orderViewModel.Payment,
                Products = this._cartService.GetCartProducts(cartId),
                UserData = new BillingInformation
                {
                    FirstName = orderViewModel.UserData.FirstName,
                    LastName = orderViewModel.UserData.LastName,
                    Phone = orderViewModel.UserData.Phone,
                    Street = orderViewModel.UserData.Street,
                    City = orderViewModel.UserData.City,
                    Country = orderViewModel.UserData.Country,
                    State = orderViewModel.UserData.State,
                    ZipCode = orderViewModel.UserData.ZipCode
                }
            };
            //Add order to current user's orders and update database
            user.Orders.Add(order);
            this._db.SaveChanges();
        }

        //A method that will return the last added user order id
        public async Task<string> GetLatestAddedOrderIdAsync(string token)
        {
            var user = await this._tokenService.GetUserByIdAsync(token);
            var orderId = this._db.Orders
                .Where(x => x.UserId == user.Id)
                .OrderByDescending(o => o.CreatedOn)
                .First()
                .Id;

            return orderId;
        }


        public async Task<Order> GetOrderByIdAsync(string id)
        {
            var order = await this._db.Orders.FirstOrDefaultAsync(o => o.Id == id);
            return order;
        }

        public void ModifyPaidOrder(string orderId)
        {
            var order = this.GetOrderByIdAsync(orderId).Result;
            order.Status = Status.DeliveryPaid;
            this._db.SaveChanges();
        }
    }
}
