namespace Footwear.Controllers
{
    using Footwear.Controllers.ErrorHandler;
    using Footwear.Data.Models;
    using Footwear.Services.CartService;
    using Footwear.Services.TokenService;
    using Footwear.Services.UserService;
    using Footwear.ViewModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly ICartService _cartService;
        private string AuthToken { get => HttpContext.Items["token"].ToString(); }

        public UserController(UserManager<User> userManager, ITokenService tokenService, IUserService userService, ICartService cartService)
        {
            this._userManager = userManager;
            this._userService = userService;
            this._tokenService = tokenService;
            this._cartService = cartService;
        }

        /// <summary>
        /// A method for register a new user in the database after validation the data send from the model.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(new { message = IdentityErrors.InvalidData });

            bool isUserDupplicate = this._userService.IsUsernameInUse(model.Email);

            if (isUserDupplicate) return Conflict(new { message = IdentityErrors.UserIsInUse });

            // Create user with blank address, user can modify his profile later and add address or modify the account information the Account page
            IdentityResult result = await this._userService.CreateUserAsync(model);

            if (!result.Succeeded) return BadRequest(new { message = IdentityErrors.CannotRegister });
            return Ok(new { succeeded = true });
        }

        /// <summary>
        /// ValidatE the data from the client and generate auth token.
        /// For JWT token configuration go to StartUp.cs and find the service for the token auth
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(new { message = IdentityErrors.InvalidData });

            //Check if user exists in the database
            var user = await _userManager.FindByNameAsync(model.Email);
            var passwordMatch = await _userManager.CheckPasswordAsync(user, model.Password);

            if (user == null || !passwordMatch)
                return BadRequest(new { message = IdentityErrors.InvalidUsernamePassword });

            var cartId = this._cartService.GetCartId(user.Id);

            var token = this._tokenService.GenerateToken(user.Id, cartId);

            return Ok(new { token });
        }


        /// <summary>
        /// Gets user profile data from database and send the view model to the client.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getProfileData")]
        public async Task<ActionResult<UserProfileDataViewModel>> GetProfileData()
        {
            var user = await this._tokenService.GetUserByTokenAsync(this.AuthToken);
            var userData = this._userService.GetUserData(user);
            return userData;
        }

        /// <summary>
        /// Updates user data from input data after few validations.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateUserProfile")]
        public async Task<IActionResult> UpdateProfileData(ProfileUpdateViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(new { message = IdentityErrors.InvalidData });

            var user = await this._tokenService.GetUserByTokenAsync(this.AuthToken);
            IdentityResult result = await this._userService.UpdateUserDataAsync(user, model);

            if (!result.Succeeded)
                return BadRequest(new { message = IdentityErrors.UnableToUpdateUserInfo });

            return Accepted(new { succeeded = true });
        }

        /// <summary>
        /// Updates user email and and return status 202(Accepted) when succeeded. Validation errors will return status 400(BadRequest).
        /// status.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateEmail")]
        public async Task<IActionResult> UpdateEmail(EmailViewModel model)
        {
            var email = model.Email;
            var confEmail = model.ConfirmEmail;

            if (!ModelState.IsValid)
                return BadRequest(new { message = IdentityErrors.InvalidData });

            if (email != confEmail)
                return BadRequest(new { message = IdentityErrors.EmailAndConfEmailAreNotTheSame });

            if (this._userService.IsUsernameInUse(email))
                return BadRequest(new { message = IdentityErrors.EmailInUse });

            var user = await this._tokenService.GetUserByTokenAsync(this.AuthToken);
            IdentityResult result = await this._userService.UpdateEmailAsync(user, email);

            if (!result.Succeeded)
                return BadRequest(new { message = IdentityErrors.UnableToUpdateEmail });

            return Accepted(new { succeeded = true });
        }

        /// <summary>
        /// Updates user password and returns status 202(Accepted) when succeeded. Validation errors will return status
        /// 400(BadRequest).
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updatePassword")]
        public async Task<IActionResult> UpdatePassword(PasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = IdentityErrors.InvalidData });

            if (model.NewPassword != model.ConfirmPassword)
                return BadRequest(new { message = IdentityErrors.PasswordsNotMatch });

            var user = await this._tokenService.GetUserByTokenAsync(this.AuthToken);

            var isPassValid = await this._userManager.CheckPasswordAsync(user, model.Password);

            if (!isPassValid)
                return BadRequest(new { message = IdentityErrors.InvalidPassword });

            IdentityResult result = await this._userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);

            if (!result.Succeeded)
                return BadRequest(new { message = IdentityErrors.UnableToUpdatePassword });

            return Accepted(new { succeeded = true });
        }
    }
}
