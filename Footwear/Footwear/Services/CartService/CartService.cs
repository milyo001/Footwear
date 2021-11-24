namespace Footwear.Services.CartService
{
    using Footwear.Data;
    using Footwear.Data.Dto;
    using Footwear.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _db;

        public CartService(ApplicationDbContext db)
        {
            this._db = db;
        }

        public IEnumerable<CartProductViewModel> GetCartProducts(int cartId)
        {
            var cart = this._db.Cart
                .Include(c => c.CartProducts)
                .FirstOrDefault(c => c.Id == cartId);

            var products = cart.CartProducts
                 .Select(cp => new CartProductViewModel
                 {
                     Id = cp.Id,
                     ProductId = cp.ProductId,
                     Name = cp.Name,
                     Size = cp.Size.Value,
                     Gender = cp.Gender.ToString(),
                     Details = cp.Details,
                     ImageUrl = cp.ImageUrl,
                     Price = cp.Price,
                     Quantity = cp.Quantity,
                     ProductType = cp.ProductType.ToString(),
                     CreatedOn = cp.CreatedOn.ToString()
                 })
                .ToArray();

            return products;
        }
        public async Task<CartProduct> GetCardProductByIdAsync(int cartProductId)
        {
            var product = await this._db.CartProducts.FirstOrDefaultAsync(p => p.Id == cartProductId);
            return product;
        }

        public void IncreaseQuantity(int cartProductId)
        {
            var cartProduct =  this.GetCardProductByIdAsync(cartProductId).Result;
            cartProduct.Quantity++;
            this._db.SaveChanges();
        }

        public void DecreaseQuantity(int cartProductId)
        {
            var cartProduct = this.GetCardProductByIdAsync(cartProductId).Result;
            cartProduct.Quantity--;
            this._db.SaveChangesAsync();
        }

        public void DeleteCartProduct(int cartProductId)
        {
            var cartProduct = this.GetCardProductByIdAsync(cartProductId).Result;
            this._db.CartProducts.Remove(cartProduct);
            this._db.SaveChanges();
        }
    }
}
