using app.advertise.DataAccess.Entities;
using app.advertise.libraries.Exceptions;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;

namespace app.advertise.DataAccess
{
    public interface IHoardingtypeMasterRepository
    {
        Task InsertUpdate(DynamicParameters parameters);
        Task<IEnumerable<HoardingtypeMaster>> GetAll();
        Task<HoardingtypeMaster> GetById(int id);
    }
    public class HoardingtypeMasterRepository : IHoardingtypeMasterRepository
    {
        private readonly AdvertisementDbContext _context;
        private readonly ILogger<HoardingtypeMasterRepository> _logger;
        public HoardingtypeMasterRepository(AdvertisementDbContext context, ILogger<HoardingtypeMasterRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task InsertUpdate(DynamicParameters parameters)
        {
            parameters.Add("out_Errorcode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("out_Errormsg", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(Queries.SP_HoardTypeMaster_Ins, parameters, commandType: CommandType.StoredProcedure);

            var errorCode = parameters.Get<int?>("out_Errorcode");
            var errorMsg = parameters.Get<string?>("out_Errormsg");

            if (errorCode != 9999)
                throw new DBException(errorMsg, _logger);
        }

        public async Task<IEnumerable<HoardingtypeMaster>> GetAll()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<HoardingtypeMaster>(Queries.List_HOARDINGTYPE_MST) ?? Enumerable.Empty<HoardingtypeMaster>();
        }

        public async Task<HoardingtypeMaster> GetById(int id)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<HoardingtypeMaster>(Queries.Single_HOARDINGTYPE_MST, new { id }) ?? new HoardingtypeMaster();
        }
    }
}
