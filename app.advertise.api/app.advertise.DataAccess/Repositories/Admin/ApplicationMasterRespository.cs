using app.advertise.DataAccess.ConnectionStrings;
using app.advertise.DataAccess.Entities.Admin;
using app.advertise.libraries.Exceptions;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;

namespace app.advertise.DataAccess.Repositories.Admin
{
    public interface IApplicationMasterRespository
    {
        Task<IEnumerable<Application>> AuthSearch(DynamicParameters parameters);
        Task<Application> ApplicationById(DynamicParameters parameters);
        Task UpdateAppliStatus(DynamicParameters parameters);
        Task<IEnumerable<Application>> DeauthSearch(DynamicParameters parameters);
        Task<ApplicationClose> DeauthAppliStatus(DynamicParameters parameters);
        Task<ApplicationClose> ApplicationCloseByAppId(ApplicationClose parameters);
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


        public async Task<IEnumerable<Application>> AuthSearch(DynamicParameters parameters)
        {

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Application>(Queries.Application_Auth_Search, parameters) ?? Enumerable.Empty<Application>();
        }

        public async Task<IEnumerable<Application>> DeauthSearch(DynamicParameters parameters)
        {

            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Application>(Queries.Application_Deauth_Search, parameters) ?? Enumerable.Empty<Application>();
        }

        public async Task<Application> ApplicationById(DynamicParameters parameters)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Application>(Queries.Application_Details_By_AppliId, parameters) ?? new Application();
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

        public async Task<ApplicationClose> DeauthAppliStatus(DynamicParameters parameters)
        {
            parameters.Add("out_errcode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("out_ErrMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);
            parameters.Add("out_AppCloseID", dbType: DbType.String, direction: ParameterDirection.Output, size: 2000);

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(Queries.SP_AppliClose_Ins, parameters, commandType: CommandType.StoredProcedure);

            var errorCode = parameters.Get<int?>("out_errcode");
            var errorMsg = parameters.Get<string?>("out_ErrMsg");

            if (errorCode != 9999)
                throw new DBException($"Status:{errorCode}, Message:{errorMsg} ", _logger);

            return new ApplicationClose() { VAR_APPLICLOSE_ID = parameters.Get<string?>("out_AppCloseID") };
        }

        public async Task<ApplicationClose> ApplicationCloseByAppId(ApplicationClose parameters)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<ApplicationClose>(Queries.ApplicationClose_By_AppId, parameters) ?? new ApplicationClose();
        }
    }

}

