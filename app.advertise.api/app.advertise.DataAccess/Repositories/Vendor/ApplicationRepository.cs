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
        Task<IEnumerable<Application>> CloseApplications(IEnumerable<Application> applications, DynamicParameters parameters);
        Task<IEnumerable<Application>> ApplicationsByStatus(DynamicParameters parameters, bool all);
        Task<Application> ValidateAppById(DynamicParameters parameters,bool appNotNull=false);
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

        public async Task<IEnumerable<Application>> CloseApplications(IEnumerable<Application> applications, DynamicParameters parameters)
        {
            var result = new List<Application>();

            try
            {
                parameters.Add("out_AppCloseID", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
                parameters.Add("out_errorcode", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("out_errormsg", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

                using var connection = _context.CreateConnection();
                foreach (var app in applications)
                {
                    parameters.Add("IN_AppCloseID", app.num_appliclose_id, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("in_Holding", app.NUM_APPLI_HORDINGID, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("in_STR", app.VAR_APPLI_APPLINO, DbType.String, ParameterDirection.Input);

                    await connection.ExecuteAsync(Queries.SP_aoad_AppliClose_ins, parameters, commandType: CommandType.StoredProcedure);

                    var closeId = parameters.Get<string>("out_AppCloseID");
                    var errorCode = parameters.Get<int>("out_errcode");
                    var errorMsg = parameters.Get<string>("out_ErrMsg");
                    errorCode = 9999;
                    if (errorCode != 9999)
                        result.Add(new Application { VAR_APPLI_APPLINO = app.VAR_APPLI_APPLINO });
                }

            }
            catch (Exception ex)
            {
                throw new DBException($"Failed to close the application; {ex.Message}", _logger);
            }

            return result;

        }

        public async Task<Application> ValidateAppById(DynamicParameters parameters,bool appNotNull)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Application>(appNotNull? Queries.Validate_Appli_By_Id_Number: Queries.Validate_Appli_By_Id, parameters) ?? new Application();
        }
    }
}
