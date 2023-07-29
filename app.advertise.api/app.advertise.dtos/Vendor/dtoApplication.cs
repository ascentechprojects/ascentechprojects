namespace app.advertise.dtos.Vendor
{
    public class dtoApplication:dtoBase
    {
        public string ApplicationNo { get; set; }
        public string AppliAppName { get; set; }
        public string HordingHoldName { get; set; }
        public string LocationName { get; set; }
        public string RemarkFlag { get; set; }
        public string AppliDate { get; set; }
        public string AppliLicenseNo { get; set; }

        public string AppliFrom { get; set;}
        public string AppliTo { get; set;}

        public int AppliCloseId { get;set; }
    }
}
