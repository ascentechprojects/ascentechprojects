using app.advertise.dtos.Admin;
using app.advertise.libraries;

namespace app.advertise.services.Admin.Interfaces
{
    public interface ILocationMasterService
    {
        Task InsertUpdate(dtoLocationMaster dtoRequest, QueryExecutionMode mode);
        Task<IEnumerable<dtoLocationMaster>> GetAll();
        Task<dtoLocationMaster> GetById(int id);
        Task ModifyStatusById(int id);
    }
}
