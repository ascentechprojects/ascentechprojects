namespace app.advertise.libraries
{
  //to do claim,
    public sealed class UserRequestHeaders
    {
        public string IpAddress { get; internal set; }
        public string UserId { get; internal set; }= "WT_BNCMC1";
        public string Source { get; internal set; } = UserSource.Web.ToString();
        public int PrabhagId { get; internal set; }=1;

        public int UlbId { get; internal set; }=0;
    }
}
