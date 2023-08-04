namespace app.advertise.libraries
{
  //to do claim,
    public sealed class VendorRequestHeaders
    {
        public string IpAddress { get; internal set; }
        public string UserId { get; internal set; }= "WT_BNCMC1_v";
        public string Source { get; internal set; } = UserSource.Web.ToString();
        public int UlbId { get; internal set; }
        public string HostAddress { get; internal set; }
        public string DeptId { get;internal set;}
    }
}
