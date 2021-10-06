namespace Footwear.Controllers
{
    using Footwear.Data;
    using Footwear.Data.Dto;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;

    [Route("[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _db;



        public CartController(ApplicationDbContext db)
        {
            this._db = db;
        }


        [HttpGet("getCartItems")]
        public IEnumerable<CartProductViewModel> Get()
        {
            
            var authCookie = Request.Cookies["token"];
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(authCookie);

            var cartId = Int32.Parse(token.Claims.FirstOrDefault(x => x.Type == "CartId").Value);

            var cart = this._db.Cart
                .Include(c => c.CartProducts)
                .FirstOrDefault(c => c.Id == cartId);

            
            
            var products = cart.CartProducts
                 .Select(cp => new CartProductViewModel
                 {
                     ProductId = cp.ProductId,
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
