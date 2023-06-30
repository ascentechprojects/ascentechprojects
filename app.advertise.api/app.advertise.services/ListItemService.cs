using app.advertise.DataAccess;
using app.advertise.dtos;
using app.advertise.libraries;
using app.advertise.services.Interfaces;

namespace app.advertise.services
{
    public class ListItemService : IListItemService
    {
        private readonly IListItemRepository<dtoListItem> _listItemRepository;
        public ListItemService(IListItemRepository<dtoListItem> listItemRepository)
        {
            _listItemRepository = listItemRepository;
        }

        public async Task<IEnumerable<dtoListItem>> DisplayTypes() => await _listItemRepository.Items(ListItemEntity.DisplayType);
        public async Task<IEnumerable<dtoListItem>> Locations() => await _listItemRepository.Items(ListItemEntity.Location);
        public async Task<IEnumerable<dtoListItem>> Prabhags() => await _listItemRepository.Items(ListItemEntity.Prabhag);
        public async Task<IEnumerable<dtoListItem>> HoardingTypes() => await _listItemRepository.Items(ListItemEntity.HoardingType);

        public IEnumerable<dtoListItem> HoardingOwnerships() =>
             new List<dtoListItem>(){
                new (){Id="P",DisplayName="Private"},
                new (){Id="C",DisplayName="Corporation"}
                };

    }
}
