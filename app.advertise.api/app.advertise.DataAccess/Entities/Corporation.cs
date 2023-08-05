namespace app.advertise.DataAccess.Entities
{
    public record Corporation:baseEntity
    {
        public int NUM_CORPORATION_ID { get; set; }
        public string VAR_CORPORATION_MNAME { get; set; }
        public string VAR_CORPORATION_NAME { get; set; }
        public DateTime DAT_CORPORATION_INSDT { get; set; }
        public string VAR_CORPORATION_INSBY { get; set; }
        public DateTime DAT_LOACTION_UPDT { get; set; }
        public string VAR_CORPORATION_UPDBY { get; set; }
        public byte[] BLOB_CORPORATION_IMG { get; set; }
        public string VAR_CORPORATION_BOOKNO { get; set; }
        public DateTime DAT_CORPORATION_RECDT { get; set; }
        public string VAR_CORPORATION_CODE { get; set; }
        public string VAR_CORPORATION_ADDRESS { get; set; }
        public byte[] BLOB_CORPORATION_IMGREPORT { get; set; }
        public int NUM_CORPORATION_ABMULBID { get; set; }
        public int NUM_CORPORATION_DISTID { get; set; }
        public string VAR_CORPORATION_BILLPRINTRULE { get; set; }
        public string VAR_CORPORATION_FLAG { get; set; }
        public string VAR_CORPORATION_CLASS { get; set; }
        public string VAR_CORPORATION_SHASTIFLAG { get; set; }
        public string VAR_CORPORATION_MRRGCFCFLAG { get; set; }
        public int NUM_CORPORATION_TALID { get; set; }
        public string VAR_CORPORATION_CFCCHALLAN { get; set; }
    }
}
