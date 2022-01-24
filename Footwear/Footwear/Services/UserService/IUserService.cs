
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

        Task<IdentityResult> UpdateUserDataAsync(User user, ProfileUpdateViewModel model);

        Task<IdentityResult> UpdateEmailAsync(User user, string email);

    }
}
