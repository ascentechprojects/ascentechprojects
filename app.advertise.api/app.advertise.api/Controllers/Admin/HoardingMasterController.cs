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
    public class HoardingMasterController : APIControllerBase<HoardingMasterController>
    {
        private readonly IHoardingMasterService _hoardingMasterService;
        private readonly ILogger<HoardingMasterController> _logger;
        public HoardingMasterController(ILogger<HoardingMasterController> logger, IInternalExceptionHandler internalExceptionHandler, IHoardingMasterService hoardingMasterService) : base(logger, internalExceptionHandler)
        {
            _hoardingMasterService = hoardingMasterService;
            _logger = logger;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(dtoHoardingMaster dtoHording)
        {
            try
            {
                var validator = new HoardingMasterValidator(QueryExecutionMode.Insert);
                var validationResult = validator.Validate(dtoHording);

                if (!validationResult.IsValid)
                    throw new FluentException(validationResult);


                await _hoardingMasterService.Insert(dtoHording);
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
        public async Task<IActionResult> Update(dtoHoardingMaster dtoHording)
        {
            try
            {
                var validator = new HoardingMasterValidator(QueryExecutionMode.Update);
                var validationResult = validator.Validate(dtoHording);

                if (!validationResult.IsValid)
                    throw new FluentException(validationResult);

                await _hoardingMasterService.Update(dtoHording);
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
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _hoardingMasterService.GetAll();
                return Ok(new ApiResponse<IEnumerable<dtoHoardingMaster>>
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

        [HttpGet]
        [Route("{id}/ById")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await _hoardingMasterService.GetById(id);
                return Ok(new ApiResponse<dtoHoardingMaster>
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
        [Route("{id:int}/toggleStatus")]
        public async Task<IActionResult> Deactivate(int id)
        {
            try
            {
                await _hoardingMasterService.ModifyStatusById(id);
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
