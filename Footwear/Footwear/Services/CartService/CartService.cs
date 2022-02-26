namespace Footwear.Services.CartService
{
    using AutoMapper;
    using Footwear.Data;
    using Footwear.Data.Models;
    using Footwear.Services.TokenService;
    using Footwear.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _db;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public CartService(ApplicationDbContext db, ITokenService tokenService, IMapper mapper)
        {
            this._db = db;
            this._tokenService = tokenService;
            this._mapper = mapper;
        }

        //Returns the cart id by given user id.
        public int GetCartId(string userId)
        {
            var cartId = this._db.Cart.FirstOrDefault(x => x.UserId == userId).Id;
            return cartId;
        }

        //Gets the cart model by given cart id.
        public async Task<Cart> GetCartAsync(int cartId)
        {
            var cart = await this._db.Cart
                .Include(c => c.CartProducts)
                .FirstOrDefaultAsync(c => c.Id == cartId);
            return cart;
        }

        //Get all cart products and return the view model.
        public async Task<IEnumerable<CartProductViewModel>> GetCartProductsViewModelAsync(int cartId)
        {
            var cart = await this.GetCartAsync(cartId);
            //Gets the products that are not ordered yet
            var products = cart.CartProducts
                 .Where(cp => cp.IsOrdered == false)
                 .ToList()
                 .Select(cp => this._mapper.Map<CartProduct, CartProductViewModel>(cp))
                 .ToList();
            return products;
        }


        // Add a product to the user's cart.Also check if the product name already exists and have the same size in the CartProducts and change the quantity of that dupplicate cartProduct, instead of adding additional instance of CartProduct
        public async Task AddCartProductAsync(string token, Product product, int size)
        {
            
            var cartId = this._tokenService.GetCartId(token);
            var cart = await this.GetCartAsync(cartId);

            // Check if product with same name and size exists and if product is not ordered
            var dupplicateProduct = cart.CartProducts
                    .Where(x => x.Name == product.Name)
                    .Where(x => x.Size == size)
                    .Where(x => x.IsOrdered == false)
                    .FirstOrDefault();

            if (dupplicateProduct == null)
            {
                // Var cartProduct = this._mapper.Map<CartProductViewModel, CartProduct>(product);
                var cartProduct = this._mapper.Map<Product, CartProduct>(product);
                // Set the product size recieved from the client after mapping
                cartProduct.Size = size;
                
                
                cart.CartProducts.Add(cartProduct);
            }
            else
            {
                dupplicateProduct.Quantity++;
            }
            await this._db.SaveChangesAsync();
        }

        //Gets all cart products by given cart id.
        public async Task<ICollection<CartProduct>> GetCartProductsAsync(int cartId)
        {
            var cart = await this.GetCartAsync(cartId);
            var products = cart.CartProducts
                .Where(cp => cp.CartId == cartId)
                .Where(cp => cp.IsOrdered == false)
                .ToList();
            return products;
        }

        //Gets a cart product by given cart product id.
        public async Task<CartProduct> GetCartProductByIdAsync(int cartProductId)
        {
            var product = await this._db.CartProducts
                .FirstOrDefaultAsync(p => p.Id == cartProductId);
            return product;
        }

        //Increases the given cart product quantity.
        public async Task IncreaseQuantityAsync(int cartProductId)
        {
            var cartProduct = this.GetCartProductByIdAsync(cartProductId).Result;
            cartProduct.Quantity++;
            await this._db.SaveChangesAsync();
        }

        //Decreases the given cart product quantity.
        public async Task DecreaseQuantityAsync(int cartProductId)
        {
            var cartProduct = this.GetCartProductByIdAsync(cartProductId).Result;
            cartProduct.Quantity--;
            await this._db.SaveChangesAsync();
        }

        //Removes the cart product by given cart product id.
        public async Task DeleteCartProductAsync(int cartProductId)
        {
            var cartProduct = this.GetCartProductByIdAsync(cartProductId).Result;
            this._db.CartProducts.Remove(cartProduct);
            await this._db.SaveChangesAsync();
        }

        //Removes all cart products by given cart id.
        public async Task DeleteCartProductsAsync(int cartId)
        {
            var cartProducts = this._db.CartProducts.Where(x => x.CartId == cartId);
            this._db.CartProducts.RemoveRange(cartProducts);
            await this._db.SaveChangesAsync();
        }

        //Change IsOrdered property all cart products after order is ordered.
        public async Task ChangeOrderStateCartProductsAsync(int cartId)
        {
            var cartProducts = await this.GetCartProductsAsync(cartId);
            cartProducts.ToList().ForEach(cp => cp.IsOrdered = true);
            await this._db.SaveChangesAsync();
        }
    }
}
