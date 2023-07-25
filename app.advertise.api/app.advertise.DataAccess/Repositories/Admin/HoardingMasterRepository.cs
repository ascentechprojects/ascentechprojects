using app.advertise.DataAccess.ConnectionStrings;
using app.advertise.DataAccess.Entities.Admin;
using app.advertise.libraries.Exceptions;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;

namespace app.advertise.DataAccess.Repositories.Admin
{
    public interface IHoardingMasterRepository
    {
        Task InsertUpdate(DynamicParameters parameters);
        Task<IEnumerable<HoardingMaster>> GetAll(int ulbId);
        Task<HoardingMaster> GetById(int id, int ulbId);
    }
    public class HoardingMasterRepository : IHoardingMasterRepository
    {
        private readonly AdvertisementDbContext _context;
        private readonly ILogger<HoardingMasterRepository> _logger;
        public HoardingMasterRepository(AdvertisementDbContext context, ILogger<HoardingMasterRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task InsertUpdate(DynamicParameters parameters)
        {
            parameters.Add("out_errorcode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("out_errormsg", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(Queries.SP_HoardingMaster_Ins, parameters, commandType: CommandType.StoredProcedure);

            var errorCode = parameters.Get<int?>("out_errorcode");
            var errorMsg = parameters.Get<string?>("out_errormsg");

            if (errorCode != 9999)
                throw new DBException(errorMsg, _logger);
        }

        public async Task<IEnumerable<HoardingMaster>> GetAll(int ulbId)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<HoardingMaster>(Queries.SelectAll_HoardingMaster, new { ulbId }) ?? Enumerable.Empty<HoardingMaster>();
        }

        public async Task<HoardingMaster> GetById(int id, int ulbId)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<HoardingMaster>(Queries.Select_byId_HoardingMaster, new { id, ulbId }) ?? new HoardingMaster();
        }

    }
}
