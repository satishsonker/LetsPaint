using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;

namespace LetsPaint.BusinessAccess.Common
{
    public class FileUploadDetails
    {
        IHostingEnvironment _hostingEnvironment;
        public FileUploadDetails(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public List<string> UploadProductImage(List<IFormFile> files, string basePath)
        {
            return FileUpload(files, basePath);
        }

        public string UploadProductImage(IFormFile file, string basePath)
        {
            List<IFormFile> files = new List<IFormFile>
            {
                file
            };
            return FileUpload(files, basePath).FirstOrDefault();
        }

        private List<string> FileUpload(List<IFormFile> files, string basePath)
        {
            if(!Directory.Exists(_hostingEnvironment.WebRootPath + basePath))
            {
                Directory.CreateDirectory(_hostingEnvironment.WebRootPath + basePath);
            }
            List<string> filePath = new List<string>();
            if (files != null && files.Count() > 0)
            {
                foreach (IFormFile file in files)
                {
                    Guid guid = Guid.NewGuid();
                    string imagePath = Path.Combine(_hostingEnvironment.WebRootPath+ basePath, guid.ToString()+ Path.GetExtension(file.FileName));
                    using (var fileStream = new FileStream(imagePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                        filePath.Add(imagePath);
                    }
                }
            }
            return filePath;
        }
        public string ResizeImage(string filePath,int height,int width)
        {
            string _fileName = Path.GetFileNameWithoutExtension(filePath);
            string _fileExt = Path.GetExtension(filePath);
            var image = Image.Load(filePath);
            image.Mutate(x => x.Resize(width, height));
            filePath=filePath.Replace(_fileName + _fileExt, _fileName + "_sm" + _fileExt);
            image.Save(filePath);
            return filePath;
        }
    }
}
