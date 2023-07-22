using app.advertise.dtos.Admin;
using app.advertise.libraries;

namespace app.advertise.services.Admin.Interfaces
{
    public interface IHoardingtypeMasterService
    {
        Task Insert(dtoHoardingtypeMaster dtoRequest);
        Task Update(dtoHoardingtypeMaster dtoRequest);
        Task<IEnumerable<dtoHoardingtypeMaster>> GetAll();
        Task<dtoHoardingtypeMaster> GetById(string id);
        Task ModifyStatusById(int id);

    }
}
