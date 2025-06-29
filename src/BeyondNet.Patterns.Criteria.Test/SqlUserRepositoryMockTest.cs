using BeyondNet.Patterns.Criteria.Test.Domain;
using Microsoft.Data.SqlClient;

namespace BeyondNet.Patterns.Criteria.Test
{
    [TestClass]
    public class SqlUserRepositoryMockTest
    {
        [TestMethod]
        public async Task Search_ThrowsNotImplementedException()
        {
            // Arrange
            var connection = new SqlConnection(); // You may want to mock this if needed
            var repository = new SqlUserRepository(connection);
            var criteria = Models.Criteria.FromPrimitives(
                        [], "", "", 0, 0);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<NotImplementedException>(async () =>
            {
                await repository.Search(criteria);
            });
        }
    }
}
