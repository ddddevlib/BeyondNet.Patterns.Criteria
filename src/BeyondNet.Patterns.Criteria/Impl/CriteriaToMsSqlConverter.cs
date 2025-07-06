using BeyondNet.Patterns.Criteria.Interfaces;
using System.Text.RegularExpressions;

namespace BeyondNet.Patterns.Criteria.Impl
{
    public class CriteriaToMsSqlConverter : ICriteriaConverter
    {
        public string Convert(string[] fieldsToSelect, string tableName, Models.Criteria criteria)
        {
            var query = $"SELECT {string.Join(", ", fieldsToSelect)} FROM {tableName}";

            if (criteria.HasFilters())
            {
                var filters = Regex.Split(criteria.Filters.Value, @" AND | OR ");
                var connectors = Regex.Matches(criteria.Filters.Value, @" AND | OR /g")
                                      .Cast<Match>()
                                      .Select(m => m.Value.Trim())
                                      .ToList();

                var queryFilters = new List<string>();

                for (int i = 0; i < filters.Length; i++)
                {
                    var parts = filters[i].Split(' ');
                    var field = parts[0];
                    var operatorKey = parts[1];
                    var value = string.Join(" ", parts.Skip(2));

                    string operatorValue = OperatorEnum.GetValue(operatorKey);

                    if (operatorKey == "CONTAINS" || operatorKey == "NOT_CONTAINS")
                    {
                        value = $"%{value}%";
                    }

                    var finalValue = "";

                    if(int.TryParse(value, out int number))
                    {
                        finalValue = number.ToString();  // Output: 30
                    }
                    else
                    {
                        finalValue = $"'{value}'";  
                    }

                    queryFilters.Add($"{field} {operatorValue} {finalValue}");
    
                    if (i < connectors.Count)
                    {
                        queryFilters.Add(connectors[i]);
                    }
                }

                query += $" WHERE {string.Join(" ", queryFilters)}";
            }

            if (criteria.HasOrder())
            {
                query += $" ORDER BY {criteria.Order.OrderBy.Value} {criteria.Order.OrderType.Value}";
            }

            if (criteria.PageSize != 0)
            {
                query += $" LIMIT {criteria.PageSize}";
            }

            if (criteria.PageSize != 0 && criteria.PageNumber != 0)
            {
                query += $" OFFSET {criteria.PageSize * (criteria.PageNumber - 1)}";
            }

            return $"{query};";
        }

    }
}
