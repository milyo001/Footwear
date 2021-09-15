
namespace Footwear.Data.Dto
{
    using Footwear.Data.Models;
    using Footwear.Data.Models.Enums;
    using System.Collections.Generic;

    public class CartProductViewModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Details { get; set; }

        public string ImageUrl { get; set; }

        public string Gender { get; set; }

        public double Price { get; set; }

        public string ProductType { get; set; }

        public int Size { get; set; }

        public int Quantity { get; set; }

        public string UserName { get; set; }

    }

}

