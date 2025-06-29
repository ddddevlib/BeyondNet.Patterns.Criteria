namespace BeyondNet.Patterns.Criteria.Models
{
    public class FilterOperator
    {
        public string Value { get; set; }

        public FilterOperator(string value)
        {
            Value = value;
        }

        public static bool IsContains(string value)
        {
            return string.Equals(value, "contains", StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsNoContains(string value)
        {
            return string.Equals(value, "notcontains", StringComparison.OrdinalIgnoreCase);
        }
    }
}
