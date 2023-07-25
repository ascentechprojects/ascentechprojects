namespace app.advertise.DataAccess.Entities.Admin
{
    public record HoardingtypeMaster
    {
        public int NUM_HOARDINGTYPE_ID { get; set; }
        public string VAR_HOARDINGTYPE_NAME { get; set; }
        public string VAR_HOARDINGTYPE_STATUS { get; set; }
        public string VAR_HOARDINGTYPE_INSBY { get; set; }
        public DateTime DAT_HOARDINGTYPE_INSDT { get; set; }
        public string VAR_HOARDINGTYPE_UPDBY { get; set; }
        public DateTime DAT_HOARDINGTYPE_UPDT { get; set; }
        public string VAR_HOARDINGTYPE_IPADDRESS { get; set; }
    }
}
