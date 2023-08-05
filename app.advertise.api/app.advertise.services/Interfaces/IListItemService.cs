﻿using app.advertise.dtos;

namespace app.advertise.services.Interfaces
{
    public interface IListItemService
    {
        Task<IEnumerable<dtoListItem>> DisplayTypes();
        Task<IEnumerable<dtoListItem>> Locations();
        Task<IEnumerable<dtoListItem>> Prabhags();
        Task<IEnumerable<dtoListItem>> HoardingTypes();
        IEnumerable<dtoListItem> HoardingOwnerships();

        Task<IEnumerable<dtoListItem>> LocationsByPrabhagId(int prabhagId);
        Task<IEnumerable<dtoListItem>> HordingByLocId(int locId);
        Task<IEnumerable<dtoListItem>> Corporations();
    }
}
