
namespace Footwear.Services.UserService
{
    using Footwear.Data.Dto;
    using System.Threading.Tasks;

    public interface IUserService
    {
        bool isUsernameInUse(string email);

        Task CreateUserAsync(RegisterViewModel model);

    }
}
