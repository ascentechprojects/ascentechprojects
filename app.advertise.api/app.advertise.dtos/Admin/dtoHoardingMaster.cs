namespace app.advertise.dtos.Admin
{
    public class dtoHoardingMaster:dtoBase
    {
        public string UlbName { get; set; } = null!;
        public int PrabhagId { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public string Address { get; set; }
        public string Landmark { get; set; }
        public string GeoLocation { get; set; }
        public string HoardingType { get; set; }
        public int DisplayTypeId { get; set; }
        public int LocationId { get; set; }
        public double TotalSQFT { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public string Ownership { get; set; }

        public string DisplayTypeName { get; set; }
        public string PrabhagName { get; set; }
        public string LocationName { get; set; }
        public string HordingTypeName { get; set; }

        public double Latitude { get;set; }
        public double Longitude { get;set; }
        public string Building { get;set;}
    }

}
