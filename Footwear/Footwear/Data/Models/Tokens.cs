namespace Footwear.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Tokens
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
