
namespace Footwear.Services.OrderService
{

    using Footwear.Data.Dto;

    public interface IOrderService
    {
        void CreateOrder(string token, OrderViewModel order);

        string GetLatestAddedOrderIdAsync(string token);
    }
}
