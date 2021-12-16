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

        public Cart GetCart(int cartId)
        {
            var cart = this._db.Cart
                .Include(c => c.CartProducts)
                .FirstOrDefault(c => c.Id == cartId);
            return cart;
        }
        //Get all cart products and return the view model to send it to the client
        public IEnumerable<CartProductViewModel> GetCartProductsViewModel(int cartId)
        {
            var cart = this.GetCart(cartId);
            //Gets the products that are not ordered yet
            var products = cart.CartProducts
                 .Where(cp => cp.isOrdered == false)
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
                     ProductType = cp.ProductType.ToString()
                 })
                .ToArray();

            return products;
        }

        //Get all cart products to add them to order
        public ICollection<CartProduct> GetCartProducts(int cartId)
        {
            var cart = this.GetCart(cartId);
                
            var products = cart.CartProducts.Where(cp => cp.CartId == cartId).ToList();

            return products;
        }

        //Get cart product by given cart product id asynchronous
        public async Task<CartProduct> GetCartProductByIdAsync(int cartProductId)
        {
            var product = await this._db.CartProducts.FirstOrDefaultAsync(p => p.Id == cartProductId);
            return product;
        }

        //Increases the given cart product quantity
        public void IncreaseQuantity(int cartProductId)
        {
            var cartProduct =  this.GetCartProductByIdAsync(cartProductId).Result;
            cartProduct.Quantity++;
            this._db.SaveChanges();
        }

        //Decreases the given cart product quantity
        public void DecreaseQuantity(int cartProductId)
        {
            var cartProduct = this.GetCartProductByIdAsync(cartProductId).Result;
            cartProduct.Quantity--;
            this._db.SaveChangesAsync();
        }

        //Removes the cart product by given id
        public void DeleteCartProduct(int cartProductId)
        {
            var cartProduct = this.GetCartProductByIdAsync(cartProductId).Result;
            this._db.CartProducts.Remove(cartProduct);
            this._db.SaveChanges();
        }

        //Removes all cart products
        public void DeleteCartProducts(int cartId)
        {
            var cartProducts = this._db.CartProducts.Where(x => x.CartId == cartId);
            this._db.CartProducts.RemoveRange(cartProducts);
            this._db.SaveChanges();

        }
    }
}
