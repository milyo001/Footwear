

namespace Footwear.Services.CartService
{
    using Footwear.Data.Dto;
    using Footwear.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICartService
    {
        IEnumerable<CartProductViewModel> GetCartProducts(int cartId);
        void IncreaseQuantityAsync(int productId);
        void DecreaseQuantityAsync(int productId);
        Task<CartProduct> GetCardProductByIdAsync(int productId);

    }
}
