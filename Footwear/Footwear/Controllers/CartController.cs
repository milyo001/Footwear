namespace Footwear.Controllers
{
    using Footwear.Data;
    using Footwear.Data.Dto;
    using Footwear.Services.CartService;
    using Footwear.Services.TokenService;
    using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet("getCartItems")]
        public IEnumerable<CartProductViewModel> Get()
        {  
            var authCookie = Request.Cookies["token"];

            if (authCookie != "" || authCookie != null)
            {
                var cartId = this._tokenService.GetCartId(authCookie);
                var products = this._cartService.GetCartProducts(cartId);
                return products;
            }
            throw new Exception("User is not logged in.");
        }

        [Authorize]
        [HttpPut("increaseProductQuantity")]
        public async Task<Object> IncrementCartProductQuantity([FromBody]QuantityModel model)
        {
            var cartProductId = model.CartProductId;
            var token = model.Token;
            if(await this._cartService.IncrementQuantityAsync(cartProductId) != null)
            {
                return Ok(new { succeeded = true });
            }
            return BadRequest("Error, modifing the data!");
            
        }




    }
}
