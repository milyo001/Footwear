namespace Footwear.Helpers
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;

    //Helper class for decoding the JWT Authorization token plus reusing purpose 
    public class TokenHandler
    {
        private JwtSecurityToken _token;

        public TokenHandler(HttpRequest request)
        {
            var handler = new JwtSecurityTokenHandler();
            var headerToken = request.Headers.FirstOrDefault(x => x.Key == "Authorization");
            //Authrization token contains string with "Bearer" as first word and the encoded string of the token as second
            var encodedToken = headerToken.Value.ToString().Split(" ")[1];
            var token = handler.ReadJwtToken(encodedToken);
            this._token = token;
        }

        public int GetCartId()
        {
            var cartId = Int32.Parse(this._token.Claims.FirstOrDefault(x => x.Type == "CartId").Value);
            return cartId;
        }

        public string GetUserId()
        {
            var cartId = this._token.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            return cartId;
        }
        
    }
}
