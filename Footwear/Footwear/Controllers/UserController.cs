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
    using Microsoft.Extensions.Options;
    using System.Linq;

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private readonly ApplicationSettings _appSettings;


        public UserController(ApplicationDbContext db, UserManager<User> userManager, SignInManager<User> signInManager, IOptions<ApplicationSettings> appSettings)
        {
            this._db = db;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route("register")]
        public async Task<Object> RegisterUser(RegisterViewModel model)
        {
            var user = new User()
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                Cart = new Cart { }
            };

            try
            {
                var result = await this._userManager.CreateAsync(user, model.Password);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                return BadRequest(new { message = "Username or password is incorrect." });
        }
    }
}
