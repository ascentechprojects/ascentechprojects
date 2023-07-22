using app.advertise.DataAccess.Entities;
using app.advertise.libraries.Exceptions;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;

namespace app.advertise.DataAccess
{
    public interface IDisplayTypeMasterRepository
    {
        Task InsertUpdate(DynamicParameters parameters);
        Task<IEnumerable<DisplayTypeMaster>> GetAll();
        Task<DisplayTypeMaster> GetById(int id);
        Task<IEnumerable<DisplayTypeMaster>> ActiveDisplayTypes();
        Task<IEnumerable<DisplayTypeMaster>> DisplayTypesExistsInConfig(int displayConfigUlbId);
        Task InsertUpdateConfig(DynamicParameters parameters);
    }
    public class DisplayTypeMasterRepository : IDisplayTypeMasterRepository
    {
        private readonly AdvertisementDbContext _context;
        private readonly ILogger<DisplayTypeMasterRepository> _logger;
        public DisplayTypeMasterRepository(AdvertisementDbContext context, ILogger<DisplayTypeMasterRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task InsertUpdate(DynamicParameters parameters)
        {
            parameters.Add("out_errorcode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("out_errormsg", dbType: DbType.String, size: 1000, direction: ParameterDirection.Output);

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(Queries.SP_DisplayTypeMaster_Ins, parameters, commandType: CommandType.StoredProcedure);

            var errorCode = parameters.Get<int?>("out_errorcode");
            var errorMsg = parameters.Get<string?>("out_errormsg");

            if (errorCode != 9999)
                throw new DBException(errorMsg, _logger);
        }

        public async Task<IEnumerable<DisplayTypeMaster>> GetAll()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<DisplayTypeMaster>(Queries.List_DisplayTYPE_MST) ?? Enumerable.Empty<DisplayTypeMaster>();
        }

        public async Task<DisplayTypeMaster> GetById(int id)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<DisplayTypeMaster>(Queries.Single_ById_DisplayTYPE_MST, new { id }) ?? new DisplayTypeMaster();
        }

        public async Task<IEnumerable<DisplayTypeMaster>> ActiveDisplayTypes()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<DisplayTypeMaster>(Queries.Select_Active_DisplayTypes) ?? Enumerable.Empty<DisplayTypeMaster>();
        }

        public async Task<IEnumerable<DisplayTypeMaster>> DisplayTypesExistsInConfig(int displayConfigUlbId)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<DisplayTypeMaster>(Queries.Select_DisplayTypes_Exists_Config, new { displayConfigUlbId }) ?? Enumerable.Empty<DisplayTypeMaster>();
        }

        public async Task InsertUpdateConfig(DynamicParameters parameters)
        {
            parameters.Add("Out_ErrorCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("Out_ErrorMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(Queries.SP_DisplayTypeConfig_Ins, parameters, commandType: CommandType.StoredProcedure);

            var errorCode = parameters.Get<int?>("Out_ErrorCode");
            var errorMsg = parameters.Get<string?>("Out_ErrorMsg");

            if (errorCode != 9999)
                throw new DBException($"Status:{errorCode}, Message:{errorMsg} ", _logger); ;
        }
    }
}
