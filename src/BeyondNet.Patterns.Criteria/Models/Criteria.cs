namespace BeyondNet.Patterns.Criteria.Models
{
    public class Criteria
    {
        public Filters Filters { get; }
        public Order Order { get; }
        public int PageSize { get; }
        public int PageNumber { get; }

        public Criteria(Filters filters, Order order, int pageSize = 0, int pageNumber = 0)
        {
            Filters = filters;
            Order = order;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }

        public static Criteria FromPrimitives(string filters, string order, string orderType, int pageSize = 0, int pageNumber = 0, string cursor = "")
        {
            return new Criteria(
                new Filters(filters),
                Order.FromPrimitives(order, orderType),
                pageSize, pageNumber);
        }

        public bool HasOrder()
        {
            return !Order.IsEmpty();
        }

        public bool HasFilters()
        {
            return !Filters.IsEmpty();
        }
    }
}
