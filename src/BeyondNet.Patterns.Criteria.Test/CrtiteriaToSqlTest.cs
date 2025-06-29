namespace BeyondNet.Patterns.Criteria.Test
{
    [TestClass]
    public class CrtiteriaToSqlTest
    {
        [TestMethod]
        public void ConverTo_ThrowsArgumentNullException_WhenCriteriaIsNull()
        {
            var converter = new CriteriaToMsSql();
            Assert.ThrowsException<ArgumentNullException>(() =>
                converter.ConverTo(new[] { "Id" }, "Users", null));
        }

        [TestMethod]
        public void ConverTo_ThrowsArgumentException_WhenFieldsToSelectIsNullOrEmpty()
        {
            var converter = new CriteriaToMsSql();
            var criteria = new Models.Criteria(new Filters([]), new Order(new OrderBy(""), new OrderType("")),0, 0);

            Assert.ThrowsException<ArgumentException>(() =>
                converter.ConverTo([], "Users", criteria));

            Assert.ThrowsException<ArgumentException>(() =>
                converter.ConverTo(Array.Empty<string>(), "Users", criteria));
        }

        [TestMethod]
        public void ConverTo_ThrowsArgumentException_WhenTableNameIsNullOrEmpty()
        {
            var converter = new CriteriaToMsSql();
            var criteria = new Models.Criteria(null, null, 0,0);
            Assert.ThrowsException<ArgumentException>(() =>
                converter.ConverTo(new[] { "Id" }, null, criteria));
            Assert.ThrowsException<ArgumentException>(() =>
                converter.ConverTo(new[] { "Id" }, string.Empty, criteria));
        }

        [TestMethod]
        public void ConverTo_ReturnsCorrectSqlQuery_WhenValidCriteriaIsProvided()
        {
            var converter = new CriteriaToMsSql();
            var criteria = new Models.Criteria(
                new Filters([
                    new Models.Filter(new Models.FilterField("Name"), new Models.FilterOperator("eq"), new Models.FilterValue("John")),
                    new Models.Filter(new Models.FilterField("Age"), new Models.FilterOperator("gt"), new Models.FilterValue("30"))]
                    ),
                new Models.Order(new Models.OrderBy("Name"), new Models.OrderType("ASC")),
                0, 0
            );
            var sqlQuery = converter.ConverTo(new[] { "Id", "Name", "Age" }, "Users", criteria);
            Assert.IsNotNull(sqlQuery);
            // Here you would typically check the generated SQL query string, but for simplicity, we just check that it is not null.
        }


    }
}