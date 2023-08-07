using app.advertise.DataAccess.ConnectionStrings;
using app.advertise.DataAccess.Entities.Admin;
using Dapper;
using Microsoft.Extensions.Logging;

namespace app.advertise.DataAccess.Repositories.Admin
{
    public interface IAdminDashboardRepository
    {
        Task<(IEnumerable<AdminDashboardStatusOverview> FlagStatus, IEnumerable<AdminDashboardPrabhagOverview> PrabhagOverview)> DashboardData(int ulbId);
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

        public async Task<(IEnumerable<AdminDashboardStatusOverview> FlagStatus, IEnumerable<AdminDashboardPrabhagOverview> PrabhagOverview)> DashboardData(int ulbId)
        {

            using (var connection = _context.CreateConnection())
            {

                var flagStatus = connection.QueryAsync<AdminDashboardStatusOverview>(Queries.Admin_Dashboard_Approval_Flag_Status, new { ulbId });
                var prabhagOverview = connection.QueryAsync<AdminDashboardPrabhagOverview>(Queries.Admin_Dashboard_Prabhag_Overview, new { ulbId });

                await Task.WhenAll(flagStatus, prabhagOverview);

                return (FlagStatus: flagStatus.Result ?? Enumerable.Empty<AdminDashboardStatusOverview>(), PrabhagOverview: prabhagOverview.Result ?? Enumerable.Empty<AdminDashboardPrabhagOverview>());
            }


        }

    }
}
