namespace app.advertise.DataAccess.Entities.Admin
{
    public record AdminDashboard : baseEntity
    {
        public int NUM_APPLI_ULBID { get; set; }
        public string VAR_APPLI_APPROVFLAG { get; set; }
        public int APPROVFLAG_StatusCount { get; set; }
        public int NUM_APPLI_PRABHAGID { get; set; }
        public string VAR_PRABHAG_NAME { get; set; }
    }
}
