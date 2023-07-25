namespace app.advertise.DataAccess.Entities.Vendor
{
    public record Application: baseEntity
    {
        public int NUM_APPLI_ULBID { get; set; }
        public int NUM_APPLI_ID { get; set; }
        public string VAR_APPLI_APPLINO { get; set; }
        public DateTime DAT_APPLI_APPLIDT { get; set; }
        public string VAR_APPLI_LICENO { get; set; }
        public string VAR_APPLI_LICEOUTNO { get; set; }
        public string VAR_APPLI_APPLINAME { get; set; }
        public string VAR_APPLI_ADDRESS { get; set; }
        public string VAR_APPLI_EMAIL { get; set; }
        public string NUM_APPLI_MOBILENO { get; set; }
        public int NUM_APPLI_HORDINGID { get; set; }
        public DateTime DAT_APPLI_FROMDT { get; set; }
        public DateTime DAT_APPLI_UPTODT { get; set; }
        public int NUM_APPLI_QTY { get; set; }
        public string VAR_APPLI_INSBY { get; set; }
        public DateTime DAT_APPLI_INSDT { get; set; }
        public string VAR_APPLI_UPDBY { get; set; }
        public DateTime DAT_APPLI_UPDT { get; set; }
        public string VAR_APPLI_APPROVFLAG { get; set; }
        public string VAR_APPLI_APPROVBY { get; set; }
        public DateTime DAT_APPLI_APPROVDT { get; set; }
        public string VAR_APPLI_APPROVREMARK { get; set; }
        public string VAR_APPLI_IPADDRESS { get; set; }
        public int NUM_APPLI_PRABHAGID { get; set; }
        public int NUM_APPLI_LOCATIONID { get; set; }

        public string VAR_PRABHAG_NAME { get; set; }
        public string VAR_LOCATION_NAME { get; set; }

        public int NUM_HORDING_LENGTH { get; set; }
        public int NUM_HORDING_WIDTH { get; set; }
        public int NUM_HORDING_TOTALSQFT { get; set; }
        public string VAR_HORDING_HOLDADDRESS { get; set; }
        public string VAR_HORDING_HOLDNAME { get; set; }
        public string VAR_HORDING_OWNERSHIP { get; set; }
        public string VAR_DISPLAYTYPE_NAME { get; set; }
    }
}
