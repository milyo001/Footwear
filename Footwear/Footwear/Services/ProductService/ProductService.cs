using Footwear.Data;
using Footwear.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Footwear.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;
        public ProductService(ApplicationDbContext db)
        {
            this._db = db;
        }
        public IEnumerable<ProductDto> GetAllProducts()
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
    }
}
