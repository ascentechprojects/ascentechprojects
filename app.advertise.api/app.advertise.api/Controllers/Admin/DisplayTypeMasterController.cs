using app.advertise.dtos.Admin;
using app.advertise.dtos.Admin.Validators;
using app.advertise.libraries;
using app.advertise.libraries.Exceptions;
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
                var validator = new DisplayTypeMasterValidator(QueryExecutionMode.Insert);
                var validationResult = validator.Validate(dtoMaster);

                if (!validationResult.IsValid)
                    throw new FluentException(validationResult);

                await _displayTypeMasterService.Insert(dtoMaster);
                return Ok(new ApiResponse
                {
                    Status = libraries.StatusCode.Ok,
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex, dtoMaster);
            }
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(dtoDisplayTypeMaster dtoMaster)
        {
            try
            {
                var validator = new DisplayTypeMasterValidator(QueryExecutionMode.Update);
                var validationResult = validator.Validate(dtoMaster);

                if (!validationResult.IsValid)
                    throw new FluentException(validationResult);

                await _displayTypeMasterService.Update(dtoMaster);
                return Ok(new ApiResponse
                {
                    Status = libraries.StatusCode.Ok
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex, dtoMaster);
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(new ApiResponse<IEnumerable<dtoDisplayTypeMaster>>
                {
                    Status = libraries.StatusCode.Ok,
                    Data = await _displayTypeMasterService.GetAll()
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet]
        [Route("{id}/ById")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                return Ok(new ApiResponse<dtoDisplayTypeMaster>
                {
                    Status = libraries.StatusCode.Ok,
                    Data = await _displayTypeMasterService.GetById(id)
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPost]
        [Route("{id:int}/toggleStatus")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            try
            {
                await _displayTypeMasterService.ModifyStatusById(id);
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
