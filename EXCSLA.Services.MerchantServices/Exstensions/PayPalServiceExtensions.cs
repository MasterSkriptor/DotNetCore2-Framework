using EXCSLA.Services.MerchantServices.Paypal;
using Microsoft.Extensions.DependencyInjection;

namespace EXCSLA.Exstensions
{
    public static class PayPalServiceExstensions
    {
        public static IServiceCollection AddPayPalService(this IServiceCollection services, PayPalOptions options)
        {
            return services.AddTransient<IPayPalService>(DefaultServiceProviderFactory => {return new PayPalService(options); });
        }
    }
}