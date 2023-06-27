namespace app.advertise.DataAccess.Entities
{
    public record class DisplayTypeMaster : baseEntity
    {
        public int NUM_DISPLAYTYPE_ID { get; set; }
        public string VAR_DISPLAYTYPE_NAME { get; set; }
        public string VAR_DISPLAYTYPE_STATUS { get; set; }
        public string VAR_DISPLAYTYPE_INSBY { get; set; }
        public DateTime DAT_DISPLAYTYPE_INSDT { get; set; }
        public string VAR_DISPLAYTYPE_UPDBY { get; set; }
        public DateTime DAT_DISPLAYTYPE_UPDT { get; set; }
        public string VAR_DISPLAYTYPE_IPADDRESS { get; set; }

    }
}
