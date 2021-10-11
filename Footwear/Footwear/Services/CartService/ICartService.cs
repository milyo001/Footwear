

namespace Footwear.Services.CartService
{
    using Footwear.Data.Dto;
    using Footwear.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICartService
    {
        IEnumerable<CartProductViewModel> GetCartProducts(int cartId);
        Task<CartProduct> IncreaseQuantityAsync(int productId);
        Task<CartProduct> DecreaseQuantityAsync(int productId);
    }
}
