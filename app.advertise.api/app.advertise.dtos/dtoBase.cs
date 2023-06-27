using System;

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
        public DateTime UpdBy { get; set; }
        public DateTime UpdDt { get; set; }
        public string StatusFlag { get; set; }
        public int UserId { get; set; }
        public int OrgId { get; set; }
        public string IPAddress { get; set; }
        public int PrabhagId { get; set; }

        public string DisplayStatus => !string.IsNullOrEmpty(StatusFlag) && StatusPrefix.Contains(StatusFlag, StringComparer.OrdinalIgnoreCase) ? "Active" : "InActive";

        private static readonly string[] StatusPrefix = new string[] { "Y", "A" };

        public string FormattedInsDt => InsDt.ToString("dd-MMM-yyyy");
        public string FormattedUpdDt => UpdDt.ToString("dd-MMM-yyyy");

        public readonly Dictionary<string,string> StatusFlags = new(){ { "A", "Active" }, { "I", "Inactive" }, };
    }
}
