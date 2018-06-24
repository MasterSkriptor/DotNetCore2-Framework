using EXCSLA.Services.DataServices;
using Microsoft.AspNetCore.Builder;

namespace EXCSLA.Exstensions
{
    public static class HandleDbUpdateExceptionExstensions
    {
        public static IApplicationBuilder UseDbUpdateExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DbUpdateExceptionHandler>();
        }
    }
}