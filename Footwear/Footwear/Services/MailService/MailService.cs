namespace Footwear.Services.MailService
{
    using Footwear.Data.Models.Email;
    using Footwear.Settings;
    using Microsoft.Extensions.Options;
    using System.Threading.Tasks;

    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        /// <summary>
        /// Sends an email with Simple Mail Transfer Protocol (SMTP) 
        /// </summary>
        /// <param name="mailRequest"></param>
        /// <returns></returns>
        public Task SendEmailAsync(EmailRequest mailRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}
