﻿namespace Footwear.Services.OrderService
{
    using AutoMapper;
    using Footwear.Data;
    using Footwear.Data.Models;
    using Footwear.Data.Models.Enums;
    using Footwear.Services.CartService;
    using Footwear.Services.TokenService;
    using Footwear.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

        // Creates new order 
        public async Task CreateOrderAsync(string token, OrderViewModel orderViewModel)
        {
            //Get the current logged in user
            var user = await this._tokenService.GetUserByTokenAsync(token);

            //Get the user cart
            var cartId = this._tokenService.GetCartId(token);

            // Check the payment type and set the data directly in the view model
            if (orderViewModel.Payment == "card")
            {
                orderViewModel.Status = "Pending";
            }
            else if (orderViewModel.Payment == "cash")
            {
                orderViewModel.Status = "DeliveryCash";
            }

            var order = this._mapper.Map<OrderViewModel, Order>(orderViewModel);
            order.UserData = this._mapper.Map<UserProfileDataViewModel, BillingInformation>(orderViewModel.UserData);
            order.Products = await this._cartService.GetCartProductsAsync(cartId);

            // Add order to current user's orders and update database
            user.Orders.Add(order);
            this._db.SaveChanges();
        }

        // Gets the delivery data (AppData) and return it
        public async Task<DeliveryInfoViewModel> GetDeliveryDataAsync()
        {
            AppData data = await this._db.AppData.FirstOrDefaultAsync();
            var result = this._mapper.Map<DeliveryInfoViewModel>(data);
            return result;
        }

        // Get the latest added order id from the database and return it
        public async Task<string> GetLatestAddedOrderIdAsync(string token)
        {
            var user = await this._tokenService.GetUserByTokenAsync(token);
            var orderId = this._db.Orders
                .Where(x => x.UserId == user.Id)
                .OrderByDescending(o => o.CreatedOn)
                .First()
                .Id;

            return orderId;
        }

        // Get the latest added order for user from the database and return it
        // CreatedOn property MUST be with time inluded(hours and minutes) in order to find it the accurate information
        public async Task<Order> GetLatestAddedOrderAsync(string token)
        {
            var user = await this._tokenService.GetUserByTokenAsync(token);

            var order = await this._db.Orders
                .Where(x => x.UserId == user.Id)
                .Include(x => x.Products)
                .OrderByDescending(o => o.CreatedOn)
                .FirstAsync();

            return order;
        }

        // Gets and returns the order entity
        public async Task<Order> GetOrderByIdAsync(string id)
        {
            var order = await this._db.Orders
                .Include(o => o.UserData)
                .FirstOrDefaultAsync(o => o.Id == id);
                
            return order;
        }

        // Calculates and returns the total price of all cart products
        public double GetTotalPrice(Order order)
        {
            var totPrice = order.Products.Sum(p => p.Price * p.Quantity);
            return totPrice;
        }

        // Change payment status of order to paid.
        public void ModifyPaidOrder(string orderId)
        {
            var order = this.GetOrderByIdAsync(orderId).Result;
            order.Status = Status.DeliveryPaid;
            this._db.SaveChanges();
        }

        // Gets only the delivery price from the AppData model
        public async Task<double> GetDeliveryPriceAsync()
        {
            var data = await this.GetDeliveryDataAsync();
            return (double)data.DeliveryPrice;
        }

        // Returns all orders view models by given user id
        public async Task<IEnumerable<OrderViewModel>> GetOrdersViewModelAsync(string userId)
        {
            var orders = await this.GetAllOrderProductsByUserIdAsync(userId);
            List<OrderViewModel> viewModel = this._mapper.Map<List<Order>, List<OrderViewModel>>(orders);
            return viewModel;
        }

        // Returns a collection of order entity by given user id, sorted by latest added
        public async Task<List<Order>> GetAllOrderProductsByUserIdAsync(string userId)
        {
            var orders = await this._db.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.UserData)
                .Include(o => o.Products)
                .OrderByDescending(o => o.CreatedOn)
                .ToListAsync();
            return orders;
        }
    }
}
