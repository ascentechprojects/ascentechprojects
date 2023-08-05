namespace app.advertise.DataAccess.Entities.Vendor
{
    public record CitizenUser : baseEntity
    {
        public int NUM_CITIZENUSER_ULBID { get; set; }
        public int NUM_CITIZENUSER_USERID { get; set; }
        public string VAR_CITIZENUSER_NAME { get; set; }
        public string VAR_CITIZENUSER_ADDRESS { get; set; }
        public string VAR_CITIZENUSER_PASSWORD { get; set; }
        public string VAR_CITIZENUSER_EMAILID { get; set; }
        public int NUM_CITIZENUSER_MOBILENO { get; set; }
        public string VAR_CITIZENUSER_GENDER { get; set; }
        public int NUM_CITIZENUSER_AADHARNO { get; set; }
        public DateTime DATE_CITIZENUSER_DOB { get; set; }
        public string VAR_CITIZENUSER_INSBY { get; set; }
        public DateTime DAT_CITIZENUSER_INSDT { get; set; }
        public string VAR_CITIZENUSER_UPDBY { get; set; }
        public DateTime DAT_CITIZENUSER_UPDT { get; set; }
        public string VAR_CITIZENUSER_EMAILLINK { get; set; }
        public string VAR_CITIZENUSER_STATUS { get; set; }
        public string VAR_CITIZENUSER_EMAILLKVALUES { get; set; }
        public int NUM_CITIZENUSER_OTP { get; set; }

        public string VAR_CORPORATION_NAME { get; set; }
        public string VAR_CORPORATION_ADDRESS { get; set; }
    }
}
