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
    public class HoardingtypeMasterController : APIControllerBase<HoardingtypeMasterController>
    {
        private readonly IHoardingtypeMasterService _hoardingtypeMasterService;
        public HoardingtypeMasterController(ILogger<HoardingtypeMasterController> logger, IInternalExceptionHandler internalExceptionHandler, IHoardingtypeMasterService hoardingtypeMasterService) : base(logger, internalExceptionHandler)
        {
            _hoardingtypeMasterService = hoardingtypeMasterService;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(dtoHoardingtypeMaster dtoHording)
        {
            try
            {
                var validator = new HoardingTypeMasterValidator(QueryExecutionMode.Insert);
                var validationResult = validator.Validate(dtoHording);

                if (!validationResult.IsValid)
                    throw new FluentException(validationResult);

                await _hoardingtypeMasterService.Insert(dtoHording);
                return Ok(new ApiResponse
                {
                    Status = libraries.StatusCode.Ok,
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex, dtoHording);
            }
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(dtoHoardingtypeMaster dtoHording)
        {
            try
            {
                var validator = new HoardingTypeMasterValidator(QueryExecutionMode.Update);
                var validationResult = validator.Validate(dtoHording);

                if (!validationResult.IsValid)
                    throw new FluentException(validationResult);

                await _hoardingtypeMasterService.Update(dtoHording);
                
                return Ok(new ApiResponse
                {
                    Status = libraries.StatusCode.Ok,
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex, dtoHording);
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(new ApiResponse<IEnumerable<dtoHoardingtypeMaster>>
                {
                    Status = libraries.StatusCode.Ok,
                    Data = await _hoardingtypeMasterService.GetAll()
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
                return Ok(new ApiResponse<dtoHoardingtypeMaster>
                {
                    Status = libraries.StatusCode.Ok,
                    Data = await _hoardingtypeMasterService.GetById(id)
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPost]
        [Route("{id:int}/toggleStatus")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            try
            {
                await _hoardingtypeMasterService.ModifyStatusById(id);
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
