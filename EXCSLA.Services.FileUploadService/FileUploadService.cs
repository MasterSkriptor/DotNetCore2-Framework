using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using EXCSLA.Services.FileUploadServices;

namespace EXCSLA.Services.FileUploadServices
{
    public class FileUploadService : IFileUpload
    {
        private readonly IHostingEnvironment _environment;
        private string uploadPath;

        public FileUploadService(IHostingEnvironment environment)
        {
            _environment = environment;
            // TODO: Get the imaages/products from a configuration file...
            uploadPath = Path.Combine(_environment.WebRootPath, "images/products");
        }

        public bool FileExists(string fileName)
        {
            return System.IO.File.Exists(_environment.WebRootPath + "\\" + fileName);
        }

        public void UploadFile(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    var filename = Path.Combine(uploadPath, Path.GetFileName(file.FileName));
                    using (var fileStream = new FileStream(filename, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }

        public async Task UploadFileAsync(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    var filename = Path.Combine(uploadPath, Path.GetFileName(file.FileName));
                    using (var fileStream = new FileStream(filename, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void UploadFiles(ICollection<IFormFile> Files)
        {
            foreach (var file in Files)
            {
                UploadFile(file);
            }
        }

        public async Task UploadFilesAsync(ICollection<IFormFile> Files)
        {
            
            foreach (var file in Files)
            {
                await UploadFileAsync(file);
            }
        }
    }
}
