using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EXCSLA.Services.EmailServices.SmtpClientServices
{
    public class SmtpClientService : ISmtpClientService
    {
        private SmtpClient _client;

        public SmtpClientService(SmtpClientOptions options)
        {
            _client = new SmtpClient(options.HostName);
            _client.UseDefaultCredentials = options.UseDefaultCredentials;
            _client.EnableSsl = true;

            if (_client.UseDefaultCredentials == false)
            {
                _client.Credentials = new NetworkCredential(options.UserName, options.Password);
            }
        }

        public async Task SendMessageAsync(string email, string subject, string message)
        {
            await SendMessageAsync(email, subject, message, "he@excsla.com");
        }

        public async Task SendMessageAsync(string email, string subject, string message, string from)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(from);
            mailMessage.To.Add(email);
            mailMessage.Body = message;
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            
            await _client.SendMailAsync(mailMessage);
        }

    }
}
