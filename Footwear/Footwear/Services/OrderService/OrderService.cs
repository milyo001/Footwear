namespace Footwear.Services.OrderService
{
    using Footwear.Data;

    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _db;

        public OrderService(ApplicationDbContext db)
        {
            this._db = db;
        }

        public void CreateOrder(string token)
        {
            throw new System.NotImplementedException();
        }
    }
}
