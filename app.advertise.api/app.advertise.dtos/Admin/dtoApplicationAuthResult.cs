namespace app.advertise.dtos.Admin
{
    public class dtoApplicationAuthResult : dtoBase
    {
        public string ApplicationNo { get; set; }
        public string AppliAppName { get; set; }
        public int AppliId { get; set; }
        public string AppliLicenseNo { get; set; }
        public string AppliLicenseOutNo { get; set; }
        public string AppliAddress { get; set; }
        public string AppliEmail { get; set; }
        public string AppliMobileNo { get; set; }
        public string AppliFromDate { get; set; }
        public string AppliUpToDate { get; set; }
        public string AppliInsBy { get; set; }
        public string AppliInsDt { get; set; }
        public string HordingHoldName { get; set; }
        public string HordingHoldAddress { get; set; }
        public string HordingOwnership { get; set; }
        public int HordingLength { get; set; }
        public int HordingWidth { get; set; }
        public int HordingTotalSqFt { get; set; }
        public string DisplayTypeName { get; set; }
        public string PrabhagName { get; set; }
        public double Quantity { get; set; }
        public string LocationName { get; set; }
        public string Remark { get; set; }
        public string RemarkFlag { get; set; }
    }
}
