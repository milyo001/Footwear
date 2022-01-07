namespace Footwear.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Footwear.Data;
    using Footwear.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Footwear.Data.Dto;
    using System.Threading.Tasks;
    using Footwear.Services.TokenService;
    using Microsoft.EntityFrameworkCore;
    using Footwear.Services.UserService;
    using Footwear.Services.CartService;
    using Footwear.Controllers.ErrorHandler;

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly ICartService _cartService;

        public UserController(ApplicationDbContext db, UserManager<User> userManager,
             ITokenService tokenService, IUserService userService, ICartService cartService)
        {
            this._db = db;
            this._userManager = userManager;
            this._userService = userService;
            this._tokenService = tokenService;
            this._cartService = cartService;
        }

        //A method for validating the data from client and register new user in the database
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser(RegisterViewModel model)
        {
            bool isUserDupplicate = this._userService.isUsernameInUse(model.Email);

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = IdentityErrors.InvalidData });
            }
            if (isUserDupplicate)
            {
                return BadRequest(new { message = IdentityErrors.UserIsInUse });
            }
            //Create user with blank address, user can modify his profile later and add address or modify the account information
            IdentityResult result = await this._userService.CreateUserAsync(model);

            if (!result.Succeeded)
            {
                return BadRequest(new { message = IdentityErrors.CannotRegister });
            }
            return Ok(new { succeeded = true });
        }

        //A method for validating the data from client and generate auth token, also will generate JWT token
        //For JWT token configuration go to StartUp.cs and find the service for token auth
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = IdentityErrors.InvalidData });
            }
            //Check if user exists in the database
            var user = await _userManager.FindByNameAsync(model.Email);
            var passwordMatch = await _userManager.CheckPasswordAsync(user, model.Password);
            if (user == null || !passwordMatch)
            {
                return BadRequest(new { message = IdentityErrors.InvalidUsernamePassword });
            }
            var cartId = this._cartService.GetCartId(user.Id);
            //Store userId and cartId as Claims in the token for better accesibility
            var token = this._tokenService.GenerateToken(user.Id, cartId);
            return Ok(new { token });
        }

        [HttpGet]
        [Route("getProfileData")]
        public async Task<ActionResult<UserProfileDataViewModel>> GetProfileData()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = IdentityErrors.UnableToGetUserInfo });
            }
            var authCookie = Request.Cookies["token"];
            var user = await this._tokenService.GetUserByIdAsync(authCookie);
            var userData = this._userService.GetUserData(user);
            return userData;
        }

        [HttpPut]
        [Route("updateUserProfile")]
        public async Task<IActionResult> UpdateProfileData(ProfileUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = IdentityErrors.InvalidData });
            }
            var authCookie = Request.Cookies["token"];
            var user = await this._tokenService.GetUserByIdAsync(authCookie);
            var result = await this._userService.UpdateUserDataAsync(user, model);
            if (!result.Succeeded)
            {
                return BadRequest(new { message = IdentityErrors.UnableToUpdateUserInfo});
            }
            return Ok(new { succeeded = true });
        }

        [HttpPut]
        [Route("updateEmail")]
        public async Task<IActionResult> UpdateEmail(EmailViewModel model)
        {
            var email = model.Email;
            var confEmail = model.ConfirmEmail;
            var authCookie = Request.Cookies["token"];

            if (email != confEmail || !ModelState.IsValid)
            {
                return BadRequest(new { message = IdentityErrors.InvalidData });
            }
            //Check for existing username
            var dupplicate = await this._db.Users.AnyAsync(u => u.UserName == email);
            
            if (dupplicate)
            {
                return BadRequest(new { message = IdentityErrors.EmailInUse });
            }
            
            var user = await this._tokenService.GetUserByIdAsync(authCookie);
            IdentityResult result = await this._userService.UpdateEmailAsync(user, email);

            if (!result.Succeeded)
            {
                return BadRequest(new { message = IdentityErrors.UnableToGetUserInfo });
            }

            return Ok(new { succeeded = true });
        }


        [HttpPut]
        [Route("updatePassword")]
        public async Task<IActionResult> UpdatePassword(PasswordDto model)
        {
            if (model.ConfirmPassword != model.NewPassword || model.Password == null || model.ConfirmPassword == null
                || model.NewPassword == null || model == null)
            {
                return BadRequest(new { message = IdentityErrors.InvalidData });
            }
            var authCookie = Request.Cookies["token"];
            var user = await this._tokenService.GetUserByIdAsync(authCookie);
            var token = await this._userManager.GeneratePasswordResetTokenAsync(user);
            await this._userManager.ResetPasswordAsync(user, token, model.NewPassword);
            await this._db.SaveChangesAsync();
            return Ok(new { succeeded = true });
        }
    }
}
