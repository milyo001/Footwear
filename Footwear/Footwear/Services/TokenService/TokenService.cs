﻿
using Footwear.Data;
using Footwear.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Footwear.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly ApplicationDbContext _db;

        public TokenService(ApplicationDbContext db)
        {
            this._db = db;
        }


        //Get UserId from token's claims
        public string GetUserId(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var authToken = handler.ReadJwtToken(token);
            var userId = authToken.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            return userId;
        }

        //Get CartId from token's claims
        public int GetCartId(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var authToken = handler.ReadJwtToken(token);
            var cartId = Int32.Parse(authToken.Claims.FirstOrDefault(x => x.Type == "CartId").Value);
            return cartId;
        }

        //Get user by 
        public async Task<User> GetUserByIdAsync(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var authToken = handler.ReadJwtToken(token);
            var userId = authToken.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            var user = await this._db.Users
                .Where(u => u.Id == userId)
                .Include(a => a.Address)
                .FirstOrDefaultAsync();
            return user;
        }

        

    }
}
