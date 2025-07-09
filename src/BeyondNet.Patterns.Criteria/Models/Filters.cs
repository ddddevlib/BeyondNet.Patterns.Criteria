namespace BeyondNet.Patterns.Criteria.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class Filters
    {
        public IReadOnlyList<Filter> Value { get; }

        public Filters(IEnumerable<Filter> value)
        {
            Value = value.ToList().AsReadOnly();
        }

        public static Filters FromPrimitives(IEnumerable<FiltersPrimitives> filters)
        {
            var filterList = filters.Select(f => Filter.FromPrimitives(f.Field, f.Operator, f.Value));
            return new Filters(filterList);
        }

        public IEnumerable<FiltersPrimitives> ToPrimitives()
        {
            return Value.Select(filter => filter.ToPrimitives());
        }

        public bool IsEmpty()
        {
            return !Value.Any();
        }
    }


}
