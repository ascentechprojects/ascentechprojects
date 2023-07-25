namespace app.advertise.DataAccess.Entities.Admin
{
    public record ApplicationClose : baseEntity
    {
        public int NUM_APPLICLOSE_ID { get; set; }
        public int NUM_APPLICLOSE_HORDING_ID { get; set; }
        public string VAR_APPLICLOSE_REMARK { get; set; }
        //public string BLO_APPLICLOSE_IMAGE { get; set; }
        public string VAR_APPLICLOSE_INSBY { get; set; }
        public DateTime DAT_APPLICLOSE_INSDT { get; set; }
        public string VAR_APPLICLOSE_APPROVBY { get; set; }
        public DateTime DAT_APPLICLOSE_APPROVDT { get; set; }
        public string VAR_APPLICLOSE_APPROVREMARK { get; set; }
        public int NUM_APPLICLOSE_APPID { get; set; }
        public int NUM_APPLICLOSE_ULBID { get; set; }


        //out parameter
        public string VAR_APPLICLOSE_ID { get; set; }

    }
}
