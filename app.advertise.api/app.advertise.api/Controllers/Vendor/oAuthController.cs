﻿using app.advertise.dtos.Vendor;
using app.advertise.dtos.Vendor.Validators;
using app.advertise.libraries;
using app.advertise.libraries.Exceptions;
using app.advertise.libraries.Interfaces;
using app.advertise.services.Vendor.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace app.advertise.api.Controllers.Vendor
{
    [Route("api/vendor/[controller]")]
    [ApiController]
    public class oAuthController : APIControllerBase<oAuthController>
    {
        private readonly ILogger<oAuthController> _logger;
        private readonly IoAuthService _authService;
        public oAuthController(IoAuthService authService, ILogger<oAuthController> logger, IInternalExceptionHandler internalExceptionHandler) : base(logger, internalExceptionHandler)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost]
        [Route("Verify")]
        public async Task<IActionResult> Add(dtoCitizenLoginRequest dto)
        {
            try
            {
                if (dto == null)
                    throw new ApiException("Invalid input request", _logger);


                var validator = new CitizenLoginRequestValidator();
                var validationResult = validator.Validate(dto);

                if (!validationResult.IsValid)
                    throw new FluentException(validationResult);


                return Ok(new ApiResponse<dtoCitizenLoginResponse>
                {
                    Status = libraries.StatusCode.Ok,
                    Data = await _authService.VerifyCitizen(dto)
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex, dto);
            }
        }
    }
}