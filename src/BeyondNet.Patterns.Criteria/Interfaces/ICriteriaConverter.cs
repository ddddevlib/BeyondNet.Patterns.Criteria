namespace BeyondNet.Patterns.Criteria.Interfaces
{
    public interface ICriteriaConverter
    {
        string Convert(string[] fieldsToSelect, string tableName, Models.Criteria criteria);
    }
}