namespace Footwear.Data.Dto
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class OrderViewModel
    {

        //Will recieve "card" or "cash" from client
        [Required]
        public string Payment { get; set; }

        [Required]
        public string CreatedOn { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public UserProfileDataViewModel UserData { get; set; }

    }
}
