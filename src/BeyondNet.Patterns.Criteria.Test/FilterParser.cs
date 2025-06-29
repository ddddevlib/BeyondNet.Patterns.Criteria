using System.Text.RegularExpressions;
using System.Web;

namespace BeyondNet.Patterns.Criteria.Test
{
    public class QueryFilter
    {
        public string Field { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return $"{Field} {Operator} {Value}";
        }

        public QueryFilter(string field, string @operator, string value)
        {
            Field = field;
            Operator = @operator;
            Value = value;
        }
    }

    public class FilterParser
    {
        public static List<QueryFilter> ParseFilters(string queryString)
        {
            var searchParams = HttpUtility.ParseQueryString(queryString);
            var tempFilters = new Dictionary<string, Dictionary<string, string>>();

            foreach (string key in searchParams)
            {
                if (key == null) continue;

                var match = Regex.Match(key, @"filters\[(\d+)]\[(.+)]");

                if (match.Success)
                {
                    var index = match.Groups[1].Value;
                    var property = match.Groups[2].Value;
                    var value = searchParams[key];

                    if (!tempFilters.ContainsKey(index))
                    {
                        tempFilters[index] = new Dictionary<string, string>();
                    }

                    tempFilters[index][property] = value;
                }
            }

            var result = new List<QueryFilter>();

            foreach (var filterGroup in tempFilters.Values)
            {
                if (filterGroup.ContainsKey("field") && filterGroup.ContainsKey("operator") && filterGroup.ContainsKey("value"))
                {
                    result.Add(new QueryFilter(                    
                        filterGroup["field"],
                        filterGroup["operator"],
                        filterGroup["value"])
                    );
                }
            }

            return result;
        }
    }
}
