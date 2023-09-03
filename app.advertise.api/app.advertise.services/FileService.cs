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


        public string ByteToBase64(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return null;

            return Convert.ToBase64String(bytes);
        }
    }

}
