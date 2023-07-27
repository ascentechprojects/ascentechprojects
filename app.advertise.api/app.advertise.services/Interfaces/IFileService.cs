using app.advertise.dtos;

namespace app.advertise.services.Interfaces
{
    public interface IFileService
    {
        Task WriteFile(dtoFormFile file);
        Task<byte[]> ReadFile(string fileId);
    }
}
