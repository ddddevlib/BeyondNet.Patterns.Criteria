namespace BeyondNet.Patterns.Criteria.Models
{
    public class OrderBy
    {
        public string Value { get; set; }

        public OrderBy(string value)
        {
            Value = value;
        }
    }
}
