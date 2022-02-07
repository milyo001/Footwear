namespace Footwear.Services.ProductService
{
    using Footwear.ViewModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductService
    {

        Task<IEnumerable<ProductDto>>GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
    }
}
