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
    using System.Security.Claims;
    using Microsoft.AspNetCore.Identity;

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
        public IEnumerable<ProductDto> Get()
        {
            IEnumerable<ProductDto> products = this._db.Products.Select(p => new ProductDto
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
            var user = this._db.Users.FirstOrDefaultAsync(x => x.UserName == model.UserName).Result;
            var userId = user.Id;

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
                CreatedOn = DateTime.Today
            };

            var cart = this._db.Cart.FirstOrDefaultAsync(x => x.UserId == userId).Result;

            //Check if user have an instance of Cart Model, if not create new one
            if (cart == null)
            {
                var newCart = new Cart
                {
                    UserId = userId
                };

                await this._db.Cart.AddAsync(newCart);
                await this._db.SaveChangesAsync();
            }

            cart.CartProducts.Add(cartProduct);
            await this._db.SaveChangesAsync();
                    
            return Ok(null);
        }

    }
}
