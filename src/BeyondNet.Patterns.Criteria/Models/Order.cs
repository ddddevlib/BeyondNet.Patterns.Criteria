namespace BeyondNet.Patterns.Criteria.Models
{
    public  class Order
    {
        public OrderBy OrderBy { get; set; }
        public OrderType OrderType { get; set; }

        public Order(OrderBy orderBy, OrderType orderType)
        {
            OrderBy = orderBy;
            OrderType = orderType;
        }

        public static Order None() 
        {
            return new Order(new OrderBy(""), new OrderType(OrderTypesEnum.None));
        }

        public static Order FromPrimitives(string orderBy, string orderType)
        {
            if (string.IsNullOrEmpty(orderBy))
            {
                throw new ArgumentException("OrderBy cannot be null or empty.", nameof(orderBy));
            }
            return new Order(new OrderBy(orderBy), new OrderType(orderType));
        }
    }
}
