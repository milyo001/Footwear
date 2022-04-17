
namespace Footwear.Controllers
{
    using Footwear.Services.MailService;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    [Route("[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IMailService mailService;
        public EmailController(IMailService mailService)
        {
            this.mailService = mailService;
        }

        [HttpPost]
        [Route("send/{id}")]
        public async Task<IActionResult> Send(string id)
        {
            try
            {
                var request = await this.mailService.GetEmailRequestAsync(id);
                await mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        [Route("test")]
        public void Test()
        {
            var test = "sadasd";
            
        }
    }
}
