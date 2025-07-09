using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BeyondNet.Patterns.Criteria.Test
{
    [TestClass]
    public class CriteriaToSqlConverterTest
    {
        private static Filters CreateFilters(params (string field, string op, string value)[] filters)
        {
            var filterList = new List<Filter>(filters.Length);
            foreach (var (field, op, value) in filters)
            {
                filterList.Add(new Filter(
                    new FilterField(field),
                    new FilterOperator(Enum.Parse<Operator>(op)),
                    new FilterValue(value)));
            }
            return new Filters(filterList);
        }

        private static Models.Criteria CreateCriteria(
            Filters filters,
            string orderBy = "Id",
            OrderTypes orderType = OrderTypes.NONE,
            int pageSize = 1,
            int pageNumber = 10)
        {
            return new Models.Criteria(
                filters,
                new Order(new OrderBy(orderBy), new OrderType(orderType)),
                pageSize,
                pageNumber);
        }

        [DataTestMethod]
        [DataRow(
            "SELECT Id, Name FROM Users WHERE Id EQUAL '1' AND Name EQUAL 'John' LIMIT 1 OFFSET 9;",
            new[] { "Id", "Name" },
            "Users",
            "Id,EQUAL,1;Name,EQUAL,John",
            OrderTypes.NONE,
            1,
            10)]
        [DataRow(
            "SELECT Id, Name FROM Users WHERE Id EQUAL '1' AND Name EQUAL 'John' ORDER BY Id ASC LIMIT 1 OFFSET 9;",
            new[] { "Id", "Name" },
            "Users",
            "Id,EQUAL,1;Name,EQUAL,John",
            OrderTypes.ASC,
            1,
            10)]
        [DataRow(
            "SELECT Id, Name FROM Users WHERE Id EQUAL '1' AND Name EQUAL 'John' ORDER BY Id ASC LIMIT 1 OFFSET 9;",
            new[] { "Id", "Name" },
            "Users",
            "Id,EQUAL,1;Name,EQUAL,John",
            OrderTypes.ASC,
            1,
            10)]
        [DataRow(
            "SELECT Id, Name, Age FROM Users WHERE Id EQUAL '1' AND Name EQUAL 'John' AND Age GT '30' ORDER BY Id ASC LIMIT 1 OFFSET 9;",
            new[] { "Id", "Name", "Age" },
            "Users",
            "Id,EQUAL,1;Name,EQUAL,John;Age,GT,30",
            OrderTypes.ASC,
            1,
            10)]
        public void Convert_GeneratesExpectedSql(
            string expectedSql,
            string[] fields,
            string table,
            string filtersRaw,
            OrderTypes orderType,
            int pageSize,
            int pageNumber)
        {
            var converter = new CriteriaToSqlConverter();
            var filterTuples = ParseFilters(filtersRaw);
            var filters = CreateFilters(filterTuples);
            var criteria = CreateCriteria(filters, "Id", orderType, pageSize, pageNumber);

            var sql = converter.Convert(fields, table, criteria);

            Assert.AreEqual(expectedSql, sql);
        }

        private static (string, string, string)[] ParseFilters(string filtersRaw)
        {
            if (string.IsNullOrWhiteSpace(filtersRaw))
                return [];
            var filters = filtersRaw.Split(';');
            var result = new (string, string, string)[filters.Length];
            for (int i = 0; i < filters.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(filters[i])) continue;
                var parts = filters[i].Split(',');
                result[i] = (parts[0], parts[1], parts[2]);
            }
            return result;
        }
    }
}