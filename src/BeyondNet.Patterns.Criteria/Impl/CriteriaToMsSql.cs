using BeyondNet.Patterns.Criteria.Models;
using System.Text;

namespace BeyondNet.Patterns.Criteria.Impl
{
    public class CriteriaToMsSql
    {
        public string Convert(string[] fieldsToSelect, string tableName, Models.Criteria criteria)
        {
            var queryBuilder = new StringBuilder();
            queryBuilder.Append($"SELECT {string.Join(", ", fieldsToSelect)} FROM {tableName}");

            if (criteria.HasFilters())
            {
                queryBuilder.Append(" WHERE ");

                var whereQuery = criteria.Filters.Value
                    .Select(filter => GenerateWhereQuery(filter))
                    .ToList();

                queryBuilder.Append(string.Join(" AND ", whereQuery));
            }

            if (criteria.Cursor != null)
            {
                queryBuilder.Append(queryBuilder.ToString().Contains("WHERE") ? " AND " : " WHERE ");
                queryBuilder.Append($"{criteria.Order.OrderBy.Value} < '{criteria.Cursor}'");
            }

            if (criteria.HasOrder())
            {
                queryBuilder.Append($" ORDER BY {criteria.Order.OrderBy.Value} {criteria.Order.OrderType.Value}");
            }

            if (criteria.PageSize != 0)
            {

                queryBuilder.Append($" LIMIT {criteria.PageSize}");
            }

            return queryBuilder.ToString() + ";";
        }

        private string GenerateWhereQuery(Filter filter)
        {
            if (FilterOperator.IsContains(filter.Operator.Value))
            {
                return $"{filter.Field.Value} LIKE '%{filter.Value.Value}%'";
            }

            if (FilterOperator.IsNoContains(filter.Operator.Value))
            {
                return $"{filter.Field.Value} NOT LIKE '%{filter.Value.Value}%'";
            }

            return $"{filter.Field.Value} {filter.Operator.Value} '{filter.Value.Value}'";
        }
    }
}
