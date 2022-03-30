﻿using _01_Framework.Application;

namespace ServiceHost.Uploder
{
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploader(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

      

        public string Upload(IFormFile file, string path)
        {
            if (file == null) return "";

            var directoryPath = $"{_webHostEnvironment.WebRootPath}//UploadedFiles//{path}";

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            var fileName = DateTime.Now.ToFileName()+file.FileName ;
            var filePath = $"{directoryPath}//{fileName}";
            using var stream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(stream);
            return $"{path}/{fileName}";
        }
       
    }
}
