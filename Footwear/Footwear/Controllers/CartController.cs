namespace Footwear.Controllers
{
    using Footwear.Data;
    using Footwear.Data.Dto;
    using Footwear.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        private UserManager<User> _userManager;


        public CartController(ApplicationDbContext db, UserManager<User> userManager)
        {
            this._db = db;
            this._userManager = userManager;
        }


        [HttpGet()]
        public IEnumerable<CartProductViewModel> Get()
        {
            var handler = new JwtSecurityTokenHandler();
            var headerToken = Request.Headers.FirstOrDefault(x => x.Key == "Authorization");

            //Authrization token contains string with "Bearer" as first word and the encoded string of the token as second
            var encodedToken = headerToken.Value.ToString().Split(" ")[1];
            var token = handler.ReadJwtToken(encodedToken);

            var cartId = Int32.Parse(token.Claims.FirstOrDefault(x => x.Type == "CartId").Value);

            var cart = this._db.Cart
                .Include(c => c.CartProducts)
                .FirstOrDefault(c => c.Id == cartId);

            var products = cart.CartProducts
                 .Select(cp => new CartProductViewModel
                 {
                     Name = cp.Name,
                     Size = cp.Size.Value,
                     Gender = cp.Gender.ToString(),
                     Details = cp.Details,
                     ImageUrl = cp.ImageUrl,
                     Price = cp.Price,
                     Quantity = cp.Quantity,
                     ProductType = cp.ProductType.ToString(),
                     CreatedOn = cp.CreatedOn.ToString()
                 })
                .ToArray();

            return products;
        }

       



    }
}
