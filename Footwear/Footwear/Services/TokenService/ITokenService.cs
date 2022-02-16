namespace Footwear.Services.TokenService
{
    using Footwear.Data.Models;
    using System.Threading.Tasks;

    public interface ITokenService
    {
        /// <summary>
        /// Returns user id by given auth token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        string GetUserId(string token);

        /// <summary>
        /// Returns cart id by given auth token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        int GetCartId(string token);

        /// <summary>
        /// Returns user entity by given auth token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<User> GetUserByIdAsync(string token);

        /// <summary>
        /// Generate login token 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cartId"></param>
        /// <returns></returns>
        string GenerateToken(string userId, int cartId);

        /// <summary>
        /// Encrypt the JWT token to prevent the user from accessing the JWT token claims
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        string EncryptToken(string token);
    }
}
