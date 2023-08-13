using app.advertise.dtos.Vendor;
using Dapper;

namespace app.advertise.services.Vendor.Interfaces
{
    public interface IDashboardService
    {
        Task<dtoDashboard> DashboardDetails();
    }
}
