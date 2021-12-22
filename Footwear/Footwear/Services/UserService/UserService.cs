namespace Footwear.Services.UserService
{
    using Footwear.Data.Dto;
    using System.Threading.Tasks;
    public class UserService : IUserService
    {
        public Task CreateUser(RegisterViewModel model)
        {
            throw new System.NotImplementedException();
        }

        public bool isUsernameInUse(string email)
        {
            throw new System.NotImplementedException();
        }
    }
}
