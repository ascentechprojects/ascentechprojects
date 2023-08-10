using app.advertise.dtos.Vendor;
using app.advertise.libraries;
using app.advertise.libraries.Interfaces;
using app.advertise.services.Vendor.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace app.advertise.api.Controllers.Vendor
{
    [Route("api/vendor/[controller]")]
    [ApiController]
    public class DashboardController : APIControllerBase<DashboardController>
    {
        private readonly IDashboardService _service;
        public DashboardController(ILogger<DashboardController> logger, IInternalExceptionHandler internalExceptionHandler, IDashboardService service) : base(logger, internalExceptionHandler)
        {
            _service = service;
        }


        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Data()
        {
            try
            {
                return Ok(new ApiResponse<dtoDashboard>
                {
                    Status = libraries.StatusCode.Ok,
                    Data = await _service.DashboardDetails()
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

    }
}
