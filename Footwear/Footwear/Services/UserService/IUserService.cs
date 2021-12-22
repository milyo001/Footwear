
namespace Footwear.Services.UserService
{
    using Footwear.Data.Dto;
    using System.Threading.Tasks;

    interface IUserService
    {
        bool isUsernameInUse(string email);

        Task CreateUser(RegisterViewModel model);

    }
}
