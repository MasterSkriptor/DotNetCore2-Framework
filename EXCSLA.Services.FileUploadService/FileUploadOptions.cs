using Microsoft.AspNetCore.Hosting;

namespace EXCSLA.Services.FileUploadServices
{
    public class FileUploadOptions
    {
        public IHostingEnvironment Environment {get; set;}
        public string DefaultUploadPath {get; set;}
    }
}