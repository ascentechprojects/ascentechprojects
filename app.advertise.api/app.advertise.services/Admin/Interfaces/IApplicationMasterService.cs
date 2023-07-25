using app.advertise.dtos.Admin;

namespace app.advertise.services.Admin.Interfaces
{
    public interface IApplicationMasterService
    {
        Task<IEnumerable<dtoApplicationAuthResult>> AuthSerach(dtoApplicationAuthRequest dto);
        Task<dtoApplicationAuthResult> AppliDetailsbyId(string Id);
        Task UpdateStatusFlag(dtoApplicationAuthRequest dto);

        Task<IEnumerable<dtoApplicationAuthResult>> DeauthSearch(dtoApplicationAuthRequest dto);
        Task<string> DeauthStatus(dtoApplicationAuthRequest dto);
    }
}
