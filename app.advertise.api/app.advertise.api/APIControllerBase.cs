using app.advertise.libraries.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace app.advertise.api
{
    public class APIControllerBase<T> : ControllerBase where T : class
    {
        private readonly ILogger<T> _logger;
        private readonly IInternalExceptionHandler _internalExceptionHandler;

        public APIControllerBase(ILogger<T> logger, IInternalExceptionHandler internalExceptionHandler)
        {
            _logger = logger;
            _internalExceptionHandler = internalExceptionHandler;
        }
        protected IActionResult HandleError(Exception ex)
        {
            var result = _internalExceptionHandler.HandleException(ex);

            switch (result.Status)
            {
                case libraries.StatusCode.InternalServer:
                    _logger.LogError(ex, "Exception occurred. Message: {message}", result.ErrorMessage);
                    break;
                default:
                    break;
            }

            return Ok(result);
        }
    }
}
