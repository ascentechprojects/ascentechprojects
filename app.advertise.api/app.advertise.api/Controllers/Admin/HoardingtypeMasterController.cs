using app.advertise.dtos.Admin;
using app.advertise.libraries;
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
                await _hoardingtypeMasterService.InsertUpdate(dtoHording, services.QueryExecutionMode.Insert);
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
        public async Task<IActionResult> Update(dtoHoardingtypeMaster dtoHording)
        {
            try
            {
                await _hoardingtypeMasterService.InsertUpdate(dtoHording, services.QueryExecutionMode.Update);
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
                var result = await _hoardingtypeMasterService.GetAll();
                return Ok(new ApiResponse<IEnumerable<dtoHoardingtypeMaster>>
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
                var result = await _hoardingtypeMasterService.GetById(id);
                return Ok(new ApiResponse<dtoHoardingtypeMaster>
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
