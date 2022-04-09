namespace Footwear.Controllers
{
    using Footwear.Controllers.ErrorHandler;
    using Footwear.Services.CartService;
    using Footwear.Services.TokenService;
    using Footwear.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ICartService _cartService;
        private string AuthToken => HttpContext.Items["Authorization"].ToString();

        public CartController(ITokenService tokenService, ICartService cartService)
        {
            this._tokenService = tokenService;
            this._cartService = cartService;
        }

        /// <summary>
        /// Get a IEnumerable collection of the view models and return it to the client.
        /// </summary>
        /// <returns></returns>
        [HttpGet("getCartItems")]
        public async Task<IEnumerable<CartProductViewModel>> GetCartProductsAsync()
        {
            var cartId = this._tokenService.GetCartId(this.AuthToken);
            var products = await this._cartService.GetCartProductsViewModelAsync(cartId);
            return products;
        }

        /// <summary>
        /// Increase a cart product quantity or returns BadRequest if cart product is invalid.
        /// </summary>
        /// <param name="cartProductId"></param>
        /// <returns></returns>
        [HttpPut("increaseProductQuantity")]
        public async Task<IActionResult> IncrementCartProductQuantity([FromBody] int cartProductId)
        {
            var cartProduct = await this._cartService.GetCartProductByIdAsync(cartProductId);
            if (cartProduct == null) return BadRequest(CartErrors.ProductDoNotExists);

            await this._cartService.IncreaseQuantityAsync(cartProductId);
            return Ok(new { succeeded = true });
        }

        /// <summary>
        /// Decrease a cart product quantity if possible, otherwise returns BadRquest status code.
        /// </summary>
        /// <param name="cartProductId"></param>
        /// <returns></returns>
        [HttpPut("decreaseProductQuantity")]
        public async Task<IActionResult> DecreaseCartProductQuantity([FromBody] int cartProductId)
        {
            var cartProduct = await this._cartService.GetCartProductByIdAsync(cartProductId);

            if (cartProduct == null) return BadRequest(CartErrors.ProductDoNotExists);

            //The base validation is on the client side but the user data cannot be trusted
            if (cartProduct.Quantity <= 1) return BadRequest(CartErrors.UnableToLowerCartProductQuantity);

            await this._cartService.DecreaseQuantityAsync(cartProductId);
            return Ok(new { succeeded = true });
        }

        /// <summary>
        /// Remove cart product in the database or return BadRequest if product doesn't exist.
        /// </summary>
        /// <param name="cartProductId"></param>
        /// <returns></returns>
        [HttpPost("deleteCartProduct")]
        public async Task<IActionResult> DeleteCartProduct([FromBody] int cartProductId)
        {
            var cartProduct = await this._cartService.GetCartProductByIdAsync(cartProductId);

            if (cartProduct == null) return BadRequest(CartErrors.ProductDoNotExists);

            await this._cartService.DeleteCartProductAsync(cartProductId);
            return Ok(new { succeeded = true });
        }

        /// <summary>
        /// "Clean up" the cart by changing all cart products IsOrdered property to true. Returns status 204
        /// (No Content) if cart is empty.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("removeCartProducts")]
        public async Task<IActionResult> RemoveCartProducts()
        {
            var cartId = this._tokenService.GetCartId(this.AuthToken);
            var cart = await this._cartService.GetCartAsync(cartId);

            if (cart.CartProducts.Count <= 0) return NoContent();

            //Change the status of cart products
            await this._cartService.ChangeOrderStateCartProductsAsync(cartId);
            return Ok();
        }
    }
}
