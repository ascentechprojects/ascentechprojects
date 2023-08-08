namespace app.advertise.dtos.Admin
{
    public class dtoDashboard : dtoBase
    {
        public dtoDashboardStatus ApplicationStatus { get; set; }
        public IEnumerable<dtoDashboardPrabhagOverview> PrabhagOverview { get; set; }
    }


    public class dtoDashboardStatus
    {
        public int StatusFlag_Pending { get; set; }
        public int StatusFlag_Rejected { get; set; }
        public int StatusFlag_Cancelled { get; set; }
        public int StatusFlag_Approved { get; set; }
    }
    public class dtoDashboardPrabhagOverview
    {
        public int Pending { get; set; }
        public string PrabhaName { get; set; }
        public int PrabhaId { get; set; }
        public int Total { get; set; }
        public int Sanction { get; set; }
        public int Expired { get; set; }
    }
}
