namespace BeyondNet.Patterns.Criteria.Models
{
    public class FilterValue
    {
        public string Value { get; set; }

        public FilterValue(string value)
        {
            Value = value.Trim();
            Value = value;
        }
    }
}
