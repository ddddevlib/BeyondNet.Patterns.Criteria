using System.Text;

namespace BeyondNet.Patterns.Criteria.Impl
{
    public class CriteriaToMsSql
    {
        public Models.Criteria ConverTo(string[] fieldsToSelect, string tableName, Models.Criteria criteria)
        {
            if (criteria == null)
            {
                throw new ArgumentNullException(nameof(criteria), "Criteria cannot be null");
            }
            if (fieldsToSelect == null || fieldsToSelect.Length == 0)
            {
                throw new ArgumentException("At least one field must be specified for selection", nameof(fieldsToSelect));
            }
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentException("Table name cannot be null or empty", nameof(tableName));
            }

            var queryBuilder = new StringBuilder($"SELECT {string.Join(", ", fieldsToSelect)} FROM {tableName}");

            if (criteria.Filters != null && criteria.Filters.Value.Any())
            {
                queryBuilder.Append(" WHERE ");
                var filterConditions = criteria.Filters.Value.Select(f => $"{f.Field.Value} {f.Operator.Value} '{f.Value.Value}'");
                queryBuilder.Append(string.Join(" AND ", filterConditions));
            }

            if (criteria.Order != null && criteria.Order.OrderType != null)
            {
                queryBuilder.Append($" ORDER BY {criteria.Order.OrderBy.Value} {criteria.Order.OrderType.Value}");
            }
            
            return criteria;          

        }
    }
}
