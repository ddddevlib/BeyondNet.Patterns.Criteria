namespace BeyondNet.Patterns.Criteria.Models
{
    public class OrderType
    {
        public OrderTypes Value { get; }

        public OrderType(OrderTypes value)
        {
            Value = value;
        }

        public bool IsNone()
        {
            return Value == OrderTypes.NONE;
        }
    }

}
