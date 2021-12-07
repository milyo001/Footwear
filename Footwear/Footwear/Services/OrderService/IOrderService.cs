
namespace Footwear.Services.OrderService
{

    using Footwear.Data.Dto;
    using Footwear.Data.Models;
    using System.Threading.Tasks;

    public interface IOrderService
    {
        void CreateOrder(string token, OrderViewModel order);

        Task<Order> GetLatestAddedOrderAsync(string token);
    }
}
