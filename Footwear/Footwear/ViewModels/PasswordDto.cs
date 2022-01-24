namespace Footwear.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class PasswordViewModel
    {
        [Required]
        public string Password { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}
