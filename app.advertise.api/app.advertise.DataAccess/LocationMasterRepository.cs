using app.advertise.DataAccess.Entities;
using app.advertise.libraries.Exceptions;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;

namespace app.advertise.DataAccess
{
    public interface ILocationMasterRepository
    {
        Task InsertUpdate(DynamicParameters parameters);
        Task<IEnumerable<LocationMaster>> GetAll();
        Task<LocationMaster> GetById(int id);
    }
    public class LocationMasterRepository : ILocationMasterRepository
    {
        private readonly AdvertisementDbContext _context;
        private readonly ILogger<LocationMasterRepository> _logger;
        public LocationMasterRepository(AdvertisementDbContext context, ILogger<LocationMasterRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task InsertUpdate(DynamicParameters parameters)
        {
            parameters.Add("Out_ErrorCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("Out_ErrorMsg", dbType: DbType.String, size: 1000, direction: ParameterDirection.Output);

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(Queries.SP_LocationMaster_Ins, parameters, commandType: CommandType.StoredProcedure);

            var errorCode = parameters.Get<int?>("Out_ErrorCode");
            var errorMsg = parameters.Get<string?>("Out_ErrorMsg");

            if (errorCode != 9999)
                throw new DBException(errorMsg, _logger);
        }

        public async Task<IEnumerable<LocationMaster>> GetAll()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<LocationMaster>(Queries.List_location_MST) ?? Enumerable.Empty<LocationMaster>();
        }

        public async Task<LocationMaster> GetById(int id)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<LocationMaster>(Queries.Single_ById_location_MST, id) ?? new LocationMaster();
        }
    }
}
