using app.advertise.DataAccess.ConnectionStrings;
using app.advertise.DataAccess.Entities.Admin;
using app.advertise.libraries.Exceptions;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;

namespace app.advertise.DataAccess.Repositories.Admin
{
    public interface IHordingTypeConfigRepository
    {
        Task InsertUpdate(DynamicParameters parameters);
        Task<IEnumerable<HordingTypeConfig>> GetAllActive();
        Task<HordingTypeConfig> GetById(int id);
    }
    public class HordingTypeConfigRepository : IHordingTypeConfigRepository
    {
        private readonly AdvertisementDbContext _context;
        private readonly ILogger<HordingTypeConfigRepository> _logger;
        public HordingTypeConfigRepository(AdvertisementDbContext context, ILogger<HordingTypeConfigRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task InsertUpdate(DynamicParameters parameters)
        {
            parameters.Add("Out_ErrorCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("Out_ErrorMsg", dbType: DbType.String, direction: ParameterDirection.Output);

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(Queries.SP_HoardTyConfig_Ins, parameters, commandType: CommandType.StoredProcedure);

            var errorCode = parameters.Get<int?>("out_ErrorCode");
            var errorMsg = parameters.Get<string?>("Out_ErrorMsg");

            if (errorCode != 9999)
                throw new DBException(errorMsg, _logger);
        }

        public async Task<IEnumerable<HordingTypeConfig>> GetAllActive()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<HordingTypeConfig>(Queries.List_Active_HoardTypeConfig) ?? Enumerable.Empty<HordingTypeConfig>();
        }

        public async Task<HordingTypeConfig> GetById(int id)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<HordingTypeConfig>(Queries.Single_HoardTypeConfig, id) ?? new HordingTypeConfig();
        }
    }
}
