using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Abstractions.Files
{
    public interface IFileService
    {
        Task<string> UploadImageAsync(IFormFile file, string folderName, CancellationToken cancellationToken = default);
        Task<string> UploadVideoAsync(IFormFile file, string folderName, CancellationToken cancellationToken = default);
        FileStream GetVideoProvider(string path);
        void DeleteImage(string imagePath);
    }
}
