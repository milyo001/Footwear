namespace Footwear.Services.ProductService
{
    using Footwear.ViewModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductService
    {
        /// <summary>
        /// Returns IEnumerable collection of all products in the database
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();

        /// <summary>
        /// Returns specific product by given product id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ProductDto> GetProductByIdAsync(int id);
    }
}
