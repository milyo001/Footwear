
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
    using Microsoft.AspNetCore.Http;
    using System;

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

        // Return Types

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


        // Log in
        [Fact]
        public void TestIfBadRequestIsReturnedWhenEmailIsInvalid()
        {
            var testController = new UserController(this.UserManagerService, this.TokenService,
                this.UserService, this.CartService);

            var testResult = testController.Login(new LoginViewModel { Email = "", Password = "23sdadsadad@#" });

            Assert.IsType<BadRequestObjectResult>(testResult.Result);
        }

        [Fact]
        public void TestIfBadRequestIsReturnedWhenPasswordIsInvalid()
        {
            var testController = new UserController(this.UserManagerService, this.TokenService,
                this.UserService, this.CartService);

            var testResult = testController.Login(new LoginViewModel { Email = "cindy22@test.test", Password = "" });

            Assert.IsType<BadRequestObjectResult>(testResult.Result);
        }

        // Login 
        [Fact]
        public void TestIfBadRequestIsReturnedWhenLoginViewModelIsInvalid()
        {
            var testController = new UserController(this.UserManagerService, this.TokenService,
                this.UserService, this.CartService);
            testController.ModelState.AddModelError("fakeError", "fakeMessage");

            var testResult = testController.Login(new LoginViewModel());

            Assert.IsType<BadRequestObjectResult>(testResult.Result);
        }

        [Fact]
        public void TestIfLoginWorksCorrectly()
        {
            var testUser = new User
            {
                Id = "213213kdsakd",
                UserName = "testUser@test.com"
            };

            this.UserManagerServiceMock.Setup(u => u.FindByNameAsync("testUser@test.com"))
                .Returns(Task.FromResult(testUser));

            this.UserManagerServiceMock.Setup(u => u.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            this.CartServiceMock.Setup(c => c.GetCartId(It.IsAny<string>())).Returns(1);

            this.TokenServiceMock.Setup(t => t.GenerateToken(It.IsAny<string>(), It.IsAny<int>())).Returns("testsadladkalsdklakd");

            var testController = new UserController(this.UserManagerServiceMock.Object, this.TokenService,
                this.UserService, this.CartService);

            var testResult = testController.Login(new LoginViewModel { Email = "testUser@test.com", Password = "2134123123" });

            Assert.IsType<OkObjectResult>(testResult.Result);
        }

        [Fact]
        public void TestIfLoginMethodReturnsBadRequestWhenErrorIsHit()
        {
            var testUser = new User
            {
                Id = "213213kdsakd",
                UserName = "testUser@test.com"
            };

            this.UserManagerServiceMock.Setup(u => u.FindByNameAsync("testUser@test.com"))
                .Returns(Task.FromResult(testUser));

            this.UserManagerServiceMock.Setup(u => u.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                .Returns(Task.FromResult(false));

            var testController = new UserController(this.UserManagerServiceMock.Object, this.TokenService,
                this.UserService, this.CartService);

            var testResult = testController.Login(new LoginViewModel { Email = "testUser@test.com", Password = "2134123123" });

            Assert.IsType<BadRequestObjectResult>(testResult.Result);
        }

        // Register

        [Fact]
        public void TestIfBadRequestIsReturnedWhenRegisterViewModelIsInvalid()
        {
            var testController = new UserController(this.UserManagerService, this.TokenService,

                this.UserService, this.CartService);

            testController.ModelState.AddModelError("testError", "testMessage");

            var testResult = testController.RegisterUser(new RegisterViewModel() { });

            Assert.IsType<BadRequestObjectResult>(testResult.Result);
        }

        [Fact]
        public void TestIfConflictIsReturnedWhenDupplicateUser()
        {
            this.UserServiceMock.Setup(u => u.IsUsernameInUse(It.IsAny<string>())).Returns(true);

            var testController = new UserController(this.UserManagerService, this.TokenService,

                this.UserService, this.CartService);

            var testResult = testController.RegisterUser(new RegisterViewModel() { });

            Assert.IsType<ConflictObjectResult>(testResult.Result);
        }

        [Fact]
        public void TestIfBadRequestIsReturnedWhenUnableToCreateUser()
        {
            this.UserServiceMock.Setup(u => u.IsUsernameInUse(It.IsAny<string>())).Returns(false);
            this.UserServiceMock.Setup(u => u.CreateUserAsync(It.IsAny<RegisterViewModel>()))
                .Returns(Task.FromResult(Task.FromResult(IdentityResult.Failed()).Result));

            var testController = new UserController(this.UserManagerService, this.TokenService,
                this.UserService, this.CartService);

            var testResult = testController.RegisterUser(new RegisterViewModel() { });

            Assert.IsType<BadRequestObjectResult>(testResult.Result);
        }

        [Fact]
        public void TestIfRegisterIsWorkingCorrectly()
        {
            this.UserServiceMock.Setup(u => u.IsUsernameInUse(It.IsAny<string>())).Returns(false);
            this.UserServiceMock.Setup(u => u.CreateUserAsync(It.IsAny<RegisterViewModel>()))
                .Returns(Task.FromResult(Task.FromResult(IdentityResult.Success).Result));

            var testController = new UserController(this.UserManagerService, this.TokenService,
                this.UserService, this.CartService);

            var testResult = testController.RegisterUser(new RegisterViewModel() { });

            Assert.IsType<OkObjectResult>(testResult.Result);
        }

        // GetProfileData

        [Fact]
        public void TestIfGetProfileDataIsWorkingCorrectly()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Items.Add("token", "test");

            this.TokenServiceMock.Setup(t => t.GetUserByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(new User { }));
            this.UserServiceMock.Setup(u => u.GetUserData(It.IsAny<User>()))
                .Returns(new UserProfileDataViewModel { });

            var testController = new UserController(this.UserManagerService, this.TokenService, this.UserService, this.CartService)
            {
                ControllerContext = new ControllerContext() { HttpContext = httpContext }
            };

            var testResult = testController.GetProfileData();

            Assert.IsType<ActionResult<UserProfileDataViewModel>>(testResult.Result);
        }

        // UpdateProfileData

        [Fact]
        public void TestIfUpdateUserDataBindingReturnBadRequest()
        {
            var testController = new UserController(this.UserManagerService, this.TokenService, this.UserService, this.CartService);
            testController.ModelState.AddModelError("fakeError", "fakeMessage");

            var resposne = testController.UpdateProfileData(new ProfileUpdateViewModel());
            Assert.IsType<BadRequestObjectResult>(resposne.Result);
        }

        [Fact]
        public void TestIfUpdateUserDataWorksCorrectly()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Items.Add("token", "test");

            this.TokenServiceMock.Setup(t => t.GetUserByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(new User { }));
            this.UserServiceMock.Setup(u => u.UpdateUserDataAsync(It.IsAny<User>(), It.IsAny<ProfileUpdateViewModel>()))
                .ReturnsAsync(IdentityResult.Success);

            var testController = new UserController(this.UserManagerService, this.TokenService, this.UserService, this.CartService)
            {
                ControllerContext = new ControllerContext() { HttpContext = httpContext }
            };

            var testResult = testController.UpdateProfileData(new ProfileUpdateViewModel() { Street = "test" });
            Assert.IsType<AcceptedResult>(testResult.Result);
        }

        [Fact]
        public void TestIfUpdateUserDataReturnsBadRequestWhenIdentityResultIsFalse()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Items.Add("token", "test");

            this.TokenServiceMock.Setup(t => t.GetUserByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(new User { }));
            this.UserServiceMock.Setup(u => u.UpdateUserDataAsync(It.IsAny<User>(), It.IsAny<ProfileUpdateViewModel>()))
                .ReturnsAsync(IdentityResult.Failed());

            var testController = new UserController(this.UserManagerService, this.TokenService, this.UserService, this.CartService)
            {
                ControllerContext = new ControllerContext() { HttpContext = httpContext }
            };

            var testResult = testController.UpdateProfileData(new ProfileUpdateViewModel() { Street = "test" });
            Assert.IsType<BadRequestObjectResult>(testResult.Result);
        }

        [Fact]
        public void TestIfUpdateEmailWorksCorrectly()
        {
            var testViewModel = new EmailViewModel
            {
                Email = "test@gmail.com",
                ConfirmEmail = "test@gmail.com"
            };

            var httpContext = new DefaultHttpContext();
            httpContext.Items.Add("token", "test");

            this.UserServiceMock.Setup(u => u.IsUsernameInUse(It.IsAny<string>()))
                .Returns(false);
            this.TokenServiceMock.Setup(t => t.GetUserByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new User { }));
            this.UserServiceMock.Setup(u => u.UpdateEmailAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var testController = new UserController(this.UserManagerService, this.TokenService, this.UserService, this.CartService)
            {
                ControllerContext = new ControllerContext() { HttpContext = httpContext }
            };

            var testResult = testController.UpdateEmail(testViewModel);
            Assert.IsType<AcceptedResult>(testResult.Result);
        }

        [Fact]
        public void TestIfUpdateEmailReturnsBadRequestWhenModelStateIsInvalid()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Items.Add("token", "test");

            var testController = new UserController(this.UserManagerService, this.TokenService, this.UserService, this.CartService)
            {
                ControllerContext = new ControllerContext() { HttpContext = httpContext }
            };
            testController.ModelState.AddModelError("fakeError", "fakeMessage");

            var testResult = testController.UpdateEmail(new EmailViewModel());
            Assert.IsType<BadRequestObjectResult>(testResult.Result);
        }

        [Theory]
        [InlineData("test@gmail.com", "wrongEmailTest@gmail.com")]
        [InlineData("wrongEmailTest@gmail.com", "test@gmail.com")]
        public void TestIfUpdateEmailReturnsBadRequestWhenEmailsAreNotTheSame(string email, string confEmail)
        {
            var testViewModel = new EmailViewModel
            {
                Email = email,
                ConfirmEmail = confEmail
            };

            var httpContext = new DefaultHttpContext();
            httpContext.Items.Add("token", "test");

            var testController = new UserController(this.UserManagerService, this.TokenService, this.UserService, this.CartService)
            {
                ControllerContext = new ControllerContext() { HttpContext = httpContext }
            };

            var testResult = testController.UpdateEmail(testViewModel);
            Assert.IsType<BadRequestObjectResult>(testResult.Result);
        }

        [Fact]
        public void TestIfUpdateEmailReturnsBadRequestWhenUserNameIsTaken()
        {
            var testViewModel = new EmailViewModel
            {
                Email = "test@gmail.com",
                ConfirmEmail = "test@gmail.com"
            };

            var httpContext = new DefaultHttpContext();
            httpContext.Items.Add("token", "test");

            this.UserServiceMock.Setup(u => u.IsUsernameInUse(It.IsAny<string>()))
                .Returns(true);

            var testController = new UserController(this.UserManagerService, this.TokenService, this.UserService, this.CartService)
            {
                ControllerContext = new ControllerContext() { HttpContext = httpContext }
            };

            var testResult = testController.UpdateEmail(testViewModel);
            Assert.IsType<BadRequestObjectResult>(testResult.Result);
        }


        [Fact]
        public void TestIfUpdateEmailUpdatesEmailInDatabase()
        {
            var testViewModel = new EmailViewModel
            {
                Email = "test@gmail.com",
                ConfirmEmail = "test@gmail.com"
            };

            var httpContext = new DefaultHttpContext();
            httpContext.Items.Add("token", "test");

            this.UserServiceMock.Setup(u => u.IsUsernameInUse(It.IsAny<string>()))
                .Returns(false);
            this.TokenServiceMock.Setup(t => t.GetUserByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new User { }));
            this.UserServiceMock.Setup(u => u.UpdateEmailAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed());

            var testController = new UserController(this.UserManagerService, this.TokenService, this.UserService, this.CartService)
            {
                ControllerContext = new ControllerContext() { HttpContext = httpContext }
            };

            var testResult = testController.UpdateEmail(testViewModel);
            Assert.IsType<BadRequestObjectResult>(testResult.Result);
        }

        [Fact]
        public void TestIfChangePasswordWorksCorrectly()
        {
            var passViewModel = new PasswordViewModel
            {
                Password = "123456",
                NewPassword = "1234567",
                ConfirmPassword = "1234567"
            };

            var httpContext = new DefaultHttpContext();
            httpContext.Items.Add("token", "test");

            this.TokenServiceMock.Setup(t => t.GetUserByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new User { }));
            this.UserManagerServiceMock.Setup(u => u.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));
            this.UserManagerServiceMock.Setup(u => u.ChangePasswordAsync
                (It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(Task.FromResult(IdentityResult.Success));

            var testController = new UserController(this.UserManagerService, this.TokenService, this.UserService, this.CartService)
            {
                ControllerContext = new ControllerContext() { HttpContext = httpContext }
            };

            var testResult = testController.UpdatePassword(passViewModel);

            Assert.IsType<AcceptedResult>(testResult.Result);
        }

        [Fact]
        public void TestUpdatePasswordIfBadRequestIsReturnedWhenModelStateIsInvalid()
        {

            var testController = new UserController(this.UserManagerService, this.TokenService,
                this.UserService, this.CartService);
            testController.ModelState.AddModelError("fakeError", "fakeMessage");

            var testResult = testController.UpdatePassword(new PasswordViewModel());

            Assert.IsType<BadRequestObjectResult>(testResult.Result);
        }

        [Theory]
        [InlineData("eazyPassword", "hardPa$$word_2*@", "antiBruteForcePassword#$$$@!99212312355$")]
        [InlineData("eazyPassword", "antiBruteForcePassword#$$$@!99212312355$", "hardPa$$word_2*@")]
        public void TestIfChangePassword_PasswordAndConfirmPasswordAreEqual(string pass, string newPass, string confNewPass)
        {
            var passViewModel = new PasswordViewModel
            {
                Password = pass,
                NewPassword = newPass,
                ConfirmPassword = confNewPass
            };
            
            var testController = new UserController(this.UserManagerService, this.TokenService,
                this.UserService, this.CartService) { };

            var testResult = testController.UpdatePassword(passViewModel);

            Assert.IsType<BadRequestObjectResult>(testResult.Result);
        }
    }
}
