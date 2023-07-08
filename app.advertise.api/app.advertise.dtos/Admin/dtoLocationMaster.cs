namespace app.advertise.dtos.Admin
{
    public class dtoLocationMaster:dtoBase
    {

        public decimal LONGITUDE { get; set; }
        public decimal LATITUDE { get; set; }
        public string PinCode { get; set; }
        public decimal Area { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
