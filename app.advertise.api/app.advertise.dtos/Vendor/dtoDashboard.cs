namespace app.advertise.dtos.Vendor
{
    public class dtoDashboard
    {
        public dtoStatusOverview ApplicationStatus { get; set; }
        public IEnumerable<dtoPrabhagOverview> PrabhagOverview { get; set; }
    }

    public class dtoStatusOverview
    {
        public int Today { get; set; }
        public int ThisMonth { get; set; }
        public int ThisYear { get; set; }
        public int Expired { get; set; }
    }

    public class dtoPrabhagOverview
    {
        public int Pending { get;set; }
        public string PrabhaName { get; set; }
        public int PrabhaId { get; set; }
        public int Total { get; set; }
        public int Sanction { get; set; }
        public int Expired { get; set; }
    }
}
