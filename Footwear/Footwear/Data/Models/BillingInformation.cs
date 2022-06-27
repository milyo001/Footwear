namespace Footwear.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    // Model used to store information about the billing address of the user, in case it is different from 
    // the account data, which can be set after the user logs in endpoint <test.domain.com/user/userData>
    // Once the user orders items, he/she can select the default account data to be imported or use this model
    public class BillingInformation
    {
        [Required, Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required, RegularExpression("[- +()0-9]+")]
        public string Phone { get; set; }

        [Required, MaxLength(100)]
        public string Street { get; set; }

        [Required, MaxLength(50)]
        public string City { get; set; }

        [Required, MaxLength(50)]
        public string State { get; set; }

        [Required, MaxLength(50)]
        public string Country { get; set; }

        [Required, MaxLength(20)]
        public string ZipCode { get; set; }
    }
}
