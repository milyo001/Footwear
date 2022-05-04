namespace Footwear.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        public string ImageUrl { get; set; }
    }
}
