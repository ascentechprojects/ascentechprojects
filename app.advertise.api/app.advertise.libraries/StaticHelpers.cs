using Newtonsoft.Json.Linq;
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

        public static bool InputValidator(this object input, ValidatorType validatorType)
        {
            if(input == null)
                return false;

            var inputstring= input.ToString();

            if (string.IsNullOrEmpty(inputstring))
                return false;

            return validatorType switch
            {
                ValidatorType.Pincode => Regex.IsMatch(inputstring, @"^\d{6}$"),
                _ => false
            };
        }

    }
}
