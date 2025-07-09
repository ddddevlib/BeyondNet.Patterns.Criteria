namespace BeyondNet.Patterns.Criteria.Models
{
    public enum Operator
    {
        [System.ComponentModel.Description("=")]
        EQUAL,

        [System.ComponentModel.Description("!=")]
        NOT_EQUAL,

        [System.ComponentModel.Description(">")]
        GT,

        [System.ComponentModel.Description("<")]
        LT,

        [System.ComponentModel.Description("CONTAINS")]
        CONTAINS,

        [System.ComponentModel.Description("NOT_CONTAINS")]
        NOT_CONTAINS
    }

}
