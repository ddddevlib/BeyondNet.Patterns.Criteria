using BeyondNet.Patterns.Criteria.Models;

public class FiltersPrimitives
{
    public string Field { get; set; }
    public string Operator { get; set; }
    public string Value { get; set; }

    public FiltersPrimitives(string field, string @operator, string value)
    {
        Field = field;
        Operator = @operator;
        Value = value;
    }
}

public class Filter
{
    public FilterField Field { get; }
    public FilterOperator Operator { get; }
    public FilterValue Value { get; }

    public Filter(FilterField field, FilterOperator @operator, FilterValue value)
    {
        Field = field;
        Operator = @operator;
        Value = value;
    }

    public static Filter FromPrimitives(string field, string @operator, string value)
    {
        return new Filter(
            new FilterField(field),
            new FilterOperator(Enum.Parse<Operator>(@operator, ignoreCase: true)),
            new FilterValue(value)
        );
    }

    public FiltersPrimitives ToPrimitives()
    {
        return new FiltersPrimitives(
            Field.Value,
            Operator.Value.ToString(),
            Value.Value);
    }
}


