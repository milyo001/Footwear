

namespace Footwear.Services.TokenService
{
    using Footwear.Data.Models;
    using System.Threading.Tasks;

    public interface ITokenService
    {
        int GetCartId(string token);

        Task<User> GetUserByIdAsync(string token);

    }
}
