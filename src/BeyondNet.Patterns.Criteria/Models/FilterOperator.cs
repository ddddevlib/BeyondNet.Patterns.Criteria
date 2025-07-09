namespace BeyondNet.Patterns.Criteria.Models
{
    public class FilterOperator
    {
        public Operator Value { get; }

        public FilterOperator(Operator value)
        {
            Value = value;
        }

        public bool IsContains()
        {
            return Value == Operator.CONTAINS;
        }

        public bool IsNotContains()
        {
            return Value == Operator.NOT_CONTAINS;
        }

        public bool IsNotEquals()
        {
            return Value == Operator.NOT_EQUAL;
        }
    }
}
