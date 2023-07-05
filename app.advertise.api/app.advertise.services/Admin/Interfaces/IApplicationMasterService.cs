using app.advertise.dtos;

namespace app.advertise.services.Admin.Interfaces
{
    public interface IApplicationMasterService
    {
        Task<IEnumerable<dtoApplicationAuthResult>> AuthSerach(dtoApplicationAuthRequest dto);
        Task<dtoApplicationAuthResult> AppliDetailsbyId(int Id);
        Task UpdateStatusFlag(dtoApplicationAuthRequest dto);

        Task<IEnumerable<dtoApplicationAuthResult>> DeauthSearch(dtoApplicationAuthRequest dto);
        Task DeauthStatus(dtoApplicationAuthRequest dto);
    }
}
