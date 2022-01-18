namespace Footwear.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Token
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string DecodeToken { get; set; }
    }
}
