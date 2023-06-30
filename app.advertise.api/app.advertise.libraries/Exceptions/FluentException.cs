using FluentValidation.Results;

namespace app.advertise.libraries.Exceptions
{
    public class FluentException : Exception
    {
        public FluentException(ValidationResult validationResult) : base(BuildErrorMessage(validationResult))
        {
        }

        private static string BuildErrorMessage(ValidationResult validationResult)
        {
            var errorMessages = validationResult.Errors
                .Select(error => error.ErrorMessage);

            return string.Join(",", errorMessages);
        }
    }
}
