namespace Footwear.Controllers
{
    using Footwear.Data;
    using Footwear.ViewModels;
    using Footwear.Services.CartService;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Footwear.Services.ProductService;

    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;

        public ProductController(ApplicationDbContext db, ICartService cartService, IProductService productService)
        {
            this._db = db;
            this._cartService = cartService;
            this._productService = productService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products = await this._productService.GetAllProductsAsync();
            return Ok(products);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product =  await this._productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }


        [HttpPost]
        [Route("addToCart")]
        public async Task<IActionResult> AddCartProduct(CartProductViewModel model)
        {
            string authToken = HttpContext.Items["token"].ToString();
            await this._cartService.AddCartProductAsync(authToken, model);
            return Ok(new { succeeded = true });
        }
    }
}
