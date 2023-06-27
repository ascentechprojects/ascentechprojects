using app.advertise.libraries.Exceptions;
using app.advertise.libraries.Interfaces;
using System.Net;

namespace app.advertise.libraries
{
    public class InternalExceptionHandler : IInternalExceptionHandler
    {

        public ApiResponse HandleException(Exception ex)
        {
            return ex switch
            {
                ApiException => new ApiResponse()
                {
                    Status = StatusCode.InternalServer,
                    ErrorMessage = $"Api returned error: {ex.Message}",
                },
                DBException => new ApiResponse()
                {
                    Status = StatusCode.InternalServer,
                    ErrorMessage = ex.Message,
                },
                _ => new ApiResponse()
                {
                    Status = StatusCode.InternalServer,
                    ErrorMessage = $"Api returned error: {ex.Message}",
                },
            };
        }
    }
}
