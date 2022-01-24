﻿namespace Footwear.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class OrderViewModel
    {
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