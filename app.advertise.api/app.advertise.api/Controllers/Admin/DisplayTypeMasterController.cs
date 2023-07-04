using app.advertise.dtos.Admin;
using app.advertise.libraries;
using app.advertise.libraries.Interfaces;
using app.advertise.services.Admin.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace app.advertise.api.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class DisplayTypeMasterController : APIControllerBase<DisplayTypeMasterController>
    {
        private readonly IDisplayTypeMasterService _displayTypeMasterService;
        public DisplayTypeMasterController(ILogger<DisplayTypeMasterController> logger, IInternalExceptionHandler internalExceptionHandler, IDisplayTypeMasterService displayTypeMasterService) : base(logger, internalExceptionHandler)
        {
            _displayTypeMasterService = displayTypeMasterService;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(dtoDisplayTypeMaster dtoMaster)
        {
            try
            {
                await _displayTypeMasterService.InsertUpdate(dtoMaster, services.QueryExecutionMode.Insert);
                return Ok(new ApiResponse
                {
                    Status = libraries.StatusCode.Ok,
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(dtoDisplayTypeMaster dtoMaster)
        {
            try
            {
                await _displayTypeMasterService.InsertUpdate(dtoMaster, services.QueryExecutionMode.Update);
                return Ok(new ApiResponse
                {
                    Status = libraries.StatusCode.Ok,
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _displayTypeMasterService.GetAll();
                return Ok(new ApiResponse<IEnumerable<dtoDisplayTypeMaster>>
                {
                    Status = libraries.StatusCode.Ok,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet]
        [Route("{id:int}/ById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _displayTypeMasterService.GetById(id);
                return Ok(new ApiResponse<dtoDisplayTypeMaster>
                {
                    Status = libraries.StatusCode.Ok,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPost]
        [Route("{id:int}/Deactivate")]
        public async Task<IActionResult> Deactivate(int id)
        {
            try
            {
                await _displayTypeMasterService.ModifyStatusById(id, RecordStatus.I.ToString());
                return Ok(new ApiResponse
                {
                    Status = libraries.StatusCode.Ok,
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }


        [HttpGet]
        [Route("Active")]
        public async Task<IActionResult> ActiveDisplayTypes()
        {
            try
            {
                var result = await _displayTypeMasterService.ActiveDisplayTypes();
                return Ok(new ApiResponse<IEnumerable<dtoDisplayTypeMaster>>
                {
                    Status = libraries.StatusCode.Ok,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet]
        [Route("{displayConfigUlbId:int}/ExistsInConfig")]
        public async Task<IActionResult> ActiveDisplayExistsInConfig(int displayConfigUlbId)
        {
            try
            {
                var result = await _displayTypeMasterService.DisplayTypesExistsInConfig(displayConfigUlbId);
                return Ok(new ApiResponse<IEnumerable<dtoDisplayTypeMaster>>
                {
                    Status = libraries.StatusCode.Ok,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }


        [HttpPost]
        [Route("Config")]
        public async Task<IActionResult> Configuration(IEnumerable<dtoDisplayTypeMaster> dtoDisplayTypeMasters)
        {
            try
            {
                await _displayTypeMasterService.AddUpdateDisplayConfig(dtoDisplayTypeMasters);
                return Ok(new ApiResponse
                {
                    Status = libraries.StatusCode.Ok,
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}
