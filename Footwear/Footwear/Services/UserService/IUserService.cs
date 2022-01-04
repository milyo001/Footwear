
namespace Footwear.Services.UserService
{
    using Footwear.Data.Dto;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public interface IUserService
    {
        bool isUsernameInUse(string email);

        Task<IdentityResult> CreateUserAsync(RegisterViewModel model);

    }
}
