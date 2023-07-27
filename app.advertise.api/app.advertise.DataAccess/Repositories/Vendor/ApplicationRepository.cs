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
            return await connection.QueryAsync<Application>(Queries.Select_P_A_R_Applications, parameters) ?? Enumerable.Empty<Application>();
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
                VAR_APPLI_APPLINO=applino
            };
        }

        public async Task<Application> ApplicationById(DynamicParameters parameters)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Application>(Queries.Appli_By_Id, parameters) ?? new Application();
        }
    }
}
