

namespace Footwear.Services.ProductService
{
    using Footwear.Data;
    using Footwear.Data.Models;
    using Footwear.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;
        public ProductService(ApplicationDbContext db)
        {
            this._db = db;
        }
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            //This query is made without Automapper, Not able to include product image navigation property
            //TODO: map data to object with automaper
            var products = await this._db.Products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Details = p.Details,
                ImageUrl = p.ProductImage.ImageUrl,
                Gender = p.Gender.ToString(),
                ProductType = p.ProductType.ToString()
            })
                .ToArrayAsync();
            return products;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await this._db.Products
                .Include(p => p.ProductImage)
                .FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<ProductDto> GetProductDtoByIdAsync(int id)
        {
            //TODO: For some reason EF Core is unable to include ImageUrl from ProductImage table with .Include
            var productDto = await this._db.Products
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
            return productDto;
        }


    }
}
