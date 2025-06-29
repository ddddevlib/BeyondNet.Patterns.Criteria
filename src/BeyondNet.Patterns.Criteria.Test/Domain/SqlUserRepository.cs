using Microsoft.Data.SqlClient;

namespace BeyondNet.Patterns.Criteria.Test.Domain
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly SqlConnection connection;

        public SqlUserRepository(SqlConnection connection)
        {
            this.connection = connection ?? throw new ArgumentNullException(nameof(connection), "Connection cannot be null.");
        }

        public async Task<IEnumerable<User>> Search(Models.Criteria criteria)
        {
            await Task.Yield();
            throw new NotImplementedException("This method is not implemented yet. Please implement the SQL query logic to search users based on the provided criteria.");
        }
    }
}
