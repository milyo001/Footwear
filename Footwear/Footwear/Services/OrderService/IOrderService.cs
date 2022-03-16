
namespace Footwear.Services.OrderService
{

    using Footwear.Data.Models;
    using Footwear.ViewModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrderService
    {
        /// <summary>
        /// Creates an order by given jwt auth token and view model.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        Task CreateOrderAsync(string token, OrderViewModel order);

        /// <summary>
        /// Will returns latest added order id by given auth token.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<string> GetLatestAddedOrderIdAsync(string token);

        /// <summary>
        /// Gets latest added order for user.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<Order> GetLatestAddedOrderAsync(string token);

        /// <summary>
        /// Finds and returns an order with given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Order> GetOrderByIdAsync(string id);

        /// <summary>
        /// Change payment status of order to paid.
        /// </summary>
        /// <param name="orderId"></param>
        void ModifyPaidOrder(string orderId);


        /// <summary>
        /// Gets all information about the delivery.
        /// </summary>
        /// <returns></returns>
        Task<DeliveryInfoViewModel> GetDeliveryDataAsync();

        /// <summary>
        /// Calculate and return the total price for all cart products.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        double GetTotalPrice(Order order);

        /// <summary>
        /// Gets delivery price.
        /// </summary>
        /// <returns></returns>
        Task<double> GetDeliveryPriceAsync();

        /// <summary>
        /// Gets the order view model by given cart.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<OrderViewModel>> GetOrdersViewModel(User user);

        /// <summary>
        /// Returns of List<CartProductViewModel> by given id.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<IEnumerable<CartProductViewModel>> GetAllOrderProductsByIdAsync(string orderId);
    }
}
