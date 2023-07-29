using app.advertise.dtos.Vendor;
using Microsoft.AspNetCore.Http;

namespace app.advertise.services.Vendor.Interfaces
{
    public interface IApplicationService
    {
        Task<IEnumerable<dtoApplication>> OpenApplications();
        Task<dtoApplicationDetails> AddApplication(dtoApplicationDetails dto,IFormFile formFile);
        Task<dtoApplicationDetails> ApplicationById(string id);
        Task<dtoApplicationDetails> UpdateApplication(dtoApplicationDetails dto, IFormFile formFile);
        Task<byte[]> AppImageById(string id);
        Task<IEnumerable<dtoApplication>> AppCloseSearch(dtoAppClose dtoAppClose);
        Task<IEnumerable<dtoApplication>> CloseApplications(dtoAppClose dto);
    }
}
