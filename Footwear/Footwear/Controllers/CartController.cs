namespace Footwear.Controllers
{
    using Footwear.Data;
    using Footwear.Data.Dto;
    using Footwear.Data.Models;
    using Footwear.Helpers;
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



        public CartController(ApplicationDbContext db)
        {
            this._db = db;
        }


        [HttpGet()]
        public IEnumerable<CartProductViewModel> Get()
        {
            var authHelper = new TokenHandler(Request);
            var cartId = authHelper.GetCartId();

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
