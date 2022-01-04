namespace Footwear.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Footwear.Data;
    using Footwear.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Footwear.Data.Dto;
    using System.Threading.Tasks;
    using Footwear.Services.TokenService;
    using Microsoft.Extensions.Options;
    using Microsoft.EntityFrameworkCore;
    using Footwear.Services.UserService;
    using Footwear.Services.CartService;

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly ApplicationSettings _appSettings;
        private readonly IUserService _userService;
        private readonly ICartService _cartService;


        public UserController(ApplicationDbContext db, UserManager<User> userManager,
             ITokenService tokenService, IOptions<ApplicationSettings> appSettings, IUserService userService, ICartService cartService)
        {
            this._db = db;
            this._userManager = userManager;
            this._userService = userService;
            this._tokenService = tokenService;
            this._cartService = cartService;
            this._appSettings = appSettings.Value;
        }

        //A method for validating the data from client and register new user in the database
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid input data!" });
            }
            if (this._userService.isUsernameInUse(model.Email))
            {
                return BadRequest(new { message = "User already exists!" });
            }
            //Create user with blank address, user can modify his profile later and add address or modify the account information
            await this._userService.CreateUserAsync(model);
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
                return BadRequest(new { message = "Incorect input data!" });
            }
            //Check if user exists in the database
            var user = await _userManager.FindByNameAsync(model.Email);
            var passwordMatch = await _userManager.CheckPasswordAsync(user, model.Password);
            if (user == null || !passwordMatch)
            {
                return BadRequest(new { message = "Username or password is incorrect." });
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
                return BadRequest(new { message = "Unable to get user information. Invalid data!" });
            }
            var authCookie = Request.Cookies["token"];
            var user = await this._tokenService.GetUserByIdAsync(authCookie);

            var userData = new UserProfileDataViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Street = user.Address.Street,
                City = user.Address.City,
                State = user.Address.State,
                Country = user.Address.Country,
                ZipCode = user.Address.ZipCode
            };
            return userData;

        }

        [HttpPut]
        [Route("updateUserProfile")]
        public async Task<IActionResult> UpdateProfileData(ProfileUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Incorrect input data." });
            }

            var authCookie = Request.Cookies["token"];
            var user = await this._tokenService.GetUserByIdAsync(authCookie);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Phone = model.Phone;
            user.Address.Street = model.Street;
            user.Address.State = model.State;
            user.Address.City = model.City;
            user.Address.Country = model.Country;
            user.Address.ZipCode = model.ZipCode;

            await _userManager.UpdateAsync(user);

            return Ok(new { succeeded = true });
        }

        [HttpPut]
        [Route("updateEmail")]
        public async Task<IActionResult> UpdateEmail(EmailDto model)
        {
            var email = model.Email;
            var confEmail = model.ConfirmEmail;
            var authCookie = Request.Cookies["token"];
            
            //Check for existing username
            var dupplicate = await this._db.Users.AnyAsync(u => u.UserName == email);
            if (dupplicate)
            {
                return BadRequest(new { message = "Email already in use." });
            }
            if (email != confEmail || email == null || confEmail == null)
            {
                return BadRequest(new { message = "Incorrect input data." });
            }

            var user = await this._tokenService.GetUserByIdAsync(authCookie);
            user.Email = email;
            user.NormalizedEmail = email;
            user.UserName = email;
            user.NormalizedUserName = email;

            await this._db.SaveChangesAsync();
            return Ok(new { succeeded = true });
        }


        [HttpPut]
        [Route("updatePassword")]
        public async Task<IActionResult> UpdatePassword(PasswordDto model)
        {
            if (model.ConfirmPassword != model.NewPassword || model.Password == null || model.ConfirmPassword == null
                || model.NewPassword == null || model == null)
            {
                return BadRequest(new { message = "Incorrect input data." });
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
