using app.advertise.dtos;
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
    public class ApplicationMasterController : APIControllerBase<ApplicationMasterController>
    {
        private readonly IApplicationMasterService _service;
        private readonly ILogger<ApplicationMasterController> _logger;
        public ApplicationMasterController(ILogger<ApplicationMasterController> logger, IInternalExceptionHandler internalExceptionHandler, IApplicationMasterService service) : base(logger, internalExceptionHandler)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        [Route("Search")]
        public async Task<IActionResult> AuthSearch(dtoApplicationAuthRequest dto)
        {
            try
            {
                var validator = new ApplicationAuthRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                    throw new FluentException(validationResult);


                
                return Ok(new ApiResponse<IEnumerable<dtoApplicationAuthResult>>
                {
                    Status = libraries.StatusCode.Ok,
                    Data= await _service.AuthSerach(dto)
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet]
        [Route("{id:int}/ById")]
        public async Task<IActionResult> ApplicationDetails(int id)
        {
            try
            {
                return Ok(new ApiResponse<dtoApplicationAuthResult>
                {
                    Status = libraries.StatusCode.Ok,
                    Data = await _service.AppliDetailsbyId(id)
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPost]
        [Route("UpdateStatus")]
        public async Task<IActionResult> UpdateRemark(dtoApplicationAuthRequest dto)
        {
            try
            {
                var validator = new ApplicationAuthRequestValidator(dto);
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                    throw new FluentException(validationResult);

               await _service.UpdateStatusFlag(dto);

                return Ok(new ApiResponse()
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
