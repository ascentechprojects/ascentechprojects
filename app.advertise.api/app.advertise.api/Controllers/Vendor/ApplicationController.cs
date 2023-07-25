using app.advertise.dtos.Vendor;
using app.advertise.dtos.Vendor.Validators;
using app.advertise.libraries;
using app.advertise.libraries.Exceptions;
using app.advertise.libraries.Interfaces;
using app.advertise.services.Admin.Interfaces;
using app.advertise.services.Vendor.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace app.advertise.api.Controllers.Vendor
{
    [Route("api/vendor/[controller]")]
    [ApiController]
    public class ApplicationController : APIControllerBase<ApplicationController>
    {
        private readonly IApplicationService _service;
        private readonly ILogger<ApplicationController> _logger;
        private readonly IHoardingMasterService _hoardingMasterService;
        public ApplicationController(ILogger<ApplicationController> logger, IInternalExceptionHandler internalExceptionHandler, IApplicationService service, IHoardingMasterService hoardingMasterService) : base(logger, internalExceptionHandler)
        {
            _service = service;
            _logger = logger;
           _hoardingMasterService = hoardingMasterService;
        }
        [HttpGet]
        [Route("OpenApps")]
        public async Task<IActionResult> OpenApps()
        {
            try
            {
                return Ok(new ApiResponse<IEnumerable<dtoApplication>>
                {
                    Status = libraries.StatusCode.Ok,
                    Data = await _service.OpenApplications()
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet]
        [Route("{recordId:int}/Hording")]
        public async Task<IActionResult> HordingDetail(int recordId)
        {
            try
            {
                return Ok(new ApiResponse<dtos.Admin.dtoHoardingMaster>
                {
                    Status = libraries.StatusCode.Ok,
                    Data = await _hoardingMasterService.GetById(recordId)
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPost("addApp")]
        public async Task<IActionResult> AddApp([FromForm] dtoApplicationDetails dto)
        {

            var validator = new ApplicationDetailsValidator();
            var validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
                throw new FluentException(validationResult);


            var file = Request.Form.Files["File"];
            var fileValidator=new FormFileValidator();
            var fileValidationResult = fileValidator.Validate(file);
            if (!fileValidationResult.IsValid)
                throw new FluentException(fileValidationResult);

            try
            {
                return Ok(new ApiResponse<dtoApplicationDetails>
                {
                    Status = libraries.StatusCode.Ok,
                    Data = await _service.AddApplication(dto, file)
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}
