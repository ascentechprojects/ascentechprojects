using app.advertise.libraries;
using app.advertise.libraries.Exceptions;
using Dapper;
using Microsoft.Extensions.Logging;

namespace app.advertise.DataAccess
{
    public interface IUpdateStatusRespository
    {
        Task UpdateStatus(EntityType entityType, object parameters);
    }
    public class UpdateStatusRespository : IUpdateStatusRespository
    {
        private readonly ILogger<UpdateStatusRespository> _logger;
        private readonly AdvertisementDbContext _context;
        public UpdateStatusRespository(ILogger<UpdateStatusRespository> logger, AdvertisementDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task UpdateStatus(EntityType entityType, object parameters)
        {
            using var connection = _context.CreateConnection();
            var rowsAffected = await connection.ExecuteAsync(QueryByEntityType(entityType), parameters);

            if (!(rowsAffected > 0))
                throw new DBException(AppConstants.Msg_RecordNotFound, _logger);
        }

        private string QueryByEntityType(EntityType entityType) =>

             entityType switch
             {
                 EntityType.HoardingMaster => Queries.ModifyStatus_HoardingMaster,
                 EntityType.LocationMaster => Queries.ModifyStatus_LocationMaster,
                 EntityType.DisplayTypeMaster => Queries.ModifyStatus_DisplayType_MST,
                 _ => throw new DBException($"{AppConstants.Msg_InvalidEntityType} {entityType}", _logger)
             };

    }
}
