using E_Learning.Application.Abstractions.Files;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Infrastructure.Files
{
    internal sealed class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public void DeleteImage(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return;

            string webRootPath = _environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string fullPath = Path.Combine(webRootPath, imagePath.TrimStart('/'));

            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }

        public async Task<string> UploadImageAsync(IFormFile file, string folderName, CancellationToken cancellationToken = default)
        {
            if (file is null || file.Length == 0)
                return null;

            string webRootPath = _environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string uploadsFolder = Path.Combine(webRootPath, "uploads", folderName);

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream, cancellationToken);
            }

            return Path.Combine("uploads", folderName, uniqueFileName).Replace("\\", "/");
        }

        public async Task<string> UploadVideoAsync(IFormFile file, string folderName, CancellationToken ct = default)
        {
            if (file == null || file.Length == 0)
                return null;

            string baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "AppData", "Videos", folderName);

            if (!Directory.Exists(baseFolder))
                Directory.CreateDirectory(baseFolder);

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(baseFolder, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
            {
                await file.CopyToAsync(fileStream, ct);
            }

            return filePath;
        }

        public FileStream GetVideoProvider(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();

            return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        public void DeleteVideo(string videoPath)
        {
            if (string.IsNullOrEmpty(videoPath))
                return;
            if (File.Exists(videoPath))
            {
                File.Delete(videoPath);
            }
        }
    }
}