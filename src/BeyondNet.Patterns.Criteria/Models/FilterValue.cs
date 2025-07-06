namespace BeyondNet.Patterns.Criteria.Models
{
    public class FilterValue
    {
        public string Value { get; set; }

        public FilterValue(string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value), "Filter value cannot be null.");
        }
        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(Value);
        }

    }
}
