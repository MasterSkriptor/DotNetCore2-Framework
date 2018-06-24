using Microsoft.Extensions.DependencyInjection;
using EXCSLA.Services.EmailServices.SmtpClientServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EXCSLA.Exstensions
{
    public static class SmtpClientServiceExstensions
    {
        public static IServiceCollection AddSmtpClient(this IServiceCollection services, SmtpClientOptions options)
        {
            return services.AddTransient<ISmtpClientService>(factory => { return new SmtpClientService(options); });
        }
    }
}
