

namespace Footwear.Controllers
{
    using Footwear.Data.Dto;
    using Footwear.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        public UserManager<User> _userManager;

        public UserProfileController(UserManager<User> userManager)
        {
            this._userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<Object> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            var user = await _userManager.FindByIdAsync(userId);

            return new 
            {
                user.Email,
                user.FirstName,
                user.LastName,
                user.Phone,
                user.Address
            };
        }
    }
}
