namespace app.advertise.dtos
{
    public class dtoApplicationAuthRequest : dtoBase
    {
        public int LocationId { get; set; }
        public int PrabhagId { get; set; }
        public string Remark { get; set; }
        public string StatusFlag { get; set; }

        public int AppliId { get; set; }
    }
}
