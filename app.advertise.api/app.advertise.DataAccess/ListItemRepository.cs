using app.advertise.DataAccess.ConnectionStrings;
using app.advertise.libraries;
using app.advertise.libraries.Exceptions;
using Dapper;
using Microsoft.Extensions.Logging;

namespace app.advertise.DataAccess
{
    public interface IListItemRepository<T>
    {
        Task<IEnumerable<T>> Items(ListItemEntity entityType, DynamicParameters parameters = null);
    }
    public class ListItemRepository<T> : IListItemRepository<T>
    {
        private readonly AdvertisementDbContext _context;
        private readonly ILogger<ListItemRepository<T>> _logger;
        public ListItemRepository(AdvertisementDbContext context, ILogger<ListItemRepository<T>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<T>> Items(ListItemEntity entityType, DynamicParameters parameters)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<T>(QueryEntityType(entityType), parameters);
        }

        private string QueryEntityType(ListItemEntity entityType) =>

             entityType switch
             {
                 ListItemEntity.DisplayType => Queries.ListItem_DisplayTypes,
                 ListItemEntity.Location => Queries.ListItem_Locations,
                 ListItemEntity.Prabhag => Queries.ListItem_Prabhags,
                 ListItemEntity.HoardingType => Queries.ListItem_HordingTypes,
                 ListItemEntity.LocationByPrabhag => Queries.ListItem_Locations_By_PrabhagId,
                 _ => throw new DBException($"{AppConstants.Msg_InvalidEntityType} {entityType}", _logger)
             };

    }
}
