using app.advertise.DataAccess.ConnectionStrings;
using app.advertise.DataAccess.Entities.Admin;
using Dapper;
using Microsoft.Extensions.Logging;

namespace app.advertise.DataAccess.Admin
{
    public interface IAdminDashboardRepository
    {
        Task<(IEnumerable<AdminDashboard> FlagStatus, IEnumerable<AdminDashboard> PrabhagOverview)> DashboardData(int ulbId);
    }
    public class AdminDashboardRepository : IAdminDashboardRepository
    {
        private readonly AdvertisementDbContext _context;
        private readonly ILogger<AdminDashboardRepository> _logger;
        public AdminDashboardRepository(AdvertisementDbContext context, ILogger<AdminDashboardRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<(IEnumerable<AdminDashboard> FlagStatus, IEnumerable<AdminDashboard> PrabhagOverview)> DashboardData(int ulbId)
        {

            using (var connection = _context.CreateConnection())
            {

                var flagStatus = connection.QueryAsync<AdminDashboard>(Queries.Admin_Dashboard_Approval_Flag_Status, new { ulbId });
                var prabhagOverview = connection.QueryAsync<AdminDashboard>(Queries.Admin_Dashboard_Prabhag_Overview, new { ulbId });

                await Task.WhenAll(flagStatus, prabhagOverview);

                return (FlagStatus: flagStatus.Result, PrabhagOverview: prabhagOverview.Result);
            }


        }

    }
}
