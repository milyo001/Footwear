

namespace Footwear.Services.ProductService
{
    using Footwear.ViewModels;
    using System.Collections.Generic;

    public interface IProductService
    {
        IEnumerable<ProductDto>GetAllProducts();
    }
}
