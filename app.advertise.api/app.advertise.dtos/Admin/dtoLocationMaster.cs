namespace app.advertise.dtos.Admin
{
    public class dtoLocationMaster:dtoBase
    {
        
        public int LONGITUDE { get; set; }
        public int LATITUDE { get; set; }
        public string LocationActive { get; set; }
        public string PinCode { get; set; }
        public string Area { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
