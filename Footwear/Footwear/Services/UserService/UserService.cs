namespace Footwear.Services.UserService
{
    using AutoMapper;
    using Footwear.Data;
    using Footwear.Data.Dto;
    using Footwear.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using System.Linq;
    using System.Threading.Tasks;
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext db, UserManager<User> userManager, IMapper mapper)
        {
            this._db = db;
            this._userManager = userManager;
            this._mapper = mapper;
        }

        public async Task<IdentityResult> CreateUserAsync(RegisterViewModel model)
        {
            var user = this._mapper.Map<User>(model);
            IdentityResult result = await this._userManager.CreateAsync(user, model.Password);
            return result;
        }

        //Check if user already exist
        public bool isUsernameInUse(string email)
        {
            return this._db.Users.Any(user => user.Email == email.ToUpper());
        }

        public UserProfileDataViewModel GetUserData(User user)
        {
            var userData = this._mapper.Map<UserProfileDataViewModel>(user);
            return userData;
        }

        public async Task<IdentityResult> UpdateUserDataAsync(User user, ProfileUpdateViewModel model)
        {
            var modifiedUser = this._mapper.Map(model, user);
            
            IdentityResult result = await _userManager.UpdateAsync(modifiedUser);
            return result;
        }
    }
}
