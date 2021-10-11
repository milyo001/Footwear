
using System.Threading.Tasks;

namespace Footwear.Services.TokenService
{
    public interface ITokenService
    {
        public int GetCartId(string token);

    }
}
