namespace BeyondNet.Patterns.Criteria.Models
{
    public static class GuardCriteria
    {
        public static void AgainstNull<T>(T value, string parameterName)
        {
            if (value is null)
                throw new ArgumentNullException(parameterName);
        }

        public static void AgainstInvalidPage(int pageSize, string cursor, string parameterName)
        {
            if (pageSize <= 0 && string.IsNullOrEmpty(cursor))
                throw new ArgumentException("Either pageSize must be greater than 0 or cursor must be provided.", parameterName);
        }
    }

    public class Criteria
    {
        public Filters Filters { get; }
        public Order Order { get; }
        public int PageSize { get; }
        public int PageNumber { get; }
        public string Cursor { get; }

        public Criteria(Filters filters, Order order, int pageSize = 0, int pageNumber = 0, string cursor = "")
        {
            GuardCriteria.AgainstNull(filters, nameof(filters));
            GuardCriteria.AgainstNull(order, nameof(order));
            GuardCriteria.AgainstInvalidPage(pageSize, cursor, nameof(pageSize));

            Filters = filters;
            Order = order;
            PageSize = pageSize;
            PageNumber = pageNumber;
            Cursor = cursor;
        }

        public static Criteria FromPrimitives(FilterPrimitives[] filters, string order, string orderType, int pageSize = 0, int pageNumber = 0, string cursor = "")
        {
            GuardCriteria.AgainstNull(filters, nameof(filters));
            GuardCriteria.AgainstNull(order, nameof(order));
            GuardCriteria.AgainstInvalidPage(pageSize, cursor, nameof(pageSize));

            return new Criteria(
                Filters.FromPrimitives(filters),
                new Order(new OrderBy(order), new OrderType(orderType)),
                pageSize, pageNumber, cursor);
        }

        public bool HasOrder()
        {
            return !Order.IsEmpty();
        }

        public bool HasFilters()
        {
            return !Filters.IsEmpty();
        }

        public override string ToString()
        {
            return $"Criteria: Filters={Filters}, Order={Order}, PageSize={PageSize}, PageNumber={PageNumber}, Cursor={Cursor}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Criteria other)
                return false;
            return Filters.Equals(other.Filters) &&
                   Order.Equals(other.Order) &&
                   PageSize == other.PageSize &&
                   PageNumber == other.PageNumber &&
                   Cursor == other.Cursor;
        }
    }
}
