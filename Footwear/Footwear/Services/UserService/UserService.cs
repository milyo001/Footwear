namespace Footwear.Services.UserService
{
    using AutoMapper;
    using Footwear.Data;
    using Footwear.Data.Models;
    using Footwear.ViewModels;
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

        //Creates new user in the database
        public async Task<IdentityResult> CreateUserAsync(RegisterViewModel model)
        {
            var user = this._mapper.Map<User>(model);
            IdentityResult result = await this._userManager.CreateAsync(user, model.Password);
            return result;
        }

        //Check if user already exist in the database
        public bool isUsernameInUse(string email)
        {
            return this._db.Users.Any(user => user.Email == email.ToUpper());
        }

        //Returns the user data from the database
        public UserProfileDataViewModel GetUserData(User user)
        {
            var userData = this._mapper.Map<UserProfileDataViewModel>(user);
            return userData;
        }

        //Updates the user data by given user and model
        public async Task<IdentityResult> UpdateUserDataAsync(User user, ProfileUpdateViewModel model)
        {
            var modifiedUser = this._mapper.Map(model, user);

            IdentityResult result = await _userManager.UpdateAsync(modifiedUser);
            return result;
        }

        //Updates username(by default the email address) in the database
        public async Task<IdentityResult> UpdateEmailAsync(User user, string email)
        {
            user.Email = email;
            user.NormalizedEmail = email.ToUpper();
            user.UserName = email;
            user.NormalizedUserName = email.ToUpper();
            var result = await this._userManager.UpdateAsync(user);
            return result;
        }
    }
}
