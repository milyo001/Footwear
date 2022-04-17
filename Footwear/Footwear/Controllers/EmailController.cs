
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
        [Route("send")]
        public async Task<IActionResult> Send([FromBody] MailPostModel model)
        {
            try
            {
                var request = await this.mailService.GetEmailRequestAsync(model.Id);
                await mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        public class MailPostModel
        {
            public string Id { get; set; }    
        }
    }
}
