namespace app.advertise.dtos
{
    public class dtoBase
    {
        public int ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public bool IsSuccess { get { return string.IsNullOrEmpty(ErrorMsg); } }
        public int ULBId { get; set; }
        public string InsBy { get; set; }
        public DateTime InsDt { get; set; }
        public string UpdBy { get; set; }
        public DateTime UpdDt { get; set; }
        public string StatusFlag { get; set; }
        public string UserId { get; set; }
        public int OrgId { get; set; }
        public string IPAddress { get; set; }
        public string PrabhagName { get; set; }
        public int PrabhagId { get; set; }

        public string RecordId { get;set; }
        public string P_ULBId { get;set;} 

    }
}
