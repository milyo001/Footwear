

namespace Footwear.Services.CartService
{
    using Footwear.Data.Dto;
    using Footwear.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICartService
    {
        int GetCartId(string userId);
        IEnumerable<CartProductViewModel> GetCartProductsViewModel(int cartId);
        ICollection<CartProduct> GetCartProducts(int cartId);
        Task AddCartProductAsync(string userId, CartProductViewModel model);
        Task IncreaseQuantityAsync(int productId);
        Task DecreaseQuantityAsync(int productId);
        Task<CartProduct> GetCartProductByIdAsync(int productId);
        Task DeleteCartProductAsync(int productId);
        Task DeleteCartProductsAsync(int productId);
        Task ChangeOrderStateCartProductsAsync(int cartId);
    }
}
