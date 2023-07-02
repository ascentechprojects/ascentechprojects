namespace app.advertise.DataAccess.Entities
{
    public record ApplicationAuthSearch:baseEntity
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
        public int NUM_APPLI_PRABHAGID { get; set; }
        public int NUM_APPLI_LOCATIONID { get; set; }
        public int NUM_HORDING_ULBID { get; set; }
        public int NUM_HORDING_ID { get; set; }
        public string VAR_HORDING_HOLDNAME { get; set; }
        public string VAR_HORDING_HOLDADDRESS { get; set; }
        public string VAR_HORDING_HOLDTYPE { get; set; }
        public int NUM_HORDING_DISPTYPEID { get; set; }
        public int NUM_HORDING_PRABHAGID { get; set; }
        public int NUM_HORDING_LOCATIONID { get; set; }
        public string VAR_HORDING_LANDMARK { get; set; }
        public int NUM_HORDING_LENGTH { get; set; }
        public int NUM_HORDING_WIDTH { get; set; }
        public int NUM_HORDING_TOTALSQFT { get; set; }
        public string VAR_HORDING_ACTIVE { get; set; }
        public string VAR_HORDING_INSBY { get; set; }
        public DateTime DAT_HORDING_INSDT { get; set; }
        public string VAR_HORDING_UPDBY { get; set; }
        public DateTime DAT_HORDING_UPDT { get; set; }
        public string VAR_HORDING_OWNERSHIP { get; set; }
        public int NUM_PRABHAG_ID { get; set; }
        public string VAR_PRABHAG_NAME { get; set; }
        public string VAR_PRABHAG_STATUS { get; set; }
        public string VAR_PRABHAG_ULBID { get; set; }
        public string VAR_PRABHAG_INSBY { get; set; }
        public DateTime DAT_PRABHAG_INSDT { get; set; }
        public int NUM_LOCATION_ULBID { get; set; }
        public int NUM_LOCATION_ID { get; set; }
        public string VAR_LOCATION_NAME { get; set; }
        public int NUM_LOCATION_AREA { get; set; }
        public int NUM_LOCATION_PINCODE { get; set; }
        public string VAR_LOCATION_ACTIVE { get; set; }
        public string VAR_LOCATION_INSBY { get; set; }
        public DateTime DAT_LOCATION_INSDT { get; set; }
        public string VAR_LOCATION_UPDBY { get; set; }
        public DateTime DAT_LOCATION_UPDT { get; set; }
        public int NUM_LOCATION_LATITUDE { get; set; }
        public int NUM_LOCATION_LONGITUDE { get; set; }

        public string VAR_DISPLAYTYPE_NAME { get;set;}

    }
}
