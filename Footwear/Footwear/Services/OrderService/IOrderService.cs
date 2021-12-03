
namespace Footwear.Services.OrderService
{

    using Footwear.Data.Dto;
    using System.Threading.Tasks;

    public interface IOrderService
    {
        void CreateOrder(string token, OrderViewModel order);

        Task<string> GetLatestAddedOrderIdAsync(string token);
    }
}
