using app.advertise.dtos;
using app.advertise.libraries;
using app.advertise.libraries.Interfaces;
using app.advertise.services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace app.advertise.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListItemController : APIControllerBase<ListItemController>
    {
        private readonly IListItemService _listItemService;
        public ListItemController(ILogger<ListItemController> logger, IInternalExceptionHandler internalExceptionHandler, IListItemService listItemService) : base(logger, internalExceptionHandler)
        {
            _listItemService = listItemService;
        }

        [HttpGet]
        [Route("Prabhags")]
        public async Task<IActionResult> Prabhags()
        {
            try
            {
                return Ok(new ApiResponse<IEnumerable<dtoListItem>>
                {
                    Status = libraries.StatusCode.Ok,
                    Data = await _listItemService.Prabhags()
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet]
        [Route("DisplayTypes")]
        public async Task<IActionResult> DisplayTypes()
        {
            try
            {
                return Ok(new ApiResponse<IEnumerable<dtoListItem>>
                {
                    Status = libraries.StatusCode.Ok,
                    Data = await _listItemService.DisplayTypes()
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet]
        [Route("Locations")]
        public async Task<IActionResult> Locations()
        {
            try
            {
                return Ok(new ApiResponse<IEnumerable<dtoListItem>>
                {
                    Status = libraries.StatusCode.Ok,
                    Data = await _listItemService.Locations()
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet]
        [Route("HoardingTypes")]
        public async Task<IActionResult> HoardingTypes()
        {
            try
            {
                return Ok(new ApiResponse<IEnumerable<dtoListItem>>
                {
                    Status = libraries.StatusCode.Ok,
                    Data = await _listItemService.HoardingTypes()
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet]
        [Route("HoardingOwnerships")]
        public IActionResult HoardingOwnerships()
        {
            try
            {
                return Ok(new ApiResponse<IEnumerable<dtoListItem>>
                {
                    Status = libraries.StatusCode.Ok,
                    Data = _listItemService.HoardingOwnerships()
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet]
        [Route("{prabhagId:int}/PrabhaLocations")]
        public async Task<IActionResult> LocationsByPrabhagId(int prabhagId)
        {
            try
            {
                return Ok(new ApiResponse<IEnumerable<dtoListItem>>
                {
                    Status = libraries.StatusCode.Ok,
                    Data = await _listItemService.LocationsByPrabhagId(prabhagId)
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}
