namespace Footwear.Controllers
{
    using System;
    using System.Collections.Generic;
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

        [HttpPost]
        public async Task<IActionResult> PostOrderAsync(OrderViewModel model)
        {
            var order = new Order()
            {
                Id = model.Id,
                Name = model.Name,
                OrderStatus = "Pending",
                CreatedOn = model.CreatedOn,
                Address = model.Address,
                Products = null
            };
            return null;
            //try
            //{
            //    var result = await this._db.Orders.AddAsync(order);
            //    return Ok(result);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

    }
}
