namespace Footwear.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Footwear.Data;
    using Footwear.Data.Dto;
    using Footwear.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        private UserManager<User> _userManager;


        public OrderController(ApplicationDbContext db, UserManager<User> userManager)
        {
            this._db = db;
            this._userManager = userManager;
        }

        //[HttpPost]
        //public async void  PostOrder (OrderViewModel model)
        //{
        //    var order = new Order()
        //    {
        //        Id = model.Id,
        //        Name = Guid.NewGuid().ToString(),
        //        OrderStatus = model.OrderStatus,
        //        CreatedOn = model.CreatedOn,
        //        Address =  model.Address
        //    };

        //    foreach (var product in model.Products)
        //    {
        //        var currentProduct = this._db.Products.FirstOrDefault(x=> x.Id == product.Id);
        //        order.Products.Add(currentProduct);
        //    }
        //    var user = await this._userManager.FindByIdAsync(model.UserId);

        //    try
        //    {
        //         user.Orders.Add(order);
        //        _db.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

    }
}
