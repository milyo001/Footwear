using Microsoft.AspNetCore.Mvc;

namespace Footwear.Controllers.Base
{

    [Route("[controller]")]
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
       
    }
}
