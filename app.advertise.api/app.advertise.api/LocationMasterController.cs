using app.advertise.dtos.Admin;
using app.advertise.dtos.Admin.Validators;
using app.advertise.libraries;
using app.advertise.libraries.Exceptions;
using app.advertise.libraries.Interfaces;
using app.advertise.services.Admin.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace app.advertise.api
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class LocationMasterController : APIControllerBase<LocationMasterController>
    {
        private readonly ILocationMasterService _locationMasterService;
        public LocationMasterController(ILogger<LocationMasterController> logger, IInternalExceptionHandler internalExceptionHandler, ILocationMasterService locationMasterService) : base(logger, internalExceptionHandler)
        {
            _locationMasterService = locationMasterService;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(dtoLocationMaster dto)
        {
            try
            {
                var validator = new LocationMasterValidator(QueryExecutionMode.Insert);
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                    throw new FluentException(validationResult);

                await _locationMasterService.Insert(dto);
                return Ok(new ApiResponse
                {
                    Status = libraries.StatusCode.Ok,
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex, dto);
            }
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(dtoLocationMaster dtoMaster)
        {
            try
            {
                var validator = new LocationMasterValidator(QueryExecutionMode.Update);
                var validationResult = validator.Validate(dtoMaster);

                if (!validationResult.IsValid)
                    throw new FluentException(validationResult);

                await _locationMasterService.Update(dtoMaster);
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

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(new ApiResponse<IEnumerable<dtoLocationMaster>>
                {
                    Status = libraries.StatusCode.Ok,
                    Data = await _locationMasterService.GetAll()
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet]
        [Route("{id}/GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                return Ok(new ApiResponse<dtoLocationMaster>
                {
                    Status = libraries.StatusCode.Ok,
                    Data = await _locationMasterService.GetById(id)
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPost]
        [Route("{id:int}/toggleStatus")]
        public async Task<IActionResult> Deactivate(int id)
        {
            try
            {
                await _locationMasterService.ModifyStatusById(id);
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
