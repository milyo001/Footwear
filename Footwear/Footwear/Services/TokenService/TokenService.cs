﻿namespace Footwear.Services.TokenService
{
    using Footwear.Data;
    using Footwear.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class TokenService : ITokenService
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public TokenService(ApplicationDbContext db, IConfiguration configuration)
        {
            this._db = db;
            this._configuration = configuration;
        }

        //Encrypt the token to hide all the claims from user (JWT token is encoded, but can easily be decoded)
        public string EncryptToken(string token)
        {
            return AesOperations.EncryptToken(token);
        }

        //Get UserId from token's claims
        public string GetUserId(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var authToken = handler.ReadJwtToken(token);
            var userId = authToken.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            return userId;
        }

        // Get CartId from token's claims
        public int GetCartId(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var authToken = handler.ReadJwtToken(token);
            var cartId = Int32.Parse(authToken.Claims.FirstOrDefault(x => x.Type == "CartId").Value);
            return cartId;
        }

        public async Task<User> GetUserByTokenAsync(string token)
        {
            var userId = this.GetUserId(token);
            var user = await this._db.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Address)
                .Include(u => u.Orders)
                .FirstOrDefaultAsync();
            return user;
        }

        public string GenerateToken(string userId, int cartId)
        {
            // The secret is stored in appsetting<enviroment>.json, for more info visit https://jwt.io/
            var secret = this._configuration["ApplicationSettings:JWT_Secret"];
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Add new Claims for the user and add encoding to the token
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim("UserId", userId),
                        new Claim("CartId", cartId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var encodedToken = tokenHandler.WriteToken(securityToken);
            var encryptedToken = this.EncryptToken(encodedToken);
            return encryptedToken;
        }
    }
}
