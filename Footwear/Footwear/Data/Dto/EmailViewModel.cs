using System.ComponentModel.DataAnnotations;

namespace Footwear.Data.Dto
{
    public class EmailViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string ConfirmEmail { get; set; }
    }
}
