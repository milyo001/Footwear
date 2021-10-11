﻿
using System.ComponentModel.DataAnnotations;

namespace Footwear.Data.Dto
{
    public class CartProductViewModel
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Details { get; set; }

        public string ImageUrl { get; set; }

        public string Gender { get; set; }

        public double Price { get; set; }

        public string ProductType { get; set; }

        public int Size { get; set; }

        public int Quantity { get; set; }

        public string CreatedOn { get; set; }
    }

}

