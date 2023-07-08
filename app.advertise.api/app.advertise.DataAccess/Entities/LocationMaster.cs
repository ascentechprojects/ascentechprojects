namespace app.advertise.DataAccess.Entities
{
    public record LocationMaster:baseEntity
    {
        public int NUM_LOCATION_PRABHAGID { get;set; }
        public decimal NUM_LOCATION_LONGITUDE { get;set; }
        public decimal NUM_LOCATION_LATITUDE { get;set; }
        public string VAR_LOCATION_IPADDRESS { get;set; }
        public DateTime DAT_LOCATION_UPDT { get; set; }   
        public string VAR_LOCATION_UPDBY { get;set; }
        public DateTime DAT_LOCATION_INSDT { get;set; }
        public string VAR_LOCATION_INSBY { get;set; }
        public string VAR_LOCATION_ACTIVE { get;set; }
        public string NUM_LOCATION_PINCODE { get;set;}
        public decimal NUM_LOCATION_AREA { get;set; }
        public string VAR_LOCATION_NAME { get;set;}
        public int NUM_LOCATION_ID { get; set; }
        public int NUM_LOCATION_ULBID { get;set;}
        public string VAR_PRABHAG_NAME { get;set; } 
    }
}
