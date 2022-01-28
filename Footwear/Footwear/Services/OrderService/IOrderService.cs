
namespace Footwear.Services.OrderService
{

    using Footwear.ViewModels;
    using Footwear.Data.Models;
    using System.Threading.Tasks;

    public interface IOrderService
    {
        Task CreateOrderAsync(string token, OrderViewModel order);

        Task<string> GetLatestAddedOrderIdAsync(string token);

        Task<Order> GetOrderByIdAsync(string id);

        void ModifyPaidOrder(string orderId);

        Task<DeliveryInfoViewModel> GetDeliveryDataAsync();
    }
}
