using app.advertise.dtos.Admin;

namespace app.advertise.services.Admin.Interfaces
{
    public interface IDisplayTypeMasterService
    {
        Task InsertUpdate(dtoDisplayTypeMaster dtoRequest, QueryExecutionMode mode);
        Task<IEnumerable<dtoDisplayTypeMaster>> GetAll();
        Task<dtoDisplayTypeMaster> GetById(int id);

        Task ModifyStatusById(int id, string status);

    }
}
