

namespace Footwear.Services.CartService
{
    using Footwear.Data.Dto;
    using System.Collections.Generic;
    public interface ICartService
    {
        IEnumerable<CartProductViewModel> GetCartProducts(int cartId);
    }
}
