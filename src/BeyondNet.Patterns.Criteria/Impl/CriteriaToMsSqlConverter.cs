namespace BeyondNet.Patterns.Criteria.Impl
{
    using BeyondNet.Patterns.Criteria.Interfaces;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class CriteriaToSqlConverter : ICriteriaConverter
    {
        public string Convert(
            IEnumerable<string> fieldsToSelect,
            string tableName,
            Models.Criteria criteria,
            Dictionary<string, string>? mappings = null)
        {
            mappings ??= new Dictionary<string, string>();
            var queryBuilder = new StringBuilder();

            queryBuilder.Append($"SELECT {string.Join(", ", fieldsToSelect)} FROM {tableName}");

            var whereClause = BuildWhereClause(criteria, mappings);
            if (!string.IsNullOrEmpty(whereClause))
            {
                queryBuilder.Append(" WHERE ").Append(whereClause);
            }

            var orderClause = BuildOrderClause(criteria);
            if (!string.IsNullOrEmpty(orderClause))
            {
                queryBuilder.Append(" ORDER BY ").Append(orderClause);
            }

            AppendLimitOffset(criteria, queryBuilder);

            queryBuilder.Append(";");

            return queryBuilder.ToString();
        }

        private string BuildWhereClause(Models.Criteria criteria, Dictionary<string, string> mappings)
        {
            if (!criteria.HasFilters())
                return string.Empty;

            var conditions = criteria.Filters.Value
                .Select(filter => GenerateWhereQuery(filter, mappings));

            return string.Join(" AND ", conditions);
        }

        private string BuildOrderClause(Models.Criteria criteria)
        {
            if (!criteria.HasOrder() || criteria.Order.OrderType.IsNone())
                return string.Empty;

            return $"{criteria.Order.OrderBy.Value} {criteria.Order.OrderType.Value}";
        }

        private void AppendLimitOffset(Models.Criteria criteria, StringBuilder queryBuilder)
        {
            if (criteria.PageSize >= 0)
            {
                queryBuilder.Append($" LIMIT {criteria.PageSize}");

                if (criteria.PageNumber >= 0)
                {
                    var offset = criteria.PageSize * (criteria.PageNumber - 1);
                    queryBuilder.Append($" OFFSET {offset}");
                }
            }
        }

        private string GenerateWhereQuery(Filter filter, Dictionary<string, string> mappings)
        {
            var field = mappings.TryGetValue(filter.Field.Value, out var mappedField)
                ? mappedField
                : filter.Field.Value;

            var value = filter.Value.Value;

            return filter.Operator switch
            {
                var op when op.IsContains() => $"{field} LIKE '%{value}%'",
                var op when op.IsNotContains() => $"{field} NOT LIKE '%{value}%'",
                var op when op.IsNotEquals() => $"{field} != '{value}'",
                _ => $"{field} {filter.Operator.Value.ToString()} '{value}'"
            };
        }
    }

}

