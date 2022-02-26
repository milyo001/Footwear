

namespace Footwear.Services.ProductService
{
    using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProductService(ApplicationDbContext db, IMapper mapper)
        {
            this._mapper = mapper;
            this._db = db;
        }
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            // Get products from DB async
            var products = await this._db.Products
                .Include(p => p.ProductImage)
                .ToListAsync();
            
            // Map products from DB to Dto obeject
            var productsDtos =  products
                .Select(p => this._mapper.Map<Product, ProductDto>(p))
                .ToList();

            return productsDtos;
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
