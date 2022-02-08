namespace Footwear.Services.CartService
{
    using Footwear.ViewModels;
    using Footwear.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICartService
    {
        int GetCartId(string userId);
        Task<Cart> GetCartAsync(int cartId);
        Task<IEnumerable<CartProductViewModel>> GetCartProductsViewModelAsync(int cartId);
        Task<ICollection<CartProduct>> GetCartProductsAsync(int cartId);
        Task AddCartProductAsync(string userId, CartProductViewModel model);
        Task IncreaseQuantityAsync(int productId);
        Task DecreaseQuantityAsync(int productId);
        Task<CartProduct> GetCartProductByIdAsync(int productId);
        Task DeleteCartProductAsync(int productId);
        Task DeleteCartProductsAsync(int productId);
        Task ChangeOrderStateCartProductsAsync(int cartId);
    }
}
