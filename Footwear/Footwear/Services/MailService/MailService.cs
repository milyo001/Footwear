namespace Footwear.Services.MailService
{
    using Footwear.Data.Models;
    using Footwear.Data.Models.Email;
    using Footwear.Services.OrderService;
    using Footwear.Services.UserService;
    using Footwear.Settings;
    using MailKit.Net.Smtp;
    using MailKit.Security;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using MimeKit;
    using System.Threading.Tasks;

    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        private readonly IOrderService _orderService;

        private readonly UserManager<User> _userManager;

        public MailService(IOptions<MailSettings> mailSettings, IOrderService orderService, 
            UserManager<User> userManager)
        {
            this._mailSettings = mailSettings.Value;
            this._orderService = orderService;
            this._userManager = userManager;
        }

        /// <summary>
        /// Populates email request model for given order id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<EmailRequest> GetEmailRequestAsync(string orderId)
        {
            // Get the selected order data
            var order = await this._orderService.GetOrderByIdAsync(orderId);
            var userEmail = (await this._userManager.FindByIdAsync(order.UserId)).Email;
            var totalPrice = _orderService.GetTotalPrice(order);

            var request = new EmailRequest
            {
                Subject = "Order completed",
                MailBody = 
                     $"<h1>Hello, {order.UserData.FirstName}!</h1>" +
                     $"<p>Your order was completed successfully. " +
                     $"<p>Total products: <strong>{order.Products.Count}</strong></p>" +
                     $"<p>Total price for order: <strong>{totalPrice}</strong></p>" +
                     $"<h4>Thank you for ordering!</h4>",
                ToEmail = $"{userEmail}"
            };

            return request;
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

            // I am using https://ethereal.email as fake SMTP client provider, no email will be send but 
            // the functionallity is working properly, just change appsettings.Production.json Mail settings once you
            // have real SMTP client provider
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);

            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);

            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
