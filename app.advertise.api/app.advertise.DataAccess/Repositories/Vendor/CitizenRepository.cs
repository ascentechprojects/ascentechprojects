using app.advertise.DataAccess.ConnectionStrings;
using app.advertise.DataAccess.Entities.Vendor;
using app.advertise.libraries.Exceptions;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;

namespace app.advertise.DataAccess.Repositories.Vendor
{
    public interface ICitizenRepository
    {
        Task<CitizenUser> VerifyCitizen(DynamicParameters parameters);
        Task<CitizenUser> RegisterCitizen(DynamicParameters parameters);
        Task<CitizenUser> VerifyUserEmail(CitizenUser citizen);
        void UpdateUserOTP(CitizenUser citizen);
        CitizenUser VerifyUserOTP(CitizenUser citizen);
        Task UpdateUserPassword(CitizenUser citizen);
    }
    public class CitizenRepository : ICitizenRepository
    {
        private readonly AdvertisementDbContext _context;
        private readonly ILogger<CitizenRepository> _logger;
        public CitizenRepository(AdvertisementDbContext context, ILogger<CitizenRepository> logger)
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

        public async Task<CitizenUser> RegisterCitizen(DynamicParameters parameters)
        {
            parameters.Add("out_EmailLink", dbType: DbType.String, direction: ParameterDirection.Output);
            parameters.Add("out_errcode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("out_ErrMsg", dbType: DbType.String, direction: ParameterDirection.Output);

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(Queries.SP_CitzenRegistration_Ins, parameters, commandType: CommandType.StoredProcedure);

            var errcode = parameters.Get<int>("out_errcode");
            var errmsg = parameters.Get<string>("out_ErrMsg");

            if (errcode != 9999)
                throw new DBException(errmsg ?? "Error occured at DB config.", _logger);

            return new CitizenUser
            {
                VAR_CITIZENUSER_EMAILLINK = parameters.Get<string>("out_EmailLink"),
            };
        }

        public async Task<CitizenUser> VerifyUserEmail(CitizenUser citizen)
        {

            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<CitizenUser>(Queries.Select_Citizen_By_Email, citizen);
        }

        public void UpdateUserOTP(CitizenUser citizen)
        {
            using var connection = _context.CreateConnection();
            var affectedRow = connection.Execute(Queries.Update_User_OTP, citizen);

            if (affectedRow != 1)
                throw new DBException($"Failed to update verification key for {citizen.VAR_CITIZENUSER_EMAILID}", _logger);
        }

        public CitizenUser VerifyUserOTP(CitizenUser citizen)
        {

            using var connection = _context.CreateConnection();
            return  connection.QueryFirstOrDefault<CitizenUser>(Queries.Verify_User_OTP, citizen);
        }

        public async Task UpdateUserPassword(CitizenUser citizen)
        {
            using var connection = _context.CreateConnection();
            var affectedRow =await connection.ExecuteAsync(Queries.Update_User_Password, citizen);

            if (affectedRow != 1)
                throw new DBException($"Failed to reset password for {citizen.VAR_CITIZENUSER_EMAILID}", _logger);
        }
    }
}
