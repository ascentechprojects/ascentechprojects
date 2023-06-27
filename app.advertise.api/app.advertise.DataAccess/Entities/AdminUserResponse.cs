namespace app.advertise.DataAccess.Entities
{
    public record AdminUserResponse:baseEntity
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string LastLogin { get; set; }
        public string LastLogOut { get; set; }
        public string Corporation { get; set; }
        public string CorporationAddress { get; set; }
        public string ReceiptOfficeName { get; set; }
        public string ChalanOfficeName { get; set; }
        public string PrabhagName { get; set; }
        public string PrabhagId { get; set; }
        public string DesigId { get; set; }
        public string UserType { get; set; }
        public int? CollectionCenter { get; set; }
        public string MobileNo { get; set; }
        public string OtpValidate { get; set; }
        
        public int? OrgId { get; set; }
        public string ForceFullPassChag { get; set; }
    }
}
