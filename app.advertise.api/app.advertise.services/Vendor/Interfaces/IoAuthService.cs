using app.advertise.dtos.Vendor;

namespace app.advertise.services.Vendor.Interfaces
{
    public interface IoAuthService
    {
        Task<dtoCitizenLoginResponse> VerifyCitizen(dtoCitizenLoginRequest request);
        Task<dtoCitizen> RegisterCitizen(dtoCitizen request);
    }
}
