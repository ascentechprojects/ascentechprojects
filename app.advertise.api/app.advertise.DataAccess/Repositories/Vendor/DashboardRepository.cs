using app.advertise.DataAccess.ConnectionStrings;
using app.advertise.DataAccess.Entities.Vendor;
using Dapper;
using Microsoft.Extensions.Logging;

namespace app.advertise.DataAccess.Repositories.Vendor
{
    public interface IDashboardRepository
    {
        Task<(IEnumerable<StatusOverview> FlagStatus, IEnumerable<PrabhagOverview> PrabhagOverview)> DashboardData(DynamicParameters parameters);
    }
    public class DashboardRepository: IDashboardRepository
    {
        private readonly AdvertisementDbContext _context;
        private readonly ILogger<DashboardRepository> _logger;
        public DashboardRepository(AdvertisementDbContext context, ILogger<DashboardRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<(IEnumerable<StatusOverview> FlagStatus, IEnumerable<PrabhagOverview> PrabhagOverview)> DashboardData(DynamicParameters parameters)
        {

            using (var connection = _context.CreateConnection())
            {
                var flagStatus = connection.QueryAsync<StatusOverview>(Queries.Vendor_Dashboard_Application_Status, parameters);
                var prabhagOverview = connection.QueryAsync<PrabhagOverview>(Queries.Vendor_Dashboard_Prabhag_Overview, parameters);

                await Task.WhenAll(flagStatus, prabhagOverview);

                return (FlagStatus: flagStatus.Result ?? Enumerable.Empty<StatusOverview>(), PrabhagOverview: prabhagOverview.Result ?? Enumerable.Empty<PrabhagOverview>());
            }


        }
    }
}
