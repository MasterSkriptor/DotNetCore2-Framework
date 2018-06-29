using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EXCSLA.Services.FileUploadServices
{
    public interface IFileUpload
    {
        Task UploadFilesAsync(ICollection<IFormFile> Files);
        Task UploadFilesAsync(ICollection<IFormFile> Files, string uploadPath);
        void UploadFiles(ICollection<IFormFile> Files);
        void UploadFiles(ICollection<IFormFile> Files, string uploadPath);
        Task UploadFileAsync(IFormFile file);
        Task UploadFileAsync(IFormFile file, string uploadPath);
        void UploadFile(IFormFile file);
        void UploadFile(IFormFile file, string uploadPath);
        bool FileExists(string fileName);
    }
}
