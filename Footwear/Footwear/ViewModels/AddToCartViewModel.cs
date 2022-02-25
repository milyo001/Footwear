namespace Footwear.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class AddToCartModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Size { get; set; }
    }
}
