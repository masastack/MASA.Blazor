using Masa.Blazor.Components.TemplateTable;
using Masa.Blazor.Components.TemplateTable.Contracts;
using Masa.Blazor.Components.TemplateTable.Cubejs.GraphQL;

namespace Masa.Blazor.Test.TemplateTable;

[TestClass]
public class CubejsGetSortingTests
{
    [TestMethod]
    public void GetSorting_SingleAscOption()
    {
        var sortRequest = new Sort
        {
            Options =
            [
                new SortOption()
                {
                    ColumnId = "name",
                    OrderBy = SortOrder.Asc
                }
            ]
        };

        var expected = """orderBy: {name: asc}""";

        var actual = CubejsGraphQLClient.GetSorting(sortRequest);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetSorting_SingleDescOption()
    {
        var sortRequest = new Sort
        {
            Options =
            [
                new SortOption()
                {
                    ColumnId = "name",
                    OrderBy = SortOrder.Desc
                }
            ]
        };

        var expected = """orderBy: {name: desc}""";

        var actual = CubejsGraphQLClient.GetSorting(sortRequest);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetSorting_TwoOptions()
    {
        var sortRequest = new Sort
        {
            Options =
            [
                new SortOption()
                {
                    ColumnId = "name",
                    OrderBy = SortOrder.Asc
                },
                new SortOption()
                {
                    ColumnId = "age",
                    OrderBy = SortOrder.Desc
                }
            ]
        };

        var expected = """orderBy: {name: asc, age: desc}""";

        var actual = CubejsGraphQLClient.GetSorting(sortRequest);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetSorting_EmptyOption()
    {
        var sortRequest = new Sort
        {
            Options = []
        };

        var actual = CubejsGraphQLClient.GetSorting(sortRequest);
        Assert.IsNull(actual);

        var actual2 = CubejsGraphQLClient.GetSorting(null);
        Assert.IsNull(actual2);
    }

    [TestMethod]
    public void GetSorting_TwoOptions_DateKey_NestObject()
    {
        var sortRequest = new Sort
        {
            Options =
            [
                new SortOption()
                {
                    ColumnId = "datekey.value",
                    OrderBy = SortOrder.Asc,
                    Type = ExpectedType.DateTime
                },
                new SortOption()
                {
                    ColumnId = "user.info.createdAt.value",
                    OrderBy = SortOrder.Desc,
                    Type = ExpectedType.DateTime
                }
            ]
        };

        var expected = "orderBy: {datekey: asc, user: {info: {createdAt: desc}}}";

        var actual = CubejsGraphQLClient.GetSorting(sortRequest);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetSorting_SingleOption_NestObject()
    {
        var sortRequest = new Sort
        {
            Options =
            [
                new SortOption()
                {
                    ColumnId = "User.Position.Name",
                    OrderBy = SortOrder.Asc,
                    Type = ExpectedType.String
                }
            ]
        };

        var expected = "orderBy: {user: {position: {name: asc}}}";

        var actual = CubejsGraphQLClient.GetSorting(sortRequest);
        Assert.AreEqual(expected, actual);
    }
}