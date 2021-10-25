
using System.Threading.Tasks;

namespace Footwear.Services.TokenService
{
    public interface ITokenService
    {
        int GetCartId(string token);

        string GetUserId(string token);

    }
}
