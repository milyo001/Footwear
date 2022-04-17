using Microsoft.AspNetCore.Mvc;

namespace Footwear.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
       
    }
}
