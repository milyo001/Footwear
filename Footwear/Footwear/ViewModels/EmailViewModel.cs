namespace Footwear.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class EmailViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string ConfirmEmail { get; set; }
    }
}
