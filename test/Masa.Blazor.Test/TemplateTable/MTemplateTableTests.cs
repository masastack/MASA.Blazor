using Masa.Blazor.Components.TemplateTable;
using Masa.Blazor.Components.TemplateTable.FilterDialogs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ItemsProviderRequest = Masa.Blazor.Components.TemplateTable.ItemsProviderRequest;

namespace Masa.Blazor.Test.TemplateTable;

[TestClass]
public class MTemplateTableTests
{
    [TestMethod]
    public void InsertFilterTest()
    {
        var filterRequest = new Filter
        {
            Operator = FilterOperator.And,
            Options =
            [
                new FilterOption()
                {
                    ColumnId = "name",
                    Func = "eq",
                    Expected = "John"
                }
            ]
        };

        var expected = """where: {name: {eq: "John"}}""";

        var actual = ItemsProviderRequest.GetFiltering(filterRequest);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void InsertFilterTest2()
    {
        var filterRequest = new Filter
        {
            Operator = FilterOperator.And,
            Options =
            [
                new FilterOption()
                {
                    ColumnId = "name",
                    Func = "eq",
                    Expected = "John"
                }
            ]
        };

        var expected = """where: {name: {eq: "John"}}""";

        var actual = ItemsProviderRequest.GetFiltering(filterRequest);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void InsertFilterTest4()
    {
        var filterRequest = new Filter
        {
            Operator = FilterOperator.And,
            Options =
            [
                new FilterOption()
                {
                    ColumnId = "name",
                    Func = "eq",
                    Expected = "John"
                },
                new FilterOption()
                {
                    ColumnId = "age",
                    Func = "gt",
                    Expected = "18"
                }
            ]
        };

        var expected = """where: {and: [{name: {eq: "John"}}, {age: {gt: "18"}}]}""";

        var actual = ItemsProviderRequest.GetFiltering(filterRequest);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void InsertFilterTest5()
    {
        var filterRequest = new Filter
        {
            Operator = FilterOperator.And,
            Options =
            [
                new FilterOption()
                {
                    ColumnId = "name",
                    Func = "eq",
                    Expected = "John"
                },
                new FilterOption()
                {
                    ColumnId = "age",
                    Func = "gt",
                    Expected = "18"
                }
            ]
        };

        var expected = """where: {and: [{name: {eq: "John"}}, {age: {gt: "18"}}]}""";

        var actual = ItemsProviderRequest.GetFiltering(filterRequest);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void InsertSortTest()
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

        var actual = ItemsProviderRequest.GetSorting(sortRequest);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void InsertSortTest2()
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

        var actual = ItemsProviderRequest.GetSorting(sortRequest);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void InsertSortTest3()
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

        var actual = ItemsProviderRequest.GetSorting(sortRequest);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void InsertSortTest4()
    {
        var sortRequest = new Sort
        {
            Options = []
        };

        var actual = ItemsProviderRequest.GetSorting(sortRequest);
        Assert.IsNull(actual);

        var actual2 = ItemsProviderRequest.GetSorting(null);
        Assert.IsNull(actual2);
    }
}