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

        //A method for validating the data from client and register new user in the database
        [HttpPost]
        [Route("register")]
        public async Task<Object> RegisterUser(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
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

        //A method for validating the data from client and login the user, also will generate JWT token
        //For JWT token configuration go to StartUp.cs and find the service for token auth
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Check if user exists in the database
                var user = await _userManager.FindByNameAsync(model.Email);
                var passwordMatch = await _userManager.CheckPasswordAsync(user, model.Password);
                if (user != null && passwordMatch)
                {
                    //Find the user cartId and then store the cartId in the token as a claim
                    var cartId = this._db.Cart.FirstOrDefault(x => x.UserId == user.Id).Id;
    
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        //Add new Claims for the user and add encoding to the token
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("CartId", cartId.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddDays(3),
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
            else
            {
                //Model state is invalid
                return BadRequest(new { message = "Incorect input data!" });
            }
            
        }

        [HttpGet]
        [Route("getProfileData")]
        public async Task<ActionResult<UserProfileDataViewModel>> GetProfileData()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Unable to get user information. Model is not valid." });
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
