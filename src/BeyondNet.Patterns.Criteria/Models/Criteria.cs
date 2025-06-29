namespace BeyondNet.Patterns.Criteria.Models
{
    public class Criteria
    {
        public Filters Filters { get; set; }
        public Order Order { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public Criteria(Filters filters, Order order, int pageSize, int pageNumber)
        {
            Filters = filters;
            Order = order;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }

        public static Criteria FromPrimitives(FilterPrimitives[] filters, string orderBy, string orderType, int pageSize, int pageNumber)
        {
            return new Criteria(Filters.FromPrimitives(filters), 
                new Order(new OrderBy(orderBy), new OrderType(orderType)),
                pageSize, pageNumber);
        }
    }
}
