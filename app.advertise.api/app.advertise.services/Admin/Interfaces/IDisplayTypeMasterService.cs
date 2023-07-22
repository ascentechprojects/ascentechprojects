using app.advertise.dtos.Admin;

namespace app.advertise.services.Admin.Interfaces
{
    public interface IDisplayTypeMasterService
    {
        Task Insert(dtoDisplayTypeMaster dtoRequest);
        Task Update(dtoDisplayTypeMaster dtoRequest);
        Task<IEnumerable<dtoDisplayTypeMaster>> GetAll();
        Task<dtoDisplayTypeMaster> GetById(string id);
        Task ModifyStatusById(int id);
        Task<IEnumerable<dtoDisplayTypeMaster>> ActiveDisplayTypes();
        Task<IEnumerable<dtoDisplayTypeMaster>> DisplayTypesExistsInConfig(int displayConfigUlbId);
         Task AddUpdateDisplayConfig(IEnumerable<dtoDisplayTypeMaster> dto);

    }
}
