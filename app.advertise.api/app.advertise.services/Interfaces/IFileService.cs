using app.advertise.dtos;
using Microsoft.AspNetCore.Http;

namespace app.advertise.services.Interfaces
{
    public interface IFileService
    {
        byte[] ConvertToBytes(IFormFile file);
        string ByteToBase64(byte[] bytes);
    }
}
