namespace Footwear.Data.Dto
{
    using System.Collections.Generic;

    public class OrderViewModel

    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string CreatedOn { get; set; }

        public string OrderStatus { get; set; }

        public virtual ICollection<ProductViewModel> Products { get; set; }
    }
}
