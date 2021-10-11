namespace Footwear.Controllers
{
    using Footwear.Data;
    using Footwear.Data.Dto;
    using Footwear.Services.CartService;
    using Footwear.Services.TokenService;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ITokenService _tokenService;
        private readonly ICartService _cartService;


        public CartController(ApplicationDbContext db, ITokenService tokenService, ICartService cartService)
        {
            this._db = db;
            this._tokenService = tokenService;
            this._cartService = cartService;
        }


        [HttpGet("getCartItems")]
        public IEnumerable<CartProductViewModel> Get()
        {
            var authCookie = Request.Cookies["token"];
            var cartId = this._tokenService.GetCartId(authCookie);
            //var cart = this._db.Cart
            //    .Include(c => c.CartProducts)
            //    .FirstOrDefault(c => c.Id == cartId);

            //var products = cart.CartProducts
            //     .Select(cp => new CartProductViewModel
            //     {
            //         Id = cp.Id,
            //         ProductId = cp.ProductId,
            //         Name = cp.Name,
            //         Size = cp.Size.Value,
            //         Gender = cp.Gender.ToString(),
            //         Details = cp.Details,
            //         ImageUrl = cp.ImageUrl,
            //         Price = cp.Price,
            //         Quantity = cp.Quantity,
            //         ProductType = cp.ProductType.ToString(),
            //         CreatedOn = cp.CreatedOn.ToString()
            //     })
            //    .ToArray();

            var products = this._cartService.GetCartProducts(cartId);

            return products;
        }

        [HttpPut("increaseProductQuantity")]
        public async Task<Object> IncrementCartProductQuantity([FromBody]QuantityModel model)
        {

            var cartProductId = model.CartProductId;
            var token = model.Token;
            return Ok(new { succeeded = true });
        }




    }
}
