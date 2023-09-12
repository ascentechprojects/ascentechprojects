using app.advertise.DataAccess;
using app.advertise.dtos;
using app.advertise.libraries;
using app.advertise.services.Interfaces;
using Dapper;

namespace app.advertise.services
{
    public class ListItemService : IListItemService
    {
        private readonly IListItemRepository<dtoListItem> _listItemRepository;
        private readonly UserRequestHeaders _authData;
        public ListItemService(IListItemRepository<dtoListItem> listItemRepository, UserRequestHeaders authData)
        {
            _listItemRepository = listItemRepository;
            _authData = authData;
        }

        public async Task<IEnumerable<dtoListItem>> DisplayTypes() => await _listItemRepository.Items(ListItemEntity.DisplayType);
        public async Task<IEnumerable<dtoListItem>> Locations()
        {
            var parameters = new DynamicParameters();
            parameters.Add("ulbId", _authData.UlbId);
            return await _listItemRepository.Items(ListItemEntity.Location, parameters);
        }
        public async Task<IEnumerable<dtoListItem>> Prabhags()
        {
            var parameters = new DynamicParameters();
            parameters.Add("ulbId", _authData.UlbId);
            return await _listItemRepository.Items(ListItemEntity.Prabhag, parameters);
        }
        public async Task<IEnumerable<dtoListItem>> HoardingTypes() => await _listItemRepository.Items(ListItemEntity.HoardingType);

        public IEnumerable<dtoListItem> HoardingOwnerships() => StaticHelpers.HoardingOwnerships().Select(kv => new dtoListItem { Id = kv.Key, DisplayName = kv.Value });

        public async Task<IEnumerable<dtoListItem>> LocationsByPrabhagId(int prabhagId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("prabhagId", prabhagId);
            parameters.Add("ulbId", _authData.UlbId);
            return await _listItemRepository.Items(ListItemEntity.LocationByPrabhag, parameters);
        }

        public async Task<IEnumerable<dtoListItem>> HordingByLocId(int locId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("locationid", locId);
            parameters.Add("ulbId", _authData.UlbId);
            return await _listItemRepository.Items(ListItemEntity.HordingByLocId, parameters);
        }

        public async Task<IEnumerable<dtoListItem>> Corporations() => await _listItemRepository.Items(ListItemEntity.Corporation);

        public async Task<IEnumerable<dtoListItem>> LocationsByPrabhagId(int prabhagId, int ulbId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("prabhagId", prabhagId);
            parameters.Add("ulbId", ulbId);
            return await _listItemRepository.Items(ListItemEntity.LocationByPrabhag, parameters);
        }

        public async Task<IEnumerable<dtoListItem>> HordingByLocId(int locId,int ulbId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("locationid", locId);
            parameters.Add("ulbId", ulbId);
            return await _listItemRepository.Items(ListItemEntity.HordingByLocId, parameters);
        }
    }
}
