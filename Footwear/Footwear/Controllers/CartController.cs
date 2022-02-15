namespace Footwear.Controllers
{
    using Footwear.Data;
    using Footwear.ViewModels;
    using Footwear.Services.CartService;
    using Footwear.Services.TokenService;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Footwear.Controllers.ErrorHandler;

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
        public async Task<IEnumerable<CartProductViewModel>> GetItemsAsync()
        {
            string authToken = HttpContext.Items["token"].ToString();
            var cartId = this._tokenService.GetCartId(authToken);
            var products = await this._cartService.GetCartProductsViewModelAsync(cartId);
            return products;
        }

        [HttpPut("increaseProductQuantity")]
        public async Task<IActionResult> IncrementCartProductQuantity([FromBody] int cartProductId)
        {
            var cartProduct = await this._cartService.GetCartProductByIdAsync(cartProductId);
            if (cartProduct == null) return BadRequest(CartErrors.InvalidCartProduct);
            await this._cartService.IncreaseQuantityAsync(cartProductId);
            return Ok(new { succeeded = true });
        }

        /// <summary>
        /// Decrease an cart product quantity if possible, otherwise returns BadRquest status code.
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

        [HttpPost("deleteCartProduct")]
        public async Task<IActionResult> DeleteCartProduct([FromBody] int cartProductId)
        {
            var cartProduct = await this._cartService.GetCartProductByIdAsync(cartProductId);
            if (cartProduct == null) return BadRequest(CartErrors.ProductDoNotExists);

            await this._cartService.DeleteCartProductAsync(cartProductId);
            return Ok(new { succeeded = true });
        }

        [HttpDelete("removeCartProducts")]
        public async Task<IActionResult> RemoveCartProducts()
        {

            string authToken = HttpContext.Items["token"].ToString();
            var cartId = this._tokenService.GetCartId(authToken);
            var cart = await this._cartService.GetCartAsync(cartId);

            if (cart.CartProducts.Count <= 0)
            {
                return NoContent();
            }
            //Change the status of cart products
            await this._cartService.ChangeOrderStateCartProductsAsync(cartId);
            return Ok();
        }


    }
}
