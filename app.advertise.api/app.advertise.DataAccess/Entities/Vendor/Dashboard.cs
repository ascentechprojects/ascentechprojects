namespace app.advertise.DataAccess.Entities.Vendor
{
    public record StatusOverview
    {
        public int Today { get; set; }
        public int ThisMonth { get; set; }
        public int ThisYear { get; set; }
        public int Expired { get; set; }
    }

    public record PrabhagOverview
    {
        public int Pending { get; set; }
        public string PrabhaName { get; set; }
        public int TOTALCOUNT { get; set; }
        public int SANCTION { get; set; }
        public int EXPIRED { get; set; }
    }
}
