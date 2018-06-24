using EXCSLA.Services.EmailServices.SmtpClientServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EXCSLA.Services.EmailServices
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private ISmtpClientService _client;

        public EmailSender(ISmtpClientService client)
        {
            _client = client;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            await _client.SendMessageAsync(email, subject, message);
        }

        public async Task SendEmailAsync(string email, string subject, string message, string from)
        {
            await _client.SendMessageAsync(email, subject, message, from);
        }

    }
}
