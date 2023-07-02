using app.advertise.DataAccess.Entities;
using Dapper;
using Microsoft.Extensions.Logging;

namespace app.advertise.DataAccess
{
    public interface IApplicationMasterRespository
    {
        Task<IEnumerable<ApplicationAuthSearch>> AuthSearch(DynamicParameters parameters);
        Task<ApplicationAuthSearch> ApplicationById(DynamicParameters parameters);
    }
    public class ApplicationMasterRespository : IApplicationMasterRespository
    {
        private readonly AdvertisementDbContext _context;
        private readonly ILogger<ApplicationMasterRespository> _logger;
        public ApplicationMasterRespository(AdvertisementDbContext context, ILogger<ApplicationMasterRespository> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<IEnumerable<ApplicationAuthSearch>> AuthSearch(DynamicParameters parameters)
        {

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<ApplicationAuthSearch>(Queries.Application_Auth_Search, parameters) ?? Enumerable.Empty<ApplicationAuthSearch>();
        }

        public async Task<ApplicationAuthSearch> ApplicationById(DynamicParameters parameters)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstAsync<ApplicationAuthSearch>(Queries.Application_Details_By_AppliId, parameters) ?? new ApplicationAuthSearch();
        }
    }

}

