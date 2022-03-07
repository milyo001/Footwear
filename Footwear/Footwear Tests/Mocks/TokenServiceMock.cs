namespace Footwear_Tests.Mocks
{
    using Footwear.Data.Models;
    using Footwear.Services.TokenService;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    class TokenServiceMock : ITokenService
    {
        public string EncryptToken(string token)
        {
            throw new System.NotImplementedException();
        }

        public string GenerateToken(string userId, int cartId)
        {
            return "kadsadqweqweasdsc23aqsa";
        }

        public int GetCartId(string token)
        {
            return 1;
        }

        public async Task<User> GetUserByIdAsync(string token)
        {
            await Task.Delay(100);

            var user = new User
            {
                Id = "2222",
                UserName = "ivan",
                Cart = new Cart
                {
                    Id = 1,
                    CartProducts = new List<CartProduct>()
                },
                Address = new Address
                {
                    Id = 1,
                    State = "test",
                    Street = "test",
                    City = "Sofia",
                    Country = "Bulgaria",
                    ZipCode = "2333"
                },
                Phone = "223121312313",
                Email = "test@test.com"
            };

            return user;
        }

        public string GetUserId(string token)
        {
            return "2222";
        }
    }
}
