namespace app.advertise.DataAccess.Entities
{
    public record HordingTypeConfig:baseEntity
    {
        public int NUM_HOARDINGCONFIG_ID { get;set;}
        public int NUM_HOARDINGCONFIG_HOARDID { get;set;}
        public int NUM_HOARDINGCONFIG_ULBID { get;set;}
        public string VAR_HOARDINGCONFIG_INSBY { get;set;}
        public DateTime DAT_HOARDINGCONFIG_INSDT { get;set;}
        public DateTime VAR_HOARDINGCONFIG_UPDBY { get;set;}
        public DateTime DAT_HOARDINGCONFIG_UPDT { get;set;}
        public string VAR_HOARDINGCONFIG_ACTIVEFLAG { get;set;}

        public string HoardingType { get;set;}
    }
}
