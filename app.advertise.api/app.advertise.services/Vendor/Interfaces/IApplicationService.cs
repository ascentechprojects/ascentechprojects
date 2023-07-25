using app.advertise.dtos.Vendor;
using Microsoft.AspNetCore.Http;

namespace app.advertise.services.Vendor.Interfaces
{
    public interface IApplicationService
    {
        Task<IEnumerable<dtoApplication>> OpenApplications();
        Task<dtoApplicationDetails> AddApplication(dtoApplicationDetails dto,IFormFile formFile);
    }
}
