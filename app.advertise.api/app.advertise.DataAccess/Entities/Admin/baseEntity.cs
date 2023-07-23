namespace app.advertise.DataAccess.Entities.Admin
{
    public record baseEntity
    {
        public int? ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
    }
}
