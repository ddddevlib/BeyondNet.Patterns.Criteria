namespace BeyondNet.Patterns.Criteria.Models
{
    public class Filters
    {
        public Filter[] Value { get; set; }

        public Filters(Filter[] value)
        {
            Value = value ?? new Filter[0];
        }

        public static Filters FromPrimitives(FilterPrimitives[] filterPrimitives)
        {
            
            var filters = filterPrimitives.Select(fp => Filter.FromPrimitives(fp.Field, fp.Operator, fp.Value)).ToArray();
            return new Filters(filters);

        }
    }
}
