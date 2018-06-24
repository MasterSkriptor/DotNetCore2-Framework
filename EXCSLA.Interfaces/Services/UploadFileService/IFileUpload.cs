using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EXCSLA.Services.FileUploadServices
{
    public interface IFileUpload
    {
        Task UploadFilesAsync(ICollection<IFormFile> Files);
        void UploadFiles(ICollection<IFormFile> Files);
        Task UploadFileAsync(IFormFile file);
        void UploadFile(IFormFile file);
        bool FileExists(string fileName);
    }
}
