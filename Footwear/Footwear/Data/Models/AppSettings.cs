
namespace Footwear.Data.Models
{
    using Microsoft.EntityFrameworkCore;

    [Keyless]
    public class AppData
    {
        //The minimal delivery days example 1 days
        public int MinDelivery { get; set; }

        //The max delivery days, example 3 days
        public int MaxDelivery { get; set; }

        //The default price for delivery
        public decimal DeliveryPrice { get; set; }

    }
}
