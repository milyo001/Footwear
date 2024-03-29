﻿namespace Footwear.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class UserProfileDataViewModel
    {
        public string Email { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required, RegularExpression("[- +()0-9]+")]
        public string Phone { get; set; }

        [Required, MaxLength(100)]
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

