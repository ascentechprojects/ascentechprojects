using System.ComponentModel;

namespace app.advertise.libraries
{
    public enum RecordStatus
    {
        [Description("Active")]
        A = 1,
        [Description("InActive")]
        I = 2,
        [Description("Yes")]
        Y = 3,
        [Description("No")]
        N = 4,
        [Description("P")]
        Pending
    }

}
