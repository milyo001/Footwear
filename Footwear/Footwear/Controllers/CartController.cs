namespace Footwear.Controllers
{
    using Footwear.Data;
    using Footwear.Data.Dto;
    using Footwear.Services.CartService;
    using Footwear.Services.TokenService;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
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
        public async Task<IEnumerable<CartProductViewModel>> Get()
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
        public async Task<Object> IncrementCartProductQuantity([FromBody]int cartProductId)
        {
            var cartProduct = await this._cartService.GetCardProductByIdAsync(cartProductId);
            if (cartProduct != null)
            {
                this._cartService.IncreaseQuantity(cartProductId);
                return Ok(new { succeeded = true });
            }
            return BadRequest("Error, modifing the data!");
        }

        [Authorize]
        [HttpPut("decreaseProductQuantity")]
        public async Task<Object> DecreaseCartProductQuantity([FromBody] int cartProductId)
        {
            var cartProduct = await this._cartService.GetCardProductByIdAsync(cartProductId);
            if(cartProduct == null)
            {
                return BadRequest("Product do not exists in context");
            }
            if (cartProduct.Quantity > 1)
            {
                this._cartService.DecreaseQuantity(cartProductId);
                return Ok(new { succeeded = true });
            }
            return BadRequest("Error, modifing the data!");
        }

        [Authorize]
        [HttpPost("deleteCartProduct")]
        public async Task<Object> DeleteCartProduct([FromBody] int cartProductId)
        {
            if (await this._cartService.GetCardProductByIdAsync(cartProductId) == null)
            {
                return BadRequest("Product do not exists in cart");
            }
            this._cartService.DeleteCartProduct(cartProductId);

            return Ok(new { succeeded = true });
        }



    }
}
