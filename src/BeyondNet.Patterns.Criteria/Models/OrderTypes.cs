using System.ComponentModel;

namespace BeyondNet.Patterns.Criteria.Models
{
    public enum OrderTypes
    {
        [Description("ASC")]
        ASC,

        [Description("DESC")]
        DESC,

        [Description("NONE")]
        NONE
    }
}
