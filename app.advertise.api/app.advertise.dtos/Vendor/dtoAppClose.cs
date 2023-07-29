namespace app.advertise.dtos.Vendor
{
    public class dtoAppClose:dtoBase
    {
        public string[] AppliIds { get; set; }=Array.Empty<string>();
        public string Remark { get;set; }

        public int AppliLocationId { get; set; }
        public int AppliPrabhagId { get; set; }

        public int AppliHordingId { get; set; }

    }
}
