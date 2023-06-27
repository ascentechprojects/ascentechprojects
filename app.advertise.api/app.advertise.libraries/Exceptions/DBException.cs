using Microsoft.Extensions.Logging;

namespace app.advertise.libraries.Exceptions
{
    public class DBException: Exception
    {
        public DBException(string message, ILogger logger, Dictionary<string, string>? additionalErrorInfo = null) : base(message)
        {
            if (additionalErrorInfo == null)
                logger.LogError(this, "The database returned error: {errorMessage}.", message);
            else
                logger.LogError(this, "The database returned error: {errorMessage}. Additional Information: {@additionalErrorInfo}", message, additionalErrorInfo);
        }
    }
}
