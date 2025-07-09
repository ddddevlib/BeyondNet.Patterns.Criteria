namespace BeyondNet.Patterns.Criteria.Interfaces
{
    public interface ICriteriaConverter
    {
        string Convert(IEnumerable<string> fieldsToSelect, string tableName, Models.Criteria criteria, Dictionary<string, string> mappings);
    }
}