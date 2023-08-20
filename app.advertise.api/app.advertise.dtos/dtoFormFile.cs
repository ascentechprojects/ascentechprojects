using Microsoft.AspNetCore.Http;

namespace app.advertise.dtos
{
    public class dtoFormFile
    {
        public IFormFile FormFile { get; set; }
        public string FileName { get;set; }

        public int ApplicationId { get; set; }
        public string ApplicationNumber { get; set; }
    }
}
