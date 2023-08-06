using app.advertise.dtos;
using Microsoft.AspNetCore.Http;

namespace app.advertise.services.Interfaces
{
    public interface IFileService
    {
        Task WriteFile(dtoFormFile file);
        Task<byte[]> ReadFile(string fileId);
        string ConvertToBase64(IFormFile file);
    }
}
