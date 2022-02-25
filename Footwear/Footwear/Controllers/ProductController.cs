namespace Footwear.Controllers
{
    using Footwear.Data;
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
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await this._productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return product;
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

            string authToken = HttpContext.Items["token"].ToString();
            var product = await this._productService.GetProductByIdAsync(model.Id);

            await this._cartService.AddCartProductAsync(authToken, product, model);
            return Ok(new { succeeded = true });
        }
    }
}
