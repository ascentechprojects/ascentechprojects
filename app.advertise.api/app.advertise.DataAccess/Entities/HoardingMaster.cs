namespace app.advertise.DataAccess.Entities
{
    public record HoardingMaster:baseEntity
    {
        public int NUM_HORDING_ULBID { get;set;}
        public int NUM_HORDING_ID { get; set;}
        public string VAR_HORDING_HOLDNAME { get;set;}
        public string VAR_HORDING_HOLDADDRESS { get;set;}
        public string VAR_HORDING_HOLDTYPE { get;set;}  
        public int NUM_HORDING_DISPTYPEID { get;set;}   
        public int NUM_HORDING_PRABHAGID { get;set;}
        public int NUM_HORDING_LOCATIONID { get;set;}
        public string VAR_HORDING_LANDMARK { get;set;}
        public int NUM_HORDING_LENGTH { get;set;}
        public int NUM_HORDING_WIDTH { get;set;}
        public int NUM_HORDING_TOTALSQFT { get;set;}
        public string VAR_HORDING_ACTIVE { get;set;}
        public DateTime DAT_HORDING_INSDT { get;set;}
        public string VAR_HORDING_INSBY { get;set;} 
        public string VAR_HORDING_UPDBY { get;set;} 
        public DateTime DAT_HORDING_UPDT { get;set; }   
        public string VAR_HORDING_OWNERSHIP { get;set;}
    }
}

