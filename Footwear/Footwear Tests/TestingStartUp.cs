namespace MusicStore.Test
{
    using Footwear;
    using Footwear.Data.Models;
    using Footwear.Services.TokenService;
    using Footwear_Tests.Mocks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MyTested.AspNetCore.Mvc;

    public class TestStartup : Startup
    {

        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.ReplaceSingleton<UserManager<User>, UserManagerMock>();
            services.ReplaceScoped<ITokenService, TokenServiceMock>();
        }

    }
}