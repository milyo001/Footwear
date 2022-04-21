namespace Footwear.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class OrderViewModel
    {
        public string OrderId { get; set; }

        [Required]
        public string Payment { get; set; }

        public string CreatedOn { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public UserProfileDataViewModel UserData { get; set; }

        public List<CartProductViewModel> CartProducts { get; set; } = new List<CartProductViewModel>();
    }
}
