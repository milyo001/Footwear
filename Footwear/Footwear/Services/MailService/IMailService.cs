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
        /// <returns></returns>
        Task SendEmailAsync(EmailRequest mailRequest);

        /// <summary>
        /// Populates EmailRequest model with data, edit to change the email context
        /// </summary>
        /// <returns></returns>
        Task<EmailRequest> PopulateEmailRequestAsync(string token);
    }
}
