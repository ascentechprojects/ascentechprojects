using app.advertise.dtos.Admin;

namespace app.advertise.services.Admin.Interfaces
{
    public interface IHoardingtypeMasterService
    {
        Task InsertUpdate(dtoHoardingtypeMaster dtoRequest, QueryExecutionMode mode);
        Task<IEnumerable<dtoHoardingtypeMaster>> GetAll();
        Task<dtoHoardingtypeMaster> GetById(int id);
    }
}
