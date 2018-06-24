using EXCSLA.Services.DataServices;
using EXCSLA.Services.DataServices.EFDataServices;
using EXCSLA.Services.DataServices.WebObjectDataServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EXCSLA.Exstensions
{
    public static class DataServiceExstensions
    {
        public static IServiceCollection AddDataServices<TContext>(this IServiceCollection services)
            where TContext : DbContext
        {
            return services.AddScoped(typeof(IDataService<TContext>), typeof(EFDataService<TContext>));
        }

        public static IServiceCollection AddWebObjectDataServices<TContext>(this IServiceCollection services)
            where TContext : DbContext
        {
            return services.AddScoped(typeof(IWebObjectDataService<TContext>), typeof(WebObjectDataService<TContext>));
        }
    }
}