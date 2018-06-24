using EXCSLA.Services.FileUploadServices;
using Microsoft.Extensions.DependencyInjection;

namespace EXCSLA.Exstensions
{
    public static class FileUploadServiceExtensions
    {
        public static IServiceCollection AddFileUploadService(this IServiceCollection services)
        {
            return services.AddTransient<IFileUpload, FileUploadService>();
        }
    }
}