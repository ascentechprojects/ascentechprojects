namespace app.advertise.DataAccess.Entities
{
    internal record PrabhagMaster:baseEntity
    {
        internal int NUM_PRABHAG_ID { get; set; }
        internal string VAR_PRABHAG_NAME { get;set; }
        internal string VAR_PRABHAG_STATUS { get;set; }
        internal int VAR_PRABHAG_ULBID { get;set; }
        internal string VAR_PRABHAG_INSBY { get;set; }  
        internal DateTime DAT_PRABHAG_INSDT { get;set; }
    }
}
