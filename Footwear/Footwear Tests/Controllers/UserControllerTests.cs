
namespace Footwear_Tests.Controllers
{
    using Footwear.Controllers;
    using Footwear.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using Footwear.Data.Models;
    using Microsoft.EntityFrameworkCore.InMemory;
    using System;
    using Microsoft.AspNetCore.Identity;
    using Footwear.Services.TokenService;
    using Footwear.Services.UserService;
    using Footwear.Services.CartService;

    public class UserControllerTests
    {
        public Mock<IUserStore<User>> UserStore { get; set; }
        //UserManager<User> userManager, ITokenService tokenService, IUserService userService, ICartService cartService
        public Mock<UserManager<User>>  UserManager { get; set; }

        public Mock<ITokenService> TokenService { get; set; }

        public Mock<IUserService> UserService { get; set; }

        public Mock<ICartService> CartService { get; set; }

        public UserControllerTests()
        {
            this.UserStore = new Mock<IUserStore<User>>();
            this.UserManager = new Mock<UserManager<User>>
                (this.UserStore.Object, null, null, null, null, null, null, null, null);
            this.TokenService = new Mock<ITokenService>();
            this.CartService = new Mock<ICartService>();
        }

        private static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString())
                   .UseInternalServiceProvider(serviceProvider);
            return builder.Options;
        }


        [Fact]
        public void AddressAndPaymentShouldReturnDefaultView()
            => MyMvc
        .Controller<UserController>()
        .Calling(c => c.RegisterUser(new Footwear.ViewModels.RegisterViewModel()
        {
            Email = "test@test.test",
            FirstName = "Gosho",
            LastName = "Gonsalez",
            Password = "1234567m",
            Phone = "233 311 5554"
        }))
        .ShouldReturn()
        .Ok();
    }
}
