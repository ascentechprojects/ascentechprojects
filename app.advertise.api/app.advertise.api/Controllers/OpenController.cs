using app.advertise.dtos;
using app.advertise.libraries;
using app.advertise.libraries.Exceptions;
using app.advertise.libraries.Interfaces;
using app.advertise.services.Vendor.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace app.advertise.api.Controllers
{
    [Route("api")]
    [ApiController]
    public class OpenController : APIControllerBase<OpenController>
    {
        private readonly IApplicationService _service;
        private readonly ILogger<OpenController> _logger;
        public OpenController(ILogger<OpenController> logger, IInternalExceptionHandler internalExceptionHandler, IApplicationService service) : base(logger, internalExceptionHandler)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [Route("{id}/Validate")]
        public async Task<IActionResult> ValidateApplication(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new ApiException("Invalid request.", _logger);

                return Ok(new ApiResponse<dtoAppTemplate>
                {
                    Status = libraries.StatusCode.Ok,
                    Data = await _service.ValidateApplication(id)
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex, $"Id:{id}");
            }
        }


        [HttpGet]
        [Route("{id}/{appno}/Validate")]
        public async Task<IActionResult> ValidateApplication(string id, string appno)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(appno))
                    throw new ApiException("Invalid request.", _logger);

                return Ok(new ApiResponse<dtoAppTemplate>
                {
                    Status = libraries.StatusCode.Ok,
                    Data = await _service.ValidateApplication(id, appno)
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex, $"Id:{id}, App:{appno}");
            }
        }
    }
}
