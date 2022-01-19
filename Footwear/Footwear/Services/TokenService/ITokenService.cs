namespace Footwear.Services.TokenService
{
    using Footwear.Data.Models;
    using System.Threading.Tasks;

    public interface ITokenService
    {
        Task<string> GetTokenByIdAsync(string tokenId);
        Task<string> GetUserIdAsync(string token);
        int GetCartId(string token);
        Task<User> GetUserByIdAsync(string token);
        Task<string> GenerateTokenAsync(string userId, int cartId);
        Task<bool> TokenExistsAsync(string encodedToken);

        Task<string> GetTokenIdAsync(string encodedToken);




    }
}
