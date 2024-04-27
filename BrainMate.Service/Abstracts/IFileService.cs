using Microsoft.AspNetCore.Http;

namespace BrainMate.Service.Abstracts
{
    public interface IFileService
    {
        public Task<string> UploadImage(string location, IFormFile file);
    }
}
