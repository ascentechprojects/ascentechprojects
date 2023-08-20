using app.advertise.dtos;

namespace app.advertise.services.Vendor.Interfaces
{
    public interface IAppliDocService
    {
        Task<byte[]> GetFileBytes(dtoFormFile file);
        Task InsertUpdateDoc(dtoFormFile file);
    }
}
