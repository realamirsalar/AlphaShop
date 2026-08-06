using AlphaShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Infrastructure.Services
{
    public class FileService : IFileService
    {
        public string SaveFile(string rootPath,string locationName, string fileName, Stream file)
        {
            var uploadFolder = Path.Combine(rootPath, @$"images\{locationName}");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }
            var uniqFileName = Guid.NewGuid().ToString() + "_" + fileName;
            var filePath = Path.Combine(uploadFolder, uniqFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return $"\\images\\product\\{uniqFileName}";
        }

        public void DeleteFile(string rootPath, string path)
        {
            //var imagePath = product.ImageUrl.TrimStart('\\');
            var filePath = Path.Combine(rootPath, path);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

       
    }
}
