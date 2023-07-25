namespace app.advertise.dtos.Vendor
{
    public class dtoApplicationDetails : dtoBase
    {
        public int AppliId { get;set;}
        public string ApplicationNo { get; set; }
        public string AppliAppName { get; set; }
        public string AppliLicenseNo { get; set; }
        public string AppliLicenseOutNo { get; set; }
        public string AppliAddress { get; set; }
        public string AppliEmail { get; set; }
        public string AppliMobileNo { get; set; }
        public DateTime AppliFromDate { get; set; }
        public DateTime AppliUpToDate { get; set; }
        public int Quantity { get; set; }
        public int AppliPrabhagId { get;set; }
        public int AppliLocationId { get;set; }
        public int AppliHordingId { get;set; }

        public string HordingHoldAddress { get; set; }
        public string HordingOwnership { get; set; }
        public int HordingLength { get; set; }
        public int HordingWidth { get; set; }
        public int HordingTotalSqFt { get; set; }
        public string DisplayTypeName { get;set; }
    }
}
