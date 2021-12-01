﻿

namespace Footwear.Services.CartService
{
    using Footwear.Data.Dto;
    using Footwear.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICartService
    {
        IEnumerable<CartProductViewModel> GetCartProductsViewModel(int cartId);
        ICollection<CartProduct> GetCartProducts(int cartId);

        void IncreaseQuantity(int productId);
        void DecreaseQuantity(int productId);
        Task<CartProduct> GetCartProductByIdAsync(int productId);
        void DeleteCartProduct(int productId);
        void DeleteCartProducts(int productId);


    }
}
