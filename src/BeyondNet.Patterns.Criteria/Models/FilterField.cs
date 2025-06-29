namespace BeyondNet.Patterns.Criteria.Models
{
    public class FilterField
    {
        public string Value { get; set; }

        public FilterField(string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value), "Filter field value cannot be null.");
        }
    }
}
