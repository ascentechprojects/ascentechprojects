namespace app.advertise.libraries
{
  //to do claim,
    public sealed class UserRequestHeaders
    {
        public string IpAddress { get; internal set; }
        public string UserId { get; internal set; }
        public string Source { get; internal set; } = UserSource.Web.ToString();
        public int UlbId { get; internal set; }
        public string HostAddress { get; internal set; }
        public string DeptId { get;internal set;}
    }
}
