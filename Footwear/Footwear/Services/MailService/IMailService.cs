﻿namespace Footwear.Services.MailService
{
    using Footwear.Data.Models.Email;
    using System.Threading.Tasks;

    public interface IMailService
    {
        /// <summary>
        /// Sends an email with Simple Mail Transfer Protocol (SMTP) 
        /// </summary>
        /// <param name="mailRequest"></param>
        /// <returns>Task</returns>
        Task SendEmailAsync(EmailRequest mailRequest);

        /// <summary>
        /// Populates EmailRequest model with data for selected order
        /// </summary>
        /// <returns>Task of EmailRequest</returns>
        Task<EmailRequest> GetEmailRequestAsync(string orderId);
    }
}
