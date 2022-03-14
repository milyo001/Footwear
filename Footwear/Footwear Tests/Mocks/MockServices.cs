
namespace Footwear_Tests.Mocks
{
    using Footwear.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Moq;
    using System.Collections.Generic;

    public static class MockServices
    {
        public static Mock<UserManager<User>> MockUserService()
        {
            var userStore = new Mock<IUserStore<User>>();
            var mockUserService = new Mock<UserManager<User>>(userStore.Object, null, null, null, null, null, null, null, null);
            return mockUserService;
        }

        //public static IConfiguration MockConfiguration()
        //{
        //    var inMemorySettings = new Dictionary<string, Dictionary<string, string>>
        //    {
        //        {
        //            "ApplicationSettings:",
        //            new Dictionary<string, string> 
        //           {
        //                { "Stripe_Secret", "secret23213123" }
        //           }
        //        }
        //    };

        //    IConfiguration configuration = new ConfigurationBuilder()
        //        .AddInMemoryCollection(inMemorySettings)
        //        .Build();
        //    return configuration;
        //}

        //private static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
        //{
        //    // Create a fresh service provider, and therefore a fresh 
        //    // InMemory database instance.
        //    var serviceProvider = new ServiceCollection()
        //        .AddEntityFrameworkInMemoryDatabase()
        //        .BuildServiceProvider();

        //    // Create a new options instance telling the context to use an
        //    // InMemory database and the new service provider.
        //    var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        //    builder.UseInMemoryDatabase(Guid.NewGuid().ToString())
        //           .UseInternalServiceProvider(serviceProvider);
        //    return builder.Options;
        //}
    }
}
