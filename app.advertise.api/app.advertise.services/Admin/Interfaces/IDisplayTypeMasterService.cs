using app.advertise.dtos.Admin;
using app.advertise.libraries;

namespace app.advertise.services.Admin.Interfaces
{
    public interface IDisplayTypeMasterService
    {
        Task InsertUpdate(dtoDisplayTypeMaster dtoRequest, QueryExecutionMode mode);
        Task<IEnumerable<dtoDisplayTypeMaster>> GetAll();
        Task<dtoDisplayTypeMaster> GetById(int id);
        Task ModifyStatusById(int id, string status);
        Task<IEnumerable<dtoDisplayTypeMaster>> ActiveDisplayTypes();
        Task<IEnumerable<dtoDisplayTypeMaster>> DisplayTypesExistsInConfig(int displayConfigUlbId);
         Task AddUpdateDisplayConfig(IEnumerable<dtoDisplayTypeMaster> dto);

    }
}
