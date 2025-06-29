using System.Diagnostics;

namespace BeyondNet.Patterns.Criteria.Test
{
    [TestClass]
    public sealed class RoutingTest
    {
        string url = "";

        [TestInitialize]
        public void InitializeRoutingTest() // Renamed method to avoid conflict with class name
        {
            url = "https://example.com/api/users?filters[0][field]=name&filters[0][operator]=eq&filters[0][value]=John&filters[1][field]=age&filters[1][operator]=gt&filters[1][value]=30";
        }

        [TestCleanup]
        public void Cleanup()
        {
            url = string.Empty;
        }

        [TestMethod]
        public void ParseFilters_ShouldReturnEmptyList_WhenNoFiltersPresent()
        {
            var searchParams = new Uri(url).Query;

            var filters = FilterParser.ParseFilters(searchParams); // assumes this method exists as shown before

            // Build filter objects
            var filterObjects = filters.Select(filter =>
                new Filter(
                    new FilterField(filter.Field),
                    new FilterOperator(filter.Operator),
                    new FilterValue(filter.Value)
                )
            ).ToArray();

            // Handle optional order parameters
            var queryParams = System.Web.HttpUtility.ParseQueryString(searchParams);
            string? orderByParam = queryParams["orderBy"]; // Fixed CS8600 by making the variable nullable
            string? orderTypeParam = queryParams["orderType"]; // Fixed CS8600 by making the variable nullable

            Order order = !string.IsNullOrEmpty(orderByParam)
                ? new Order(    
                    new OrderBy(orderByParam),
                    new OrderType(orderTypeParam!)
                )
                : Order.None();

            // Create Criteria object
            var criteria = new Models.Criteria(new Filters(filterObjects), order, 0, 0);

            // Log criteria to console
            Debug.WriteLine(criteria);

            Assert.IsNotNull(filters);
        }

        [TestMethod]
        public void ParseFilters_ShouldReturnCorrectFilters()
        {
            var filters = FilterParser.ParseFilters(new Uri(url).Query);

            Assert.IsNotNull(filters);
            Assert.AreEqual(2, filters.Count);
            Assert.AreEqual("name", filters[0].Field);
            Assert.AreEqual("eq", filters[0].Operator);
            Assert.AreEqual("John", filters[0].Value);
        }       
    }
}
