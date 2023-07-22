using app.advertise.dtos.Admin;

namespace app.advertise.services.Admin.Interfaces
{
    public interface IDashboardService
    {
        Task<dtoDashboard> DashboardDetails();
    }
}
