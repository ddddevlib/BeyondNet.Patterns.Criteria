namespace BeyondNet.Patterns.Criteria.Test
{
    [TestClass]
    public class CrtiteriaToSqlConverterTest
    {

        [TestMethod]
        public void GenerateSimpleSelectWithAnEmptyCriteria()
        {
            var converter = new CriteriaToMsSqlConverter();
            var criteria = new Models.Criteria(new Filters(""), new Order(new OrderBy(""), new OrderType("")), 1, 10);

            var sql = converter.Convert(new[] { "Id", "Name" }, "Users", criteria);

            Assert.AreEqual("SELECT Id, Name FROM Users LIMIT 1 OFFSET 9;", sql);
        }

        [TestMethod]
        public void GenerateSelectWithOrder()
        {
            var converter = new CriteriaToMsSqlConverter();
            var criteria = new Models.Criteria(new Filters(""), new Order(new OrderBy("Name"), new OrderType("ASC")), 1, 10);

            var sql = converter.Convert(new[] { "Id", "Name" }, "Users", criteria);

            Assert.AreEqual("SELECT Id, Name FROM Users ORDER BY Name ASC LIMIT 1 OFFSET 9;", sql);
        }

        [TestMethod]
        public void GenerateSelectWithOneFilter()
        {
            var converter = new CriteriaToMsSqlConverter();
            var criteria = new Models.Criteria(new Filters("Name EQUAL John"), new Order(new OrderBy("Id"), new OrderType("ASC")), 1, 10);

            var sql = converter.Convert(new[] { "Id", "Name" }, "Users", criteria);

            Assert.AreEqual("SELECT Id, Name FROM Users WHERE Name = 'John' ORDER BY Id ASC LIMIT 1 OFFSET 9;", sql);
        }

        [TestMethod]
        public void GenerateSelectWithTwoFilter()
        {
            var converter = new CriteriaToMsSqlConverter();
            var criteria = new Models.Criteria(new Filters("Name EQUAL John AND Age GreaterThan 30"),
                                               new Order(new OrderBy("Id"), new OrderType("ASC")), 1, 10);

            var sql = converter.Convert(new[] { "Id", "Name", "Age" }, "Users", criteria);
            Assert.AreEqual("SELECT Id, Name, Age FROM Users WHERE Name = 'John' AND Age > 30 ORDER BY Id ASC LIMIT 1 OFFSET 9;", sql);

        }
    }
}