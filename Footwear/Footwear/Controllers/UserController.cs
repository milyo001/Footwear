﻿namespace Footwear.Controllers
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

        [HttpGet]
        [Route("getProfileData")]
        public async Task<ActionResult<UserProfileDataViewModel>> GetProfileData()
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

        //[HttpPut]
        //[Route("updateProfileData")]
        //public async Task<ActionResult<UserProfileDataViewModel>> GetUpdateProfileData()
        //{
        //    var authCookie = Request.Cookies["token"];
        //    var userId = this._tokenService.GetUserByIdAsync(authCookie);
        //    var user = await this._db.Users
        //        .Where(u => u.Id == userId)
        //        .Include(a => a.Address)
        //        .FirstOrDefaultAsync();

        //    var userData = new UserProfileDataViewModel
        //    {
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        Email = user.Email,
        //        Phone = user.Phone,
        //        Street = user.Address.Street,
        //        City = user.Address.City,
        //        State = user.Address.State,
        //        Country = user.Address.Country,
        //        ZipCode = user.Address.ZipCode
        //    };
        //    return userData;
        //}
    }
}
