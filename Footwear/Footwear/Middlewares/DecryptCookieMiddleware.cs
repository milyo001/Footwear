
namespace Footwear.Middlewares
{
    using Footwear.Services.TokenService;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    public class DecryptCookieMiddleware
    {
        private readonly RequestDelegate _next;

        public DecryptCookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        
        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"];

            // This check will prevent NUllReferanceExp in some controller, becase I am using 
            // getter for AuthToken
            if (!string.IsNullOrWhiteSpace(token))
            {
                context.Items.Add("Authorization", AesOperations.DecryptToken(token));
            }

            await _next(context);
        }
    }

    public static class DecryptCookieMiddlewareExtensions
    {
        /// <summary>
        /// Decrypt the token value already stored in browser cookies and store it in the dictionary<string, string> collection HttpContext.Items, where key is "token" and valure is the decrypted cookie value. With this middleware the application is protected against stolen token claims(JWT tokens are decodable and should not be exposed to the user in browser).
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseDecryptCookieMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DecryptCookieMiddleware>();
        }
    }
}