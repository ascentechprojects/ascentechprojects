using System.Text.RegularExpressions;

namespace app.advertise.libraries
{
    public static class StaticHelpers
    {
        public static string ToggleStatus(this string status)
        {
            if (string.Equals(RecordStatus.I.ToString(), status, StringComparison.OrdinalIgnoreCase))
                return RecordStatus.A.ToString();

            return RecordStatus.I.ToString();
        }

        public static Dictionary<string, string> HoardingOwnerships() => new() { { "P", "Private" }, { "C", "Corporation" } };
        public static Dictionary<string, string> RemarkStatus() => new() { { "A", "Approved" }, { "R", "Reject" }, { "P", "Pending" }, { "C", "Closed" } };
        public static string[] AllowedFileExtensions() => new string[] { "jpg", "jpeg", "png" };
        public static Dictionary<string, string> ValidGenders() => new() { { "M", "Male" }, { "F", "Female" }, { "O", "Other" } };

        public static bool ValidateDate(DateTime date)
        {
            if(date ==null) return false;

           return date==DateTime.MinValue;
        }
        public static bool InputValidator(this object input, ValidatorType validatorType)
        {
            if (input == null)
                return false;

            var inputstring = input.ToString();

            if (string.IsNullOrEmpty(inputstring))
                return false;

            return validatorType switch
            {
                ValidatorType.Pincode => Regex.IsMatch(inputstring, @"^\d{6}$"),
                ValidatorType.MobileNo => Regex.IsMatch(inputstring, @"^[0-9]{10}$"),
                ValidatorType.Email => Regex.IsMatch(inputstring, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"),
                ValidatorType.AAdhaar => Regex.IsMatch(inputstring, @"^[0-9]{12}$"),
                _ => false
            };
        }

    }
}
