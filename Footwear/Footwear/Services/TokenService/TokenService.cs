namespace Footwear.Services.TokenService
{
    using Footwear.Data;
    using Footwear.Data.Models;
    using Microsoft.EntityFrameworkCore;
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
        private readonly ApplicationSettings _appSettings;

        public TokenService(ApplicationDbContext db, IOptions<ApplicationSettings> appSettings)
        {
            this._db = db;
            this._appSettings = appSettings.Value;
        }

        public async Task<string> GetTokenByIdAsync(string tokenId)
        {
            var token = await this._db.Tokens.FirstOrDefaultAsync(t => t.Id == tokenId);
            if (token == null)
            {
                throw new SecurityTokenInvalidSignatureException();
            }
            return token.EncodedToken;

        }
        //Get UserId from token's claims
        public async Task<string> GetUserIdAsync(string tokenId)
        {
            var token = await this.GetTokenByIdAsync(tokenId);
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

        public async Task<User> GetUserByIdAsync(string tokenId)
        {
            var userId = await this.GetUserIdAsync(tokenId);
            var user = await this._db.Users
                .Where(u => u.Id == userId)
                .Include(a => a.Address)
                .FirstOrDefaultAsync();
            return user;
        }
        
        public async Task<string> GenerateTokenAsync(string userId, int cartId)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Add new Claims for the user and add encoding to the token
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim("UserId", userId),
                        new Claim("CartId", cartId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
            };


            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var encodedToken = tokenHandler.WriteToken(securityToken);

            if (await this.TokenExistsAsync(encodedToken))
            {
                var tokenId = await this.GetTokenIdAsync(encodedToken); ;
                return tokenId;
            }
            var token = new Token()
            {
                Id = Guid.NewGuid().ToString(),
                EncodedToken = encodedToken
            };
            this._db.Tokens.Add(token);
            await this._db.SaveChangesAsync();
            return token.Id;
        }

        public async Task<bool> TokenExistsAsync(string encodedToken)
        {
            return await this._db.Tokens.AnyAsync(t => t.EncodedToken == encodedToken);
        }

        public async Task<string> GetTokenIdAsync(string encodedToken)
        {
            var token = await this._db.Tokens.FirstOrDefaultAsync(t => t.EncodedToken == encodedToken);
            return token.Id;
        }
    }
}
