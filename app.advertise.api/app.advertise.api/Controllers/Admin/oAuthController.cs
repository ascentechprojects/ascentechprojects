using app.advertise.dtos.Admin;
using app.advertise.dtos.Admin.Validators;
using app.advertise.libraries;
using app.advertise.libraries.Exceptions;
using app.advertise.libraries.Interfaces;
using app.advertise.services.Admin.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace app.advertise.api.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class oAuthController : APIControllerBase<oAuthController>
    {
        private readonly IOAuthService _oAuthService;
        public oAuthController(ILogger<oAuthController> logger, IInternalExceptionHandler internalExceptionHandler, IOAuthService oAuthService) :base(logger,internalExceptionHandler)
        {
            _oAuthService = oAuthService;
        }
        
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AuthUser(dtoAuthRequest authRequest)
        {
            
            try
            {
                var validator = new AuthRequestValidator();
                var validationResult = validator.Validate(authRequest);

                if (!validationResult.IsValid)
                    throw new FluentException(validationResult);


                return Ok(new ApiResponse<dtoAuthResponse>
                {
                    Data =await _oAuthService.AuthenticateUser(authRequest),
                    Status =libraries.StatusCode.Ok,
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex, authRequest);
            }
        }
    }
}
