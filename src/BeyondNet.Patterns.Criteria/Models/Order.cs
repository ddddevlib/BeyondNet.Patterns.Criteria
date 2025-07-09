namespace BeyondNet.Patterns.Criteria.Models
{
    public class Order
    {
        public OrderBy OrderBy { get; }
        public OrderType OrderType { get; }

        public Order(OrderBy orderBy, OrderType orderType)
        {
            OrderBy = orderBy;
            OrderType = orderType;
        }

        public static Order None()
        {
            return new Order(new OrderBy(string.Empty), new OrderType(OrderTypes.NONE));
        }

        public static Order FromPrimitives(string orderBy, string orderType)
        {
            if (!string.IsNullOrEmpty(orderBy))
            {
                return new Order(
                    new OrderBy(orderBy),
                    new OrderType(Enum.Parse<OrderTypes>(orderType, ignoreCase: true))
                );
            }

            return None();
        }

        public bool IsNone()
        {
            return OrderType.IsNone();
        }
    }

}
