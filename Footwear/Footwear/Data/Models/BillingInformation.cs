namespace Footwear.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    //Model used to store information about the billing address of the user, in case it is different from 
    //the account data,which can be set after the user logs in <test.domain.com/user/userData>
    public class BillingInformation
    {
        [Required, Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required, RegularExpression("[- +()0-9]+")]
        public string Phone { get; set; }

        [Required, MaxLength(20)]
        public string Street { get; set; }

        [Required, MaxLength(20)]
        public string City { get; set; }

        [Required, MaxLength(20)]
        public string State { get; set; }

        [Required, MaxLength(20)]
        public string Country { get; set; }

        [Required, MaxLength(20)]
        public string ZipCode { get; set; }
    }
}
