namespace Footwear.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Footwear.Data;
    using Footwear.Data.Dto;
    using System;
    using Footwear.Data.Models;
    using Footwear.Data.Models.Enums;
    using Microsoft.AspNetCore.Identity;
    using System.IdentityModel.Tokens.Jwt;

    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        public ProductController(ApplicationDbContext db, UserManager<User> userManager)
        {
            this._userManager = userManager;
            this._db = db;
        }


        [HttpGet]
        public async Task<IEnumerable<ProductDto>> Get()
        {
            IEnumerable<ProductDto> products =  this._db.Products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Details = p.Details,
                ImageUrl = p.ProductImage.ImageUrl,
                Gender = p.Gender.ToString(),
                ProductType = p.ProductType.ToString()
            })
               .ToArray();

            return products;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await this._db.Products
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Details = p.Details,
                    ImageUrl = p.ProductImage.ImageUrl,
                    Gender = p.Gender.ToString(),
                    ProductType = p.ProductType.ToString()
                })
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }


        [HttpPost]
        [Route("addToCart")]
        public async Task<Object> AddCartProduct(CartProductViewModel model)
        {
            var handler = new JwtSecurityTokenHandler();
            var headerToken = Request.Headers.FirstOrDefault(x => x.Key == "Authorization");
            //Authrization token contains string with "Bearer" as first word and the encoded string of the token as second
            var encodedToken = headerToken.Value.ToString().Split(" ")[1];
            var token = handler.ReadJwtToken(encodedToken);

            var userId = token.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            var cartProduct = new CartProduct
            {
                Name = model.Name,
                Details = model.Details,
                Size = model.Size,
                Gender = (Gender)Enum.Parse(typeof(Gender), model.Gender), //Parse from string to Enum
                ProductType = (ProductType)Enum.Parse(typeof(ProductType), model.ProductType),
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                Quantity = model.Quantity,
                CreatedOn = DateTime.Today,
                ProductId = model.ProductId
            };

            var cart = this._db.Cart
                .Where(x => x.UserId == userId)
                .Include(x => x.CartProducts)
                .FirstOrDefault();

            await CheckDupplicateProductQuantityAndName(userId, cartProduct);
        
            return Ok(new { succeeded = true });
        }

        //Check if the product name is existing and have the same size
        //in the database and change the quantity of that cartProduct, instead of adding new instance of 
        //CartProduct
        private async Task CheckDupplicateProductQuantityAndName(string userId, CartProduct cartProduct)
        {
            var cart = this._db.Cart.FirstOrDefault(x => x.UserId == userId);
            var dupplicateProduct = cart.CartProducts
                    .Where(x => x.Name == cartProduct.Name)
                    .Where(x => x.Size == cartProduct.Size)
                    .FirstOrDefault();

            if (dupplicateProduct != null)
            {
                dupplicateProduct.Quantity++;
            }
            else
            {
                cart.CartProducts.Add(cartProduct);
            }
            await this._db.SaveChangesAsync();
        }
    }
}
