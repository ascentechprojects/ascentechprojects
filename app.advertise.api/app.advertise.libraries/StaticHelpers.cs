namespace app.advertise.libraries
{
    public static class StaticHelpers
    {
        public static string ToggleStatus(this string status)
        {
            if(string.Equals(RecordStatus.I.ToString(),status,StringComparison.OrdinalIgnoreCase))
                return RecordStatus.A.ToString();

            return RecordStatus.I.ToString();
        }

        public static Dictionary<string,string> HoardingOwnerships()=> new() { { "P", "Private" }, { "C", "Corporation" } };
      
        
    }
}
