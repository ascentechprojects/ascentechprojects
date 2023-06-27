using app.advertise.dtos.Admin;

namespace app.advertise.services.Admin.Interfaces
{
    public interface IOAuthService
    {
        Task<dtoAuthResponse> AuthenticateUser(dtoAuthRequest authRequest);
    }
}
