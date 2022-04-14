﻿namespace Footwear.Services.MailService
{
    using Footwear.Data.Models.Email;
    using Footwear.Services.OrderService;
    using Footwear.Services.TokenService;
    using Footwear.Settings;
    using MailKit.Net.Smtp;
    using MailKit.Security;
    using Microsoft.Extensions.Options;
    using MimeKit;
    using System.Threading.Tasks;

    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        private readonly IOrderService _orderService;

        public MailService(IOptions<MailSettings> mailSettings, IOrderService orderService)
        {
            _mailSettings = mailSettings.Value;
            _orderService = orderService;
        }


        /// <summary>
        /// Populates email request model for given order id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Task<EmailRequest> GetEmailRequestAsync(string orderId)
        {
            var orders = this._orderService.GetOrderByIdAsync(orderId);
            
            return null;
        }

        /// <summary>
        /// Sends an email with Simple Mail Transfer Protocol (SMTP) 
        /// </summary>
        /// <param name="mailRequest"></param>
        /// <returns></returns>
        public async Task SendEmailAsync(EmailRequest mailRequest)
        {
            // Multipurpose Internet Mail Extensions instance. Support text in character sets other than ASCII,
            // as well as attachments of audio, video, images, and application programs
            var email = new MimeMessage();

            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = mailRequest.MailBody;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);

            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
