namespace app.advertise.libraries
{
    public class UseroAuthClaims
    {
        public string Userid { get; set; }="1";
        public string IPAddress { get; set; }
        public string Source { get; set; }=UserSource.Web.ToString();
    }
}
