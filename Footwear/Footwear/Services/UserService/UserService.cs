namespace Footwear.Services.UserService
{
    using Footwear.Data;
    using Footwear.Data.Dto;
    using System.Linq;
    using System.Threading.Tasks;
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;

        public UserService(ApplicationDbContext db)
        {
            this._db = db;
        }
        public Task CreateUser(RegisterViewModel model)
        {
            throw new System.NotImplementedException();
        }

        //Check if user already exist
        public bool isUsernameInUse(string email)
        {
            return this._db.Users.Any(user => user.Email == email.ToUpper());

        }
    }
}
