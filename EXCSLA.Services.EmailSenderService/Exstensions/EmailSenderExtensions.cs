using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using EXCSLA.Services.EmailServices;
using EXCSLA.Services.EmailServices.SmtpClientServices;
using Microsoft.Extensions.DependencyInjection;

namespace EXCSLA.Exstensions
{
    public static class EmailSenderExstensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }

        public static IServiceCollection AddEmailService(this IServiceCollection services)
        {
            return services.AddTransient<IEmailSender, EmailSender>();
        }
    }

}
