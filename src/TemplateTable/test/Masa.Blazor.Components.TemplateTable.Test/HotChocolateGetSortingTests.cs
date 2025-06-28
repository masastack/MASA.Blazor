using Masa.Blazor.Components.TemplateTable.Contracts;
using Masa.Blazor.Components.TemplateTable.HotChocolate;

namespace Masa.Blazor.Test.TemplateTable;

[TestClass]
public class HotChocolateGetSortingTests
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

        var expected = """order: {name: ASC}""";

        var actual = HotChocolateGraphQLClient.GetSorting(sortRequest);
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

        var expected = """order: {name: DESC}""";

        var actual = HotChocolateGraphQLClient.GetSorting(sortRequest);
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

        var expected = """order: {name: ASC, age: DESC}""";

        var actual = HotChocolateGraphQLClient.GetSorting(sortRequest);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetSorting_EmptyOption()
    {
        var sortRequest = new Sort
        {
            Options = []
        };

        var actual = HotChocolateGraphQLClient.GetSorting(sortRequest);
        Assert.IsNull(actual);

        var actual2 = HotChocolateGraphQLClient.GetSorting(null);
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
                    ColumnId = "datekey",
                    OrderBy = SortOrder.Asc,
                    Type = ExpectedType.DateTime
                },
                new SortOption()
                {
                    ColumnId = "user.info.createdAt",
                    OrderBy = SortOrder.Desc,
                    Type = ExpectedType.DateTime
                }
            ]
        };

        var expected = "order: {datekey: ASC, user: {info: {createdAt: DESC}}}";

        var actual = HotChocolateGraphQLClient.GetSorting(sortRequest);
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

        var expected = "order: {user: {position: {name: ASC}}}";

        var actual = HotChocolateGraphQLClient.GetSorting(sortRequest);
        Assert.AreEqual(expected, actual);
    }
}