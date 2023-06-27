using app.advertise.api.Controllers.Admin;
using app.advertise.dtos.Admin;
using app.advertise.libraries;
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
        public async Task<IActionResult> Add(dtoLocationMaster dtoMaster)
        {
            try
            {
                await _locationMasterService.InsertUpdate(dtoMaster, services.QueryExecutionMode.Insert);
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
        public async Task<IActionResult> Update(dtoLocationMaster dtoMaster)
        {
            try
            {
                await _locationMasterService.InsertUpdate(dtoMaster, services.QueryExecutionMode.Update);
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
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _locationMasterService.GetAll();
                return Ok(new ApiResponse<IEnumerable<dtoLocationMaster>>
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
        [Route("{id:int}/GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _locationMasterService.GetById(id);
                return Ok(new ApiResponse<dtoLocationMaster>
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
    }
}
