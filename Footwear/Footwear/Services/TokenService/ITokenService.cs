﻿namespace Footwear.Services.TokenService
{
    using Footwear.Data.Models;
    using System.Threading.Tasks;

    public interface ITokenService
    {
        Task<string> GetUserIdAsync(string token);
        int GetCartId(string token);
        Task<User> GetUserByIdAsync(string token);
        Task<string> GenerateTokenAsync(string userId, int cartId);





    }
}
