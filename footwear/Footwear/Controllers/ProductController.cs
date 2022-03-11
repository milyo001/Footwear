namespace Footwear.Controllers
{
    using Footwear.Services.CartService;
    using Footwear.Services.ProductService;
    using Footwear.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;

        public ProductController(ICartService cartService, IProductService productService)
        {
            this._cartService = cartService;
            this._productService = productService;
        }

        /// <summary>
        /// Get all products from the database.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products = await this._productService.GetAllProductsAsync();
            return Ok(products);
        }

        /// <summary>
        /// Get a specific product by given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductDtoById(int id)
        {
            var product = await this._productService.GetProductDtoByIdAsync(id);
            return product == null ? NotFound() : product;
        }

        /// <summary>
        /// Add the selected product to a cart.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addToCart")]
        public async Task<IActionResult> AddCartProduct(AddToCartModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid product id." });

            string authToken = HttpContext.Items["token"].ToString();
            var product = await this._productService.GetProductByIdAsync(model.Id);

            if (product == null)
                return BadRequest(new { message = "Error, invalid product!" });

            await this._cartService.AddCartProductAsync(authToken, product, model.Size);
            return Ok(new { succeeded = true });
        }
    }
}
