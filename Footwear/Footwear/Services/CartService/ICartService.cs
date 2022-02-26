namespace Footwear.Services.CartService
{
    using Footwear.Data.Models;
    using Footwear.ViewModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICartService
    {
        /// <summary>
        /// Returns the cart id by given user id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        int GetCartId(string userId);

        /// <summary>
        /// Gets the cart model by given cart id.
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        Task<Cart> GetCartAsync(int cartId);

        /// <summary>
        /// Get all cart products and return the view model.
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        Task<IEnumerable<CartProductViewModel>> GetCartProductsViewModelAsync(int cartId);

        /// <summary>
        /// Gets all cart products by given cart id.
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        Task<ICollection<CartProduct>> GetCartProductsAsync(int cartId);

        /// <summary>
        /// Add a product to the user's cart.Also check if the product name already exists and have the same size in the CartProducts and change the quantity of that dupplicate cartProduct, instead of adding additional instance of CartProduct
        /// </summary>
        /// <param name="token"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task AddCartProductAsync(string token, Product product, int size);

        /// <summary>
        /// Increases the given cart product 
        /// by given cart product id.
        /// </summary>
        /// <param name="cartProductId"></param>
        /// <returns></returns>
        Task IncreaseQuantityAsync(int productId);

        /// <summary>
        /// Decreases the given cart product quantity by given cart product id.
        /// </summary>
        /// <param name="cartProductId"></param>
        /// <returns></returns>
        Task DecreaseQuantityAsync(int productId);

        /// <summary>
        /// Gets a cart product by given cart product id.
        /// </summary>
        /// <param name="cartProductId"></param>
        /// <returns></returns>
        Task<CartProduct> GetCartProductByIdAsync(int productId);

        /// <summary>
        /// Removes single cart product by given cart product id.
        /// </summary>
        /// <param name="cartProductId"></param>
        /// <returns></returns>
        Task DeleteCartProductAsync(int productId);

        /// <summary>
        /// Removes all cart products by given cart id.
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        Task DeleteCartProductsAsync(int productId);

        /// <summary>
        /// Change IsOrdered property all cart products after order is ordered.
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        Task ChangeOrderStateCartProductsAsync(int cartId);
    }
}
