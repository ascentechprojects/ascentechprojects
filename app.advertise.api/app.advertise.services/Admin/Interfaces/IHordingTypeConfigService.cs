using app.advertise.dtos;
using app.advertise.dtos.Admin;
using app.advertise.libraries;

namespace app.advertise.services.Admin.Interfaces
{
    public interface IHordingTypeConfigService
    {
        Task InsertUpdate(dtoHordingTypeConfig dtoHordingType, QueryExecutionMode mode);
        Task<IEnumerable<dtoHordingTypeConfig>> GetActiveHoardingTypeConfigs();
        Task<dtoHordingTypeConfig> GetHoardingTypeConfigById(int id);
    }
}
