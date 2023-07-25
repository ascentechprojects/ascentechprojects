using app.advertise.dtos.Admin;

namespace app.advertise.services.Admin.Interfaces
{
    public interface IHoardingMasterService
    {
        Task Insert(dtoHoardingMaster dtoRequest);
        Task Update(dtoHoardingMaster dto);
        Task<IEnumerable<dtoHoardingMaster>> GetAll();
        Task<dtoHoardingMaster> GetById(string id);
        Task ModifyStatusById(int id);
        Task<dtoHoardingMaster> GetById(int recordId);
    }
}
