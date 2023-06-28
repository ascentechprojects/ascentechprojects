namespace app.advertise.libraries
{
    public sealed class UserRequestHeaders
    {
        public string IpAddress { get; internal set; }
        public string UserId { get; internal set; }="1";
        public string Source { get; internal set; } = UserSource.Web.ToString();
    }
}
