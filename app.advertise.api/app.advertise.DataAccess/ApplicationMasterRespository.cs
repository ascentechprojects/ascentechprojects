using app.advertise.DataAccess.Entities;
using app.advertise.libraries.Exceptions;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;

namespace app.advertise.DataAccess
{
    public interface IApplicationMasterRespository
    {
        Task<IEnumerable<ApplicationAuthSearch>> AuthSearch(DynamicParameters parameters);
        Task<ApplicationAuthSearch> ApplicationById(DynamicParameters parameters);
        Task UpdateAppliStatus(DynamicParameters parameters);
        Task<IEnumerable<ApplicationAuthSearch>> DeauthSearch(DynamicParameters parameters);
        Task DeauthAppliStatus(DynamicParameters parameters);
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

        public async Task<IEnumerable<ApplicationAuthSearch>> DeauthSearch(DynamicParameters parameters)
        {

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<ApplicationAuthSearch>(Queries.Application_Deauth_Search, parameters) ?? Enumerable.Empty<ApplicationAuthSearch>();
        }

        public async Task<ApplicationAuthSearch> ApplicationById(DynamicParameters parameters)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstAsync<ApplicationAuthSearch>(Queries.Application_Details_By_AppliId, parameters) ?? new ApplicationAuthSearch();
        }

        public async Task UpdateAppliStatus(DynamicParameters parameters)
        {
            parameters.Add("out_errcode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("out_ErrMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);


            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(Queries.SP_AppliCloseAuth_Ins, parameters, commandType: CommandType.StoredProcedure);

            var errorCode = parameters.Get<int?>("out_errcode");
            var errorMsg = parameters.Get<string?>("out_ErrMsg");

            if (errorCode != 9999)
                throw new DBException($"Status:{errorCode}, Message:{errorMsg} ", _logger);
        }

        public async Task DeauthAppliStatus(DynamicParameters parameters)
        {
            parameters.Add("out_errcode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("out_ErrMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);


            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(Queries.SP_AppliClose_Ins, parameters, commandType: CommandType.StoredProcedure);

            var errorCode = parameters.Get<int?>("out_errcode");
            var errorMsg = parameters.Get<string?>("out_ErrMsg");

            if (errorCode != 9999)
                throw new DBException($"Status:{errorCode}, Message:{errorMsg} ", _logger);
        }
    }

}

