using app.advertise.dtos;

namespace app.advertise.services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(dtoEmailBody dto);
    }
}
