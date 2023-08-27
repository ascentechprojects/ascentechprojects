using app.advertise.services.Interfaces;

namespace app.advertise.services
{
    using Microsoft.AspNetCore.Http;
    using System.IO;

    public class FileService : IFileService
    {
        public byte[] ConvertToBytes(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
        
    }

}
