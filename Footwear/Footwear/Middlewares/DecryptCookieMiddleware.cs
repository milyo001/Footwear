
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
            var token = context.Request.Cookies["token"];
            if (!string.IsNullOrWhiteSpace(token))
            {
                context.Items.Add("token", AesOperations.DecryptToken(token));
            }

            await _next(context);
        }
    }

    public static class DecryptCookieMiddlewareExtensions
    {
        public static IApplicationBuilder UseDecryptCookieMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DecryptCookieMiddleware>();
        }
    }
}