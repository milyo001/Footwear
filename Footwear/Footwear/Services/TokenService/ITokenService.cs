namespace Footwear.Services.TokenService
{
    using Footwear.Data.Models;
    using System.Threading.Tasks;

    public interface ITokenService
    {
        string GetUserId(string token);
        int GetCartId(string token);
        Task<User> GetUserByIdAsync(string token);
        string GenerateToken(string userId, int cartId);
        string EncryptToken(string token);
        string DecryptToken(string token);






    }
}
