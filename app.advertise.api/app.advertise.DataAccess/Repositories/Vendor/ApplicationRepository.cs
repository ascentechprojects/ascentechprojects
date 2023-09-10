using app.advertise.DataAccess.ConnectionStrings;
using app.advertise.DataAccess.Entities.Vendor;
using app.advertise.libraries.Exceptions;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;

namespace app.advertise.DataAccess.Repositories.Vendor
{
    public interface IApplicationRepository
    {
        Task<IEnumerable<Application>> OpenApplications(DynamicParameters parameters);
        Task<Application> InserUpdateApplication(DynamicParameters parameters);
        Task<Application> ApplicationById(DynamicParameters parameters);
        Task<IEnumerable<Application>> AppCloseSearch(DynamicParameters parameters);
        Task<IEnumerable<Application>> ApplicationByIds(DynamicParameters parameters);
        Task CloseApplications(DynamicParameters parameters);
        Task<IEnumerable<Application>> ApplicationsByStatus(DynamicParameters parameters, bool all);
        Task<Application> ValidateAppById(DynamicParameters parameters, bool appNotNull = false);
        Task<Application> ViewApplicationById(DynamicParameters parameters);
    }
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly AdvertisementDbContext _context;
        private readonly ILogger<ApplicationRepository> _logger;
        public ApplicationRepository(AdvertisementDbContext context, ILogger<ApplicationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Application>> OpenApplications(DynamicParameters parameters)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Application>(Queries.Select_P_A_R_C_Applications, parameters) ?? Enumerable.Empty<Application>();
        }

        public async Task<IEnumerable<Application>> ApplicationsByStatus(DynamicParameters parameters, bool all)
        {
            using var connection = _context.CreateConnection();
            if (all)
                return await connection.QueryAsync<Application>(Queries.Select_P_A_R_C_Applications, parameters) ?? Enumerable.Empty<Application>();

            return await connection.QueryAsync<Application>(Queries.Select_P_A_R_Status_Applications, parameters) ?? Enumerable.Empty<Application>();
        }
        public async Task<Application> InserUpdateApplication1(DynamicParameters parameters)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Application>(Queries.Application_Details_By_AppliId, parameters) ?? new Application();
        }

        public async Task<Application> InserUpdateApplication(DynamicParameters parameters)
        {
            parameters.Add("out_appliid", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("out_applino", dbType: DbType.String, size: 50, direction: ParameterDirection.Output);
            parameters.Add("out_errorcode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("out_errormsg", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);


            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(Queries.SP_AOAD_APPLI_INS, parameters, commandType: CommandType.StoredProcedure);

            var appliId = parameters.Get<int>("out_appliid");
            var applino = parameters.Get<string>("out_applino");
            var errorcode = parameters.Get<int>("out_errorcode");
            var errormsg = parameters.Get<string>("out_errormsg");

            if (errorcode != 9999)
                throw new DBException($"Status:{errorcode}, Message:{errormsg} ", _logger);

            if (string.IsNullOrEmpty(applino) || !(appliId > 0))
                throw new DBException($"Invalid Application Number:{applino}, ApplicationId:{appliId} ", _logger);

            return new Application()
            {
                NUM_APPLI_ID = appliId,
                VAR_APPLI_APPLINO = applino
            };
        }

        public async Task<Application> ApplicationById(DynamicParameters parameters)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Application>(Queries.Appli_By_Id, parameters) ?? new Application();
        }

        public async Task<IEnumerable<Application>> AppCloseSearch(DynamicParameters parameters)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Application>(Queries.Select_App_Close_Search, parameters) ?? Enumerable.Empty<Application>();
        }

        public async Task<IEnumerable<Application>> ApplicationByIds(DynamicParameters parameters)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Application>(Queries.Select_multi_Applications, parameters) ?? Enumerable.Empty<Application>();
        }

        public async Task CloseApplications(DynamicParameters parameters)
        {
            parameters.Add("out_AppCloseID", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
            parameters.Add("out_errcode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("out_ErrMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: 200);

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(Queries.SP_AppliClose_Ins, parameters, commandType: CommandType.StoredProcedure);

            var closeIds = parameters.Get<string>("out_AppCloseID");
            var errorCode = parameters.Get<int>("out_errcode");
            var errorMsg = parameters.Get<string>("out_ErrMsg");

            if (errorCode != 9999)
                throw new DBException($"Failed to close the application, {errorMsg}", _logger);

            if (string.IsNullOrEmpty(closeIds))
                throw new DBException($"Failed to close the application", _logger);
        }

        public async Task<Application> ValidateAppById(DynamicParameters parameters, bool appNotNull)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Application>(appNotNull ? Queries.Validate_Appli_By_Id_Number : Queries.Validate_Appli_By_Id, parameters) ?? throw new DBException("No record found.", _logger);
        }

        public async Task<Application> ViewApplicationById(DynamicParameters parameters)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Application>(Queries.View_Appli_By_Id, parameters) ?? new Application();
        }
    }
}
