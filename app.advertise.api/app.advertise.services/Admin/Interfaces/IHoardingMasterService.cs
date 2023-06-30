using app.advertise.dtos.Admin;

namespace app.advertise.services.Admin.Interfaces
{
    public interface IHoardingMasterService
    {
        Task InsertUpdate(dtoHoardingMaster dtoRequest, QueryExecutionMode mode);
        Task<IEnumerable<dtoHoardingMaster>> GetAll();
        Task<dtoHoardingMaster> GetById(int id);
        Task ModifyStatusById(int id);
    }
}
