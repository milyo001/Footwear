
namespace Footwear.Services.UserService
{
    using Footwear.ViewModels;
    using Footwear.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public interface IUserService
    {
        bool isUsernameInUse(string email);

        Task<IdentityResult> CreateUserAsync(RegisterViewModel model);

        UserProfileDataViewModel GetUserData(User user);

        /// <summary>
        /// Update user data by given user and profile view model
        /// </summary>
        /// <param name="user"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IdentityResult> UpdateUserDataAsync(User user, ProfileUpdateViewModel model);

        /// <summary>
        /// Using custom update email method, instead of user manager build in one, which updates only the email and  normalized email, by default application uses email as username
        /// </summary>
        Task<IdentityResult> UpdateEmailAsync(User user, string email);

    }
}
