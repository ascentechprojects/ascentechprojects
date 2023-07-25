namespace app.advertise.DataAccess.Entities
{
    public record baseEntity
    {
        public int? ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
    }
}
