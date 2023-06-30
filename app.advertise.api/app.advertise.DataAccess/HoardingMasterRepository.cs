using app.advertise.DataAccess.Entities;
using app.advertise.libraries.Exceptions;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;

namespace app.advertise.DataAccess
{
    public interface IHoardingMasterRepository
    {
        Task InsertUpdate(DynamicParameters parameters);
        Task<IEnumerable<HoardingMaster>> GetAll();
        Task<HoardingMaster> GetById(int id);
        Task ModifyStatusById(HoardingMaster parameters);
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
            await connection.ExecuteAsync(Queries.SP_HoardTypeMaster_Ins, parameters, commandType: CommandType.StoredProcedure);

            var errorCode = parameters.Get<int?>("out_errorcode");
            var errorMsg = parameters.Get<string?>("out_errormsg");

            if (errorCode != 9999)
                throw new DBException(errorMsg, _logger);
        }

        public async Task<IEnumerable<HoardingMaster>> GetAll()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<HoardingMaster>(Queries.SelectAll_HoardingMaster) ?? Enumerable.Empty<HoardingMaster>();
        }

        public async Task<HoardingMaster> GetById(int id)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<HoardingMaster>(Queries.Select_byId_HoardingMaster, new { id }) ?? new HoardingMaster();
        }

        public async Task ModifyStatusById(HoardingMaster parameters)
        {
            using var connection = _context.CreateConnection();
            var rowsAffected = await connection.ExecuteAsync(Queries.ModifyStatus_HoardingMaster, parameters);

            if (!(rowsAffected > 0))
                throw new DBException("No rows updated", _logger);
        }
    }
}
