using app.advertise.dtos.Admin;
using app.advertise.libraries;
using app.advertise.libraries.Interfaces;
using app.advertise.services.Admin.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace app.advertise.api.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class HordingTypeConfigController : APIControllerBase<HordingTypeConfigController>
    {
        private readonly IHordingTypeConfigService _hordingTypeConfigService;
        public HordingTypeConfigController(ILogger<HordingTypeConfigController> logger, IInternalExceptionHandler internalExceptionHandler, IHordingTypeConfigService hordingTypeConfigService) : base(logger, internalExceptionHandler)
        {
            _hordingTypeConfigService = hordingTypeConfigService;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(dtoHordingTypeConfig dtoHording)
        {
            try
            {
                await _hordingTypeConfigService.InsertUpdate(dtoHording, services.QueryExecutionMode.Insert);
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
        public async Task<IActionResult> Update(dtoHordingTypeConfig dtoHording)
        {
            try
            {
                await _hordingTypeConfigService.InsertUpdate(dtoHording, services.QueryExecutionMode.Update);
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
        [Route("GetActive")]
        public async Task<IActionResult> GetActiveConfigs()
        {
            try
            {
               var result= await _hordingTypeConfigService.GetActiveHoardingTypeConfigs();
                return Ok(new ApiResponse<IEnumerable<dtoHordingTypeConfig>>
                {
                    Status = libraries.StatusCode.Ok,
                    Data=result
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
                var result = await _hordingTypeConfigService.GetHoardingTypeConfigById(id);
                return Ok(new ApiResponse<dtoHordingTypeConfig>
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
