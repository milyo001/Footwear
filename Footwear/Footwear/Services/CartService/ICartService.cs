

namespace Footwear.Services.CartService
{
    using Footwear.Data.Dto;
    using Footwear.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrderService
    {
        IEnumerable<CartProductViewModel> GetCartProducts(int cartId);
        void IncreaseQuantity(int productId);
        void DecreaseQuantity(int productId);
        Task<CartProduct> GetCardProductByIdAsync(int productId);

        void DeleteCartProduct(int productId);

    }
}
