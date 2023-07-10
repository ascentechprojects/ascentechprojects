using app.advertise.libraries;
using app.advertise.libraries.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Newtonsoft.Json;

namespace app.advertise.api
{
    public class APIControllerBase<T> : ControllerBase where T : class
    {
        private readonly ILogger<T> _logger;
        private readonly IInternalExceptionHandler _internalExceptionHandler;
        private static readonly string ExceptionSeparator = $"******************************************************* {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} **********************************************************";
        public APIControllerBase(ILogger<T> logger, IInternalExceptionHandler internalExceptionHandler)
        {
            _logger = logger;
            _internalExceptionHandler = internalExceptionHandler;
        }

        protected IActionResult HandleError(Exception ex, object requestobj = null)
        {
            var result = _internalExceptionHandler.HandleException(ex);
            var requestBody = string.Empty;

            if (requestBody != null)
                requestBody = JsonConvert.SerializeObject(requestobj);

            Log.Error("\n{ExceptionSeparator}", ExceptionSeparator);

            switch (result.Status)
            {
                case libraries.StatusCode.InternalServer:
                    _logger.LogError(ex, "Exception occurred. Message: {message}, RequestBody:{requestBody}", result.ErrorMessage, requestBody);
                    break;
                case libraries.StatusCode.BadRequest:
                    _logger.LogError(ex, "Input validation failed. Message: {message},RequestBody:{requestBody}", result.ErrorMessage, requestBody);
                    break;
                default:
                    break;
            }

            return Ok(result);
        }


    }
}
