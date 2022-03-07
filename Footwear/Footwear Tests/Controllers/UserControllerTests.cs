
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
    using Footwear.ViewModels;
    using Footwear_Tests.Mocks;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class UserControllerTests
    {
        public IUserService MockUserService { get; set; }
        public UserManager<User> MockUserManagerService { get; set; }
        public ITokenService MockTokenService { get; set; }
        public ICartService MockCartService { get; set; }

        public UserControllerTests()
        {
            this.MockUserService = new Mock<IUserService>().Object;
            this.MockUserManagerService = MockServices.MockUserService().Object;
            this.MockTokenService = new Mock<ITokenService>().Object;
            this.MockCartService = new Mock<ICartService>().Object;
        }
        
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


        [Fact]
        public void TestReturnTypeOfRegisterUserMethod()
        {
            var testController = new UserController(this.MockUserManagerService, this.MockTokenService,
                this.MockUserService, this.MockCartService);
            var result = testController.RegisterUser(new RegisterViewModel { });

            Assert.IsAssignableFrom<Task<IActionResult>>(result);
        }

        [Fact]
        public void TestReturnTypeOfLoginMethod()
        {
            var testController = new UserController(this.MockUserManagerService, this.MockTokenService,
                this.MockUserService, this.MockCartService);
            var result = testController.Login(new LoginViewModel { });

            Assert.IsAssignableFrom<Task<IActionResult>>(result);
        }

        [Fact]
        public void TestReturnTypeOfGetProfileDataMethod()
        {
            var testController = new UserController(this.MockUserManagerService, this.MockTokenService,
                this.MockUserService, this.MockCartService);
            var result = testController.GetProfileData();

            Assert.IsAssignableFrom<Task<ActionResult<UserProfileDataViewModel>>>(result);
        }

        [Fact]
        public void TestReturnTypeOfUpdateProfileDataMethod()
        {
            var testController = new UserController(this.MockUserManagerService, this.MockTokenService,
                this.MockUserService, this.MockCartService);
            var result = testController.UpdateProfileData(new ProfileUpdateViewModel { });

            Assert.IsAssignableFrom<Task<IActionResult>>(result);
        }

        [Fact]
        public void TestReturnTypeOfUpdateEmailMethod()
        {
            var testController = new UserController(this.MockUserManagerService, this.MockTokenService,
                this.MockUserService, this.MockCartService);
            var result = testController.UpdateEmail(new EmailViewModel { });

            Assert.IsAssignableFrom<Task<IActionResult>>(result);
        }

        [Fact]
        public void TestReturnTypeOfUpdatePasswordMethod()
        {
            var testController = new UserController(this.MockUserManagerService, this.MockTokenService,
                this.MockUserService, this.MockCartService);
            var result = testController.UpdatePassword(new PasswordViewModel { });

            Assert.IsAssignableFrom<Task<IActionResult>>(result);
        }
    }
}
