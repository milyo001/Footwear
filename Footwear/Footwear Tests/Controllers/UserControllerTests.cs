
namespace Footwear_Tests.Controllers
{
    using Footwear.Controllers;
    using Moq;
    using Xunit;
    using Footwear.Data.Models;
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
        public IUserService UserService { get; set; }
        public UserManager<User> UserManagerService { get; set; }
        public ITokenService TokenService { get; set; }
        public ICartService CartService { get; set; }


        public Mock<IUserService> UserServiceMock { get; set; }
        public Mock<UserManager<User>> UserManagerServiceMock { get; set; }
        public Mock<ITokenService> TokenServiceMock { get; set; }
        public Mock<ICartService> CartServiceMock { get; set; }


        public UserControllerTests()
        {
            this.UserServiceMock = new Mock<IUserService>();
            this.UserManagerServiceMock = MockServices.MockUserService();
            this.TokenServiceMock = new Mock<ITokenService>();
            this.CartServiceMock = new Mock<ICartService>();

            // To skipp writing .Object in every test controller, used to make new instance of test controller
            this.UserService = this.UserServiceMock.Object;
            this.UserManagerService = this.UserManagerServiceMock.Object;
            this.TokenService = this.TokenServiceMock.Object;
            this.CartService = this.CartServiceMock.Object;
        }
        
        


        [Fact]
        public void TestReturnTypeOfRegisterUserMethod()
        {
            var testController = new UserController(this.UserManagerService, this.TokenService,
                this.UserService, this.CartService);
            var result = testController.RegisterUser(new RegisterViewModel { });

            Assert.IsAssignableFrom<Task<IActionResult>>(result);
        }

        [Fact]
        public void TestReturnTypeOfLoginMethod()
        {
            var testController = new UserController(this.UserManagerService, this.TokenService,
                this.UserService, this.CartService);
            var result = testController.Login(new LoginViewModel { });

            Assert.IsAssignableFrom<Task<IActionResult>>(result);
        }

        [Fact]
        public void TestReturnTypeOfGetProfileDataMethod()
        {
            var testController = new UserController(this.UserManagerService, this.TokenService,
                this.UserService, this.CartService);
            var result = testController.GetProfileData();

            Assert.IsAssignableFrom<Task<ActionResult<UserProfileDataViewModel>>>(result);
        }

        [Fact]
        public void TestReturnTypeOfUpdateProfileDataMethod()
        {
            var testController = new UserController(this.UserManagerService, this.TokenService,
                this.UserService, this.CartService);
            var result = testController.UpdateProfileData(new ProfileUpdateViewModel { });

            Assert.IsAssignableFrom<Task<IActionResult>>(result);
        }

        [Fact]
        public void TestReturnTypeOfUpdateEmailMethod()
        {
            var testController = new UserController(this.UserManagerService, this.TokenService,
                this.UserService, this.CartService);
            var result = testController.UpdateEmail(new EmailViewModel { });

            Assert.IsAssignableFrom<Task<IActionResult>>(result);
        }

        [Fact]
        public void TestReturnTypeOfUpdatePasswordMethod()
        {
            var testController = new UserController(this.UserManagerService, this.TokenService,
                this.UserService, this.CartService);
            var result = testController.UpdatePassword(new PasswordViewModel { });

            Assert.IsAssignableFrom<Task<IActionResult>>(result);
        }

        [Fact]
        public void TestIfBadRequestIsReturnedWhenEmailIsInvalid()
        {
            var testController = new UserController(this.UserManagerService, this.TokenService,
                this.UserService, this.CartService);

            var response = testController.Login(new LoginViewModel { Email = "", Password = "23sdadsadad@#" });

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public void TestIfBadRequestIsReturnedWhenPasswordIsInvalid()
        {
            var testController = new UserController(this.UserManagerService, this.TokenService,
                this.UserService, this.CartService);

            var response = testController.Login(new LoginViewModel { Email = "cindy22@test.test", Password = "" });

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public void TestIfBadRequestIsReturnedWhenLoginViewModelIsInvalid()
        {
            var testController = new UserController(this.UserManagerService, this.TokenService,
                this.UserService, this.CartService);

            var response = testController.Login(new LoginViewModel());

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public void TestIfRegisterMethodIsRegisteringUserCorrectlly()
        {

        }


        [Fact]
        public void  Test49()
        {
            var testUser = new User
            {
                Id = "213213kdsakd",
                UserName = "testUser@test.com",
                Email = "testUser@test.com",
                
            };

            this.UserManagerServiceMock.Setup(u => u.FindByNameAsync("testUser@test.com"))
                .Returns(Task.FromResult(testUser));

            var user = this.UserManagerServiceMock.Object.FindByNameAsync("testUser@test.com").Result;

            this.UserManagerServiceMock.Setup(u => u.CheckPasswordAsync(user, "12345678910"))
                .Returns(Task.FromResult(testUser));


            var testController = new UserController(this.UserManagerServiceMock.Object, this.TokenService,
                this.UserService, this.CartService);

            var response = testController.Login(new LoginViewModel { Email = "testUser@test.com", Password = "2134123123"});
            Assert.IsType<ActionResult>(response.Result);
        }


        //mock.Setup(p => p.GetEmployeebyId(1)).ReturnsAsync("JK");
        //EmployeeController emp = new EmployeeController(mock.Object);
        //string result = await emp.GetEmployeeById(1);
        //Assert.Equal("JK", result);  
    }
}
