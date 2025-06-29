namespace BeyondNet.Patterns.Criteria.Models
{
    public class FilterPrimitives
    {
        public string Field { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
        public FilterPrimitives(string field, string operatorEnum, string value)
        {
            Field = field ?? throw new ArgumentNullException(nameof(field), "Field cannot be null.");
            Operator = operatorEnum ?? throw new ArgumentNullException(nameof(operatorEnum), "Operator cannot be null.");
            Value = value ?? throw new ArgumentNullException(nameof(value), "Value cannot be null.");
        }
    }

    public class Filter
    {
        public FilterField Field { get; set; }
        public FilterOperator Operator { get; set; }
        public FilterValue Value { get; set; }

        public Filter(FilterField field, FilterOperator filterOperator, FilterValue value)
        {
            Field = field;
            Operator = filterOperator;
            Value = value;
        }

        public static Filter FromPrimitives(string field, string operatorEnum, string value)
        {
            if (string.IsNullOrEmpty(field) || string.IsNullOrEmpty(operatorEnum) || string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Field, operator, and value must be provided.");
            }
            
            
            return new Filter(
                new FilterField(field),
                new FilterOperator(operatorEnum),
                new FilterValue(value)
            );
        }

        public FilterPrimitives ToPrimitives()
        {
            return new FilterPrimitives(Field.Value, Operator.Value, Value.Value);
        }
    }
}
