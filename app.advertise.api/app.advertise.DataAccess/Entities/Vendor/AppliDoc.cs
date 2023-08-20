namespace app.advertise.DataAccess.Entities.Vendor
{
    public record AppliDoc:baseEntity
    {
        public int NUM_APPLIDOC_ID {  get; set; }
        public int NUM_APPLIDOC_ULBID { get; set; }
        public int NUM_APPLIDOC_APPID { get; set; } 
        public string VAR_APPLIDOC_APPLINO { get; set; }
        public byte[] BLO_APPLIDOC_IMAGE { get; set; }
        public string VAR_APPLIDOC_INSBY { get;set; }
        public DateTime DAT_APPLIDOC_INSDT { get; set; }
        public string VAR_APPLIDOC_UPDBY { get; set; }
        public DateTime DAT_APPLIDOC_UPDDT { get; set; }
    }
}
