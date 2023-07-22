using app.advertise.dtos.Admin;
using app.advertise.libraries;

namespace app.advertise.services.Admin.Interfaces
{
    public interface ILocationMasterService
    {
        Task Update(dtoLocationMaster dtoRequest);
        Task Insert(dtoLocationMaster dtoRequest);
        Task<IEnumerable<dtoLocationMaster>> GetAll();
        Task<dtoLocationMaster> GetById(string id);
        Task ModifyStatusById(int id);
    }
}
