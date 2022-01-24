﻿namespace Footwear.Controllers
{
    using Footwear.Data;
    using Footwear.ViewModels;
    using Footwear.Data.Models;
    using Footwear.Services.CartService;
    using Footwear.Services.TokenService;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly ICartService _cartService;
        private readonly ITokenService _tokenService;

        public ProductController(ApplicationDbContext db, UserManager<User> userManager, ICartService cartService,
            ITokenService tokenService)
        {
            this._userManager = userManager;
            this._db = db;
            this._cartService = cartService;
            this._tokenService = tokenService;
        }


        [HttpGet]
        public async Task<IEnumerable<ProductDto>> Get()
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
        public async Task<IActionResult> AddCartProduct(CartProductViewModel model)
        {
            string authToken = HttpContext.Items["token"].ToString();
            await this._cartService.AddCartProductAsync(authToken, model);
            return Ok(new { succeeded = true });
        }
    }
}