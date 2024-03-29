﻿using Footwear.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Footwear.Data.Models.Base
{
    public abstract class ProductBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, Range(0.1, 10000)]
        public double Price { get; set; }

        [Required, MaxLength(500)]
        public string Details { get; set; }

        [ForeignKey("ProductImage")]
        public int ImageId { get; set; }

        public virtual ProductImage ProductImage { get; set; }

        // Used to filter results by type
        public Gender Gender { get; set; }

        // Used to filter results by product type
        public ProductType ProductType { get; set; }
    }
}
