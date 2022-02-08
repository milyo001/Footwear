namespace Footwear.Services.OrderService
{
    using Footwear.Data;
    using Footwear.ViewModels;
    using Footwear.Data.Models;
    using Footwear.Data.Models.Enums;
    using Footwear.Services.CartService;
    using Footwear.Services.TokenService;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;

    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _db;
        private readonly ITokenService _tokenService;
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public OrderService(ApplicationDbContext db, ITokenService tokenService, ICartService cartService, IMapper mapper)
        {
            this._db = db;
            this._tokenService = tokenService;
            this._cartService = cartService;
            this._mapper = mapper;
        }

        public async Task CreateOrderAsync(string token, OrderViewModel orderViewModel)
        {
            //Get the current logged in user
            var user = await this._tokenService.GetUserByIdAsync(token);
            //Get the user cart
            var cartId = this._tokenService.GetCartId(token);
            //Check the payment type and set the data directly in the view model
            if (orderViewModel.Payment == "card")
            {
                orderViewModel.Status = "Pending";
            }
            else if (orderViewModel.Payment == "cash")
            {
                orderViewModel.Status = "DeliveryCash";
            }

            //var billingInfo = this._mapper.Map<UserProfileDataViewModel, BillingInformation>(orderViewModel.UserData);

            var order = this._mapper.Map<OrderViewModel, Order>(orderViewModel);
            order.UserData = this._mapper.Map<UserProfileDataViewModel, BillingInformation>(orderViewModel.UserData);
            order.Products = await this._cartService.GetCartProductsAsync(cartId);

            //Add order to current user's orders and update database
            user.Orders.Add(order);
            this._db.SaveChanges();
        }

        public async Task<DeliveryInfoViewModel> GetDeliveryDataAsync()
        {
            AppData data = await this._db.AppData.FirstOrDefaultAsync();
            DeliveryInfoViewModel result = new()
            {
                MinDelivery = data.MinDelivery,
                MaxDelivery = data.MaxDelivery,
                DeliveryPrice = data.DeliveryPrice
            };
            return result;
        }

        //A method that will return the last added user order id
        public async Task<string> GetLatestAddedOrderIdAsync(string token)
        {
            var user = await this._tokenService.GetUserByIdAsync(token);
            var orderId =  this._db.Orders
                .Where(x => x.UserId == user.Id)
                .OrderByDescending(o => o.CreatedOn)
                .First()
                .Id;

            return orderId;
        }

        public async Task<Order> GetLatestAddedOrderAsync(string token)
        {
            var user = await this._tokenService.GetUserByIdAsync(token);
            var order = await this._db.Orders
                .Where(x => x.UserId == user.Id)
                .Include(x => x.Products)
                .OrderByDescending(o => o.CreatedOn)
                .FirstAsync();
                
            return order;
        }


        public async Task<Order> GetOrderByIdAsync(string id)
        {
            var order = await this._db.Orders.FirstOrDefaultAsync(o => o.Id == id);
            return order;
        }

        public double GetTotalPrice(Order order)
        {
            var totPrice = order.Products.Sum(p => p.Price * p.Quantity);
            return totPrice;
        }

        public void ModifyPaidOrder(string orderId)
        {
            var order = this.GetOrderByIdAsync(orderId).Result;
            order.Status = Status.DeliveryPaid;
            this._db.SaveChanges();
        }

        public async Task<double> GetDeliveryPriceAsync()
        {
            var data = await this.GetDeliveryDataAsync();
            return (double)data.DeliveryPrice;
        }
    }
}
