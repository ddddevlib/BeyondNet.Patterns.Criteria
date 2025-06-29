namespace BeyondNet.Patterns.Criteria.Models
{
    public class FilterOperator
    {
        public string Value { get; set; }

        public FilterOperator(string value)
        {
            Value = value;
        }
    }
}
