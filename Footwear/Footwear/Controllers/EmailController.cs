
namespace Footwear.Controllers
{
    using Footwear.Controllers.Base;
    using Footwear.Services.MailService;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    public class EmailController : ApiController
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
            if (!ModelState.IsValid || model.Id == null)
                return BadRequest("Invalid order id!");

            try
            {
                var request = await this.mailService.GetEmailRequestAsync(model.Id);
                await mailService.SendEmailAsync(request);
                return Ok( new { sent = true });
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
