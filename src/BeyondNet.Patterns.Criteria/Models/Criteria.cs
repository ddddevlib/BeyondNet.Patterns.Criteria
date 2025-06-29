namespace BeyondNet.Patterns.Criteria.Models
{
    public class Criteria
    {
        public Filters Filters { get; set; }
        public Order Order { get; set; }

        public Criteria(Filters filters, Order order)
        {
            Filters = filters;
            Order = order;
        }

        public static Criteria FromPrimitives(Filters filters, Order order)
        {
            return new Criteria(filters, order);
        }
    }
}
