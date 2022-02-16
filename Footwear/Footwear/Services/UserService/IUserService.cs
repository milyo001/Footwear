namespace Footwear.Services.UserService
{
    using Footwear.ViewModels;
    using Footwear.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public interface IUserService
    {
        /// <summary>
        /// Check if user name is already in use
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool isUsernameInUse(string email);

        /// <summary>
        /// Create user in the database with given RegisterViewModel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IdentityResult> CreateUserAsync(RegisterViewModel model);

        /// <summary>
        /// Returns user info by given user entity
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        UserProfileDataViewModel GetUserData(User user);

        /// <summary>
        /// Update user data by given user and profile view model
        /// </summary>
        /// <param name="user"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IdentityResult> UpdateUserDataAsync(User user, ProfileUpdateViewModel model);

        /// <summary>
        /// A custom made update email method, which updates the profile username (by default email)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<IdentityResult> UpdateEmailAsync(User user, string email);
    }
}
