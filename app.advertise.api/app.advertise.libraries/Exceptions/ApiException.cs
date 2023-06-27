using Microsoft.Extensions.Logging;

namespace app.advertise.libraries.Exceptions
{
    public class ApiException:Exception
    {
        public ApiException(string message, ILogger logger, Dictionary<string, string>? additionalErrorInfo = null) : base(message)
        {
            if (additionalErrorInfo == null)
                logger.LogError(this, "The api returned error: {errorMessage}.", message);
            else
                logger.LogError(this, "The api returned error: {errorMessage}. Additional Information: {@additionalErrorInfo}", message, additionalErrorInfo);
        }
    }
}
