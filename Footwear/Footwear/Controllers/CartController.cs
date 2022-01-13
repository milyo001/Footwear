﻿namespace Footwear.Controllers
{
    using Footwear.Data;
    using Footwear.Data.Dto;
    using Footwear.Services.CartService;
    using Footwear.Services.TokenService;
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


        [HttpGet("getCartItems")]
        public async Task<IEnumerable<CartProductViewModel>> Get()
        {
            var authCookie = Request.Cookies["token"];

            if (authCookie == "" || authCookie == null)
            {
                throw new Exception("User is not logged in.");
            }
            var cartId = this._tokenService.GetCartId(authCookie);
            var products = this._cartService.GetCartProductsViewModel(cartId);
            return products;

        }

        [HttpPut("increaseProductQuantity")]
        public async Task<IActionResult> IncrementCartProductQuantity([FromBody] int cartProductId)
        {
            var cartProduct = await this._cartService.GetCartProductByIdAsync(cartProductId);
            if (cartProduct != null)
            {
                await this._cartService.IncreaseQuantityAsync(cartProductId);
                return Ok(new { succeeded = true });
            }
            return BadRequest("Error, modifing the data!");
        }

        [HttpPut("decreaseProductQuantity")]
        public async Task<IActionResult> DecreaseCartProductQuantity([FromBody] int cartProductId)
        {
            var cartProduct = await this._cartService.GetCartProductByIdAsync(cartProductId);

            if (cartProduct == null)
            {
                return BadRequest("Product do not exists in context");
            }
            if (cartProduct.Quantity > 1)
            {
                await this._cartService.DecreaseQuantityAsync(cartProductId);
                return Ok(new { succeeded = true });
            }

            return BadRequest("Error, modifing the data!");
        }

        [HttpPost("deleteCartProduct")]
        public async Task<IActionResult> DeleteCartProduct([FromBody] int cartProductId)
        {
            if (await this._cartService.GetCartProductByIdAsync(cartProductId) == null)
            {
                return BadRequest("Product do not exists in cart");
            }
            await this._cartService.DeleteCartProductAsync(cartProductId);

            return Ok(new { succeeded = true });
        }

        [HttpDelete("removeCartProducts")]
        public async Task<IActionResult> RemoveCartProducts()
        {
            var authCookie = Request.Cookies["token"];
            var cartId = this._tokenService.GetCartId(authCookie);
            //Change the status of cart products
            await this._cartService.ChangeOrderStateCartProductsAsync(cartId);
            return Ok();
        }


    }
}
