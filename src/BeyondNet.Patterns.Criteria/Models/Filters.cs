namespace BeyondNet.Patterns.Criteria.Models
{
    public class Filters
    {
        public string Value { get; set; }

        public Filters(string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value), "Filters value cannot be null.");
        }
        
        public bool IsEmpty()
        {
            return Value.Length == 0;
        }
    }
}
