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
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        private UserManager<User> _userManager;


        public CartController(ApplicationDbContext db, UserManager<User> userManager)
        {
            this._db = db;
            this._userManager = userManager;
        }


    }
}
