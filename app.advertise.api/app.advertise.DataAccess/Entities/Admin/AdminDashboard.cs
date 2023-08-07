namespace app.advertise.DataAccess.Entities.Admin
{
    public record AdminDashboardStatusOverview
    {
        public int Pending { get; set; }
        public int Approved { get; set; }
        public int Rejected { get; set; }
        public int Cancelled { get; set; }
    }

    public record AdminDashboardPrabhagOverview {
        public int Pending { get; set; }
        public string VAR_PRABHAG_NAME { get; set; }
        public int NUM_APPLI_PRABHAGID { get; set; }
        public int TOTALCOUNT { get; set; }
        public int SANCTION { get; set; }
        public int EXPIRED { get; set; }
    }
}
