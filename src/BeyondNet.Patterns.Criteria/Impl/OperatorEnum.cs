using System.Globalization;

namespace BeyondNet.Patterns.Criteria.Impl
{
    public static class OperatorEnum
    {
        public const string Equal = "=";
        public const string NotEqual = "!=";
        public const string GreaterThan = ">";
        public const string GreaterThanOrEqual = ">=";
        public const string LessThan = "<";
        public const string LessThanOrEqual = "<=";
        public const string Like = "LIKE";
        public const string In = "IN";
        public const string NotIn = "NOT IN";
        public const string IsNull = "IS NULL";
        public const string IsNotNull = "IS NOT NULL";
        public const string Between = "BETWEEN";
        public const string NotBetween = "NOT BETWEEN";
        public const string StartsWith = "STARTS WITH";
        public const string EndsWith = "ENDS WITH";
        public const string Contains = "CONTAINS";
        public const string NotContains = "NOT CONTAINS";
        public const string Exists = "EXISTS";
        public const string NotExists = "NOT EXISTS";
        public const string Any = "ANY";
        public const string All = "ALL";
        public const string IsTrue = "IS TRUE";
        public const string IsFalse = "IS FALSE";
        public const string IsEmpty = "IS EMPTY";

        public static string GetValue(string key)
        {
            key = CultureInfo.CurrentCulture.TextInfo.ToLower(key.ToLower());

            return key switch
            {
                "equal" => Equal,
                "notequal" => NotEqual,
                "greaterthan" => GreaterThan,
                "greaterthanorequal" => GreaterThanOrEqual,
                "lessthan" => LessThan,
                "LessThanOrEqual" => LessThanOrEqual,
                "like" => Like,
                "in" => In,
                "notin" => NotIn,
                "isnull" => IsNull,
                "isnotnull" => IsNotNull,
                "between" => Between,
                "notbetween" => NotBetween,
                "startswith" => StartsWith,
                "endswith" => EndsWith,
                "contains" => Contains,
                "notcontains" => NotContains,
                "exists" => Exists,
                "notexists" => NotExists,
                "any" => Any,
                "all" => All,
                "istrue" => IsTrue,
                "isfalse" => IsFalse,
                "isempty" => IsEmpty,
                _ => throw new ArgumentException($"Unknown operator: {key}")
            };
        }
    }    
}
