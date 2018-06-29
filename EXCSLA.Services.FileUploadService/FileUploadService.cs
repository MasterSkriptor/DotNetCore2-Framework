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

        public FileUploadService(FileUploadOptions options)
        {
            _environment = options.Environment;
            uploadPath = Path.Combine(_environment.WebRootPath, options.DefaultUploadPath);
        }

        public bool FileExists(string fileName)
        {
            return System.IO.File.Exists(_environment.WebRootPath + "\\" + fileName);
        }

        public void UploadFile(IFormFile file)
        {
            UploadFile(file, "");
        }
        public void UploadFile(IFormFile file, string relativePath)
        {
            try
            {
                if (file.Length > 0)
                {
                    string filename;
                    string path;

                    if(string.IsNullOrEmpty(relativePath))
                        path = uploadPath;
                    else
                        path = relativePath;
                    
                    filename = Path.Combine(path, Path.GetFileName(file.FileName));               
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
            await UploadFileAsync(file, "");
        }
        public async Task UploadFileAsync(IFormFile file, string relativePath)
        {
            try
            {
                if (file.Length > 0)
                {
                    string path;

                    if(string.IsNullOrEmpty(relativePath))
                        path = uploadPath;
                    else
                        path = relativePath;

                    var filename = Path.Combine(path, Path.GetFileName(file.FileName));
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
            UploadFiles(Files, "");
        }
        public void UploadFiles(ICollection<IFormFile> Files, string relativePath)
        {
            string path;

            if(string.IsNullOrEmpty(relativePath))
                path = uploadPath;
            else
                path = relativePath;
            
            foreach (var file in Files)
            {
                UploadFile(file, path);
            }
        }

        public async Task UploadFilesAsync(ICollection<IFormFile> Files)
        {
            await UploadFilesAsync(Files, "");
        }
        public async Task UploadFilesAsync(ICollection<IFormFile> Files, string relativePath)
        {
            string path;

            if(string.IsNullOrEmpty(relativePath))
                path = uploadPath;
            else
                path = relativePath;

            foreach (var file in Files)
            {
                await UploadFileAsync(file, path);
            }
        }
    }
}
