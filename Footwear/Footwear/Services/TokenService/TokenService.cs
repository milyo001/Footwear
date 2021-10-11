
using Footwear.Data;
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

        public int GetCartId(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var authToken = handler.ReadJwtToken(token);
            var cartId = Int32.Parse(authToken.Claims.FirstOrDefault(x => x.Type == "CartId").Value);
            return cartId;
        }

        
    }
}
