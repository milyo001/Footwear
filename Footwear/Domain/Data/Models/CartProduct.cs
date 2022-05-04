﻿namespace Footwear.Data.Models
{
    using Footwear.Data.Models.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CartProduct
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, Range(0.1, 10000)]
        public double Price { get; set; }

        [Required, MaxLength(500)]
        public string Details { get; set; }

        public string ImageUrl { get; set; }

        public int? Size { get; set; }

        //Used to filter results by type
        public Gender Gender { get; set; }

        //Used to filter results by product type
        public ProductType ProductType { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public int CartId { get; set; }

        public bool IsOrdered { get; set; }

        public string OrderId { get; set; }

    }
}