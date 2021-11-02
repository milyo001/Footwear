namespace Footwear.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Footwear.Data;
    using Footwear.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Footwear.Data.Dto;
    using System.Threading.Tasks;
    using System;
    using Microsoft.IdentityModel.Tokens;
    using System.Security.Claims;
    using System.Text;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using Footwear.Services.TokenService;
    using Microsoft.Extensions.Options;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private UserManager<User> _userManager;
        private ITokenService _tokenService;
        private readonly ApplicationSettings _appSettings;


        public UserController(ApplicationDbContext db, UserManager<User> userManager,
             ITokenService tokenService, IOptions<ApplicationSettings> appSettings)
        {
            this._db = db;
            this._userManager = userManager;
            this._tokenService = tokenService;
            this._appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route("register")]
        public async Task<Object> RegisterUser(RegisterViewModel model)
        {
            var test = ModelState.Values;
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid input data!" } );
            }
            //Check if Username already exists
            var dupplicateName = this._db.Users.Any(user => user.Email == (model.Email).ToUpper());
            if (dupplicateName)
            {
                return BadRequest(new { message = "User already exists!" });
            }
            //Create user with blank address, user can modify his profile later and add address
            var user = new User()
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                Cart = new Cart { },
                Address = new Address { City = "", Street = "", Country = "", State = "", ZipCode = "" }
            };
            
            var result = await this._userManager.CreateAsync(user, model.Password);
            return Ok(result);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            var passwordMatch = await _userManager.CheckPasswordAsync(user, model.Password);

            if (user != null && passwordMatch)
            {

                var cartId = this._db.Cart.FirstOrDefault(x => x.UserId == user.Id).Id;

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("CartId", cartId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return Ok(new { token });
            }
            else
            {
                return BadRequest(new { message = "Username or password is incorrect." });
            }
        }

        [HttpGet]
        [Route("getProfileData")]
        public async Task<ActionResult<UserProfileDataViewModel>> GetProfileData()
        {
            if (ModelState.IsValid)
            {
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
            return BadRequest(new { message = "Unable to get user information. Model is not valid." });

        }

        [HttpPut]
        [Route("updateUserProfile")]
        public async Task<IActionResult> UpdateProfileData(ProfileUpdateViewModel model)
        {


            if (ModelState.IsValid)
            {
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
            return BadRequest(new { message = "Incorrect input data." });
        }

        [HttpPut]
        [Route("updateEmail")]
        public async Task<IActionResult> UpdateEmail(EmailDto model)
        {
            var email = model.Email;
            var authCookie = Request.Cookies["token"];
            var user = await this._tokenService.GetUserByIdAsync(authCookie);
            var dupplicate = await this._db.Users.AnyAsync(u => u.UserName == email);
            if (model.Email != model.ConfirmEmail || model.Email == null || model.ConfirmEmail == null)
            {
                return BadRequest(new { message = "Incorrect input data." });
            }
            if (dupplicate)
            {
                return BadRequest(new { message = "Email already in use." });
            }
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
