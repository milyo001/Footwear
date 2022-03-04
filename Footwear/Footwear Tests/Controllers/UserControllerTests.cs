
namespace Footwear_Tests.Controllers
{
    using Footwear.Controllers;
    using Moq;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class UserControllerTests
    {
        public Mock<IEmployeeService> mock = new Mock<IEmployeeService>();

        [Fact]
        public void AddressAndPaymentShouldReturnDefaultView()
            => MyMvc
        .Controller<UserController>()
        .Calling(c => c.RegisterUser(new Footwear.ViewModels.RegisterViewModel()
        {
            Email = "test@test.test",
            FirstName = "Gosho",
            LastName = "Gonsalez",
            Password = "1234567m",
            Phone = "233 311 5554"
        }))
        .ShouldReturn()
        .View();
    }
}
