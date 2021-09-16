namespace Footwear.Controllers
{
    using Footwear.Data;
    using Footwear.Data.Dto;
    using Footwear.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        private UserManager<User> _userManager;


        public CartController(ApplicationDbContext db, UserManager<User> userManager)
        {
            this._db = db;
            this._userManager = userManager;
        }


        [HttpGet]
        public IEnumerable<CartProductViewModel> Get(string userName)
        {
            
            //IEnumerable<CartProductViewModel> cartProducts = 
            //    this._db.Cart
            //    .Where(x => x.Id == cartId)
            //    .Select(c => c.CartProducts)
            //   .ToArray();

            return null;
            //return products;
            //TODO
        }

       



    }
}
