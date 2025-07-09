namespace BeyondNet.Patterns.Criteria.Models
{
    using System;
    using System.Collections.Generic;

    public class Criteria
    {
        public Filters Filters { get; }
        public Order Order { get; }
        public int? PageSize { get; }
        public int? PageNumber { get; }

        public Criteria(Filters filters, Order order, int? pageSize, int? pageNumber)
        {
            if (pageNumber.HasValue && !pageSize.HasValue)
            {
                throw new ArgumentException("Page size is required when page number is defined");
            }

            Filters = filters;
            Order = order;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }

        public static Criteria FromPrimitives(
            IEnumerable<FiltersPrimitives> filters,
            string orderBy,
            string orderType,
            int? pageSize,
            int? pageNumber)
        {
            return new Criteria(
                Filters.FromPrimitives(filters),
                Order.FromPrimitives(orderBy, orderType),
                pageSize,
                pageNumber
            );
        }

        public bool HasOrder()
        {
            return !Order.IsNone();
        }

        public bool HasFilters()
        {
            return !Filters.IsEmpty();
        }
    }


}
