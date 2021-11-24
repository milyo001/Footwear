
using System.ComponentModel.DataAnnotations;

namespace Footwear.Data.Dto
{
    public class CartProductViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Details { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string ProductType { get; set; }

        [Required]
        public int Size { get; set; }

        [Required]
        public int Quantity { get; set; }
    }

}

