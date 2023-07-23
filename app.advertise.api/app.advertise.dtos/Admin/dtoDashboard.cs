namespace app.advertise.dtos.Admin
{
    public class dtoDashboard:dtoBase
    {
        public dtoDashboardStatus ApplicationStatus {get;set;}
        public IEnumerable<dtoDashboardPrabhagOverview> PrabhagOverview { get;set;}
    }


    public class dtoDashboardStatus :dtoBase
    {
        public int StatusFlag_Pending { get; set; }
        public int StatusFlag_Rejected { get; set; }
        public int StatusFlag_Closed { get; set; }
        public int StatusFlag_Approved { get;set; }
    }
    public class dtoDashboardPrabhagOverview : dtoBase
    {
        public string AppliStatusFlag { get; set; }
        public int StatusFlagTotal { get; set; }
    }
}
