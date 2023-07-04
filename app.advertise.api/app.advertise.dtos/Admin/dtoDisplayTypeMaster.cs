namespace app.advertise.dtos.Admin
{
    public class dtoDisplayTypeMaster:dtoBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsExistsInConfig { get;set; }
        public int ConfigUlbId { get;set; }
        public int DisplayConfigId { get;set; }
    }
}
