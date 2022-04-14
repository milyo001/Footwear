using Footwear.Data.Models.Email;
using System.Threading.Tasks;

namespace Footwear.Services.MailService
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public Task SendEmailAsync(EmailRequest mailRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}
