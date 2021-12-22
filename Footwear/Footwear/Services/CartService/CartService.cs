namespace Footwear.Services.CartService
{
    using Footwear.Data;
    using Footwear.Data.Dto;
    using Footwear.Data.Models;
    using Footwear.Data.Models.Enums;
    using Microsoft.EntityFrameworkCore;
    using System;
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


        //Check if the product name already exists and have the same size
        //in the database and change the quantity of that cartProduct, instead of adding additional instance of 
        //CartProduct or create new cart product
        public async Task AddCartProductAsync(string userId, CartProductViewModel model)
        {
            var cart = this._db.Cart.FirstOrDefault(x => x.UserId == userId);

            //Check if product with same name and size already exists, check if product is unordered
            var dupplicateProduct = cart.CartProducts
                    .Where(x => x.Name == model.Name)
                    .Where(x => x.Size == model.Size)
                    .Where(x => x.isOrdered == false)
                    .FirstOrDefault();

            if (dupplicateProduct != null)
            {
                dupplicateProduct.Quantity++;
            }
            else
            {
                var cartProduct = new CartProduct
                {
                    Name = model.Name,
                    Details = model.Details,
                    Size = model.Size,
                    Gender = (Gender)Enum.Parse(typeof(Gender), model.Gender), //Parse from string to Enum
                    ProductType = (ProductType)Enum.Parse(typeof(ProductType), model.ProductType),
                    ImageUrl = model.ImageUrl,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    ProductId = model.ProductId
                };
                cart.CartProducts.Add(cartProduct);
            }
            await this._db.SaveChangesAsync();
        }


        //Get all cart products to add them to order
        public ICollection<CartProduct> GetCartProducts(int cartId)
        {
            var cart = this.GetCart(cartId);
            var products = cart.CartProducts.Where(cp => cp.CartId == cartId).ToList();
            return products;
        }

        //Get a single cart product by given cart product id
        public async Task<CartProduct> GetCartProductByIdAsync(int cartProductId)
        {
            var product = await this._db.CartProducts.FirstOrDefaultAsync(p => p.Id == cartProductId);
            return product;
        }

        //Increases the given cart product quantity
        public async Task IncreaseQuantityAsync(int cartProductId)
        {
            var cartProduct =  this.GetCartProductByIdAsync(cartProductId).Result;
            cartProduct.Quantity++;
            await this._db.SaveChangesAsync();
        }

        //Decreases the given cart product quantity
        public async Task DecreaseQuantityAsync(int cartProductId)
        {
            var cartProduct = this.GetCartProductByIdAsync(cartProductId).Result;
            cartProduct.Quantity--;
            await this._db.SaveChangesAsync();
        }

        //Removes the cart product by given id
        public async Task DeleteCartProductAsync(int cartProductId)
        {
            var cartProduct = this.GetCartProductByIdAsync(cartProductId).Result;
            this._db.CartProducts.Remove(cartProduct);
            await this._db.SaveChangesAsync();
        }

        //Removes all cart products
        public async Task DeleteCartProductsAsync(int cartId)
        {
            var cartProducts = this._db.CartProducts.Where(x => x.CartId == cartId);
            this._db.CartProducts.RemoveRange(cartProducts);
            await this._db.SaveChangesAsync();
        }

        //Change isOrdered property all cart products after order is finished
        public async Task ChangeOrderStateCartProductsAsync(int cartId)
        {
            var cartProducts = this.GetCartProducts(cartId).ToList();
            cartProducts.ForEach(cp => cp.isOrdered = true);
            await this._db.SaveChangesAsync();
        }
    }
}
