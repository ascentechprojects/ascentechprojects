using app.advertise.DataAccess.ConnectionStrings;
using app.advertise.DataAccess.Entities.Vendor;
using app.advertise.libraries.Exceptions;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;

namespace app.advertise.DataAccess.Repositories.Vendor
{
    public interface IoAuthRepository
    {
        Task<CitizenUser> VerifyCitizen(DynamicParameters parameters);
    }
    public class oAuthRepository : IoAuthRepository
    {
        private readonly AdvertisementDbContext _context;
        private readonly ILogger<oAuthRepository> _logger;
        public oAuthRepository(AdvertisementDbContext context, ILogger<oAuthRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<CitizenUser> VerifyCitizen(DynamicParameters parameters)
        {
            parameters.Add("out_ulbid", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("out_userId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("out_userFullName", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
            parameters.Add("out_corporationName", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);
            parameters.Add("out_corporationAddr", dbType: DbType.String, direction: ParameterDirection.Output, size: 1000);
            parameters.Add("out_errcode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("out_errmsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 1000);

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(Queries.SP_Citzenlogin_Ins, parameters, commandType: CommandType.StoredProcedure);

            var errorCode = parameters.Get<int?>("out_errcode");
            var errorMsg = parameters.Get<string?>("out_errmsg");

            if (errorCode != 9999)
                throw new DBException(errorMsg ?? "Error occured at DB config.", _logger);

            return new CitizenUser
            {
                NUM_CITIZENUSER_ULBID = parameters.Get<int>("out_ulbid"),
                NUM_CITIZENUSER_USERID = parameters.Get<int>("out_userId"),
                VAR_CITIZENUSER_NAME = parameters.Get<string>("out_userFullName"),
                VAR_CORPORATION_NAME = parameters.Get<string>("out_corporationName"),
                VAR_CORPORATION_ADDRESS = parameters.Get<string>("out_corporationAddr")
            }; 
        }
    }
}
