using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaShop.Application.Common.Interfaces
{
    public interface IFileService
    {
        string SaveFile(string rootPath, string locationName, string fileName, Stream file);
        void DeleteFile(string rootPath, string path);
    }
}
