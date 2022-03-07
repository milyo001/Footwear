
namespace Footwear_Tests.Mocks
{
    using Footwear.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Moq;

    public static class MockServices
    {
        public static Mock<UserManager<User>> MockUserService()
        {
            var userStore = new Mock<IUserStore<User>>();
            var mockUserService = new Mock<UserManager<User>>(userStore.Object, null, null, null, null, null, null, null, null);
            return mockUserService;
        }
    }
}
