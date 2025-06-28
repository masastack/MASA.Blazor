using Masa.Blazor.Components.TemplateTable;
using Masa.Blazor.Components.TemplateTable.Contracts;
using Masa.Blazor.Components.TemplateTable.HotChocolate;

namespace Masa.Blazor.Test.TemplateTable;

[TestClass]
public class HotChocolateGetFilteringTests
{
    [TestMethod]
    public void GetFiltering_And_SingleOption()
    {
        var filterRequest = new Filter
        {
            Operator = FilterOperator.And,
            Options =
            [
                new FilterOption()
                {
                    ColumnId = "name",
                    Func = StandardFilter.Equals,
                    Expected = "John"
                }
            ]
        };

        var expected = """where: {name: {eq: "John"}}""";

        var actual = HotChocolateGraphQLClient.GetFiltering(filterRequest);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetFiltering_And_TwoOptions()
    {
        var filterRequest = new Filter
        {
            Operator = FilterOperator.And,
            Options =
            [
                new FilterOption()
                {
                    ColumnId = "name",
                    Func = StandardFilter.Equals,
                    Expected = "John"
                },
                new FilterOption()
                {
                    ColumnId = "age",
                    Func = StandardFilter.Gt,
                    Expected = "18"
                }
            ]
        };

        var expected = """where: {and: [{name: {eq: "John"}}, {age: {gt: "18"}}]}""";

        var actual = HotChocolateGraphQLClient.GetFiltering(filterRequest);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetFiltering_Or_TwoOptions()
    {
        var filterRequest = new Filter
        {
            Operator = FilterOperator.Or,
            Options =
            [
                new FilterOption()
                {
                    ColumnId = "name",
                    Func = StandardFilter.Equals,
                    Expected = "John"
                },
                new FilterOption()
                {
                    ColumnId = "age",
                    Func = StandardFilter.Gt,
                    Expected = "18"
                }
            ]
        };

        var expected = """where: {or: [{name: {eq: "John"}}, {age: {gt: "18"}}]}""";

        var actual = HotChocolateGraphQLClient.GetFiltering(filterRequest);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetFiltering_EmptyFilter()
    {
        var filterRequest = new Filter
        {
            Operator = FilterOperator.And,
            Options = []
        };

        var actual = HotChocolateGraphQLClient.GetFiltering(filterRequest);
        Assert.IsNull(actual);
    }

    [TestMethod]
    public void GetFiltering_NotSet()
    {
        var filterRequest = new Filter
        {
            Operator = FilterOperator.And,
            Options =
            [
                new FilterOption()
                {
                    ColumnId = "value",
                    Func = StandardFilter.NotSet
                }
            ]
        };

        var expected = """where: {value: {eq: null}}""";
        var actual = HotChocolateGraphQLClient.GetFiltering(filterRequest);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetFiltering_Set()
    {
        var filterRequest = new Filter
        {
            Operator = FilterOperator.And,
            Options =
            [
                new FilterOption()
                {
                    ColumnId = "value",
                    Func = StandardFilter.Set
                }
            ]
        };

        var expected = """where: {value: {neq: null}}""";
        var actual = HotChocolateGraphQLClient.GetFiltering(filterRequest);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetFiltering_True()
    {
        var filterRequest = new Filter
        {
            Operator = FilterOperator.And,
            Options =
            [
                new FilterOption()
                {
                    ColumnId = "value",
                    Func = StandardFilter.True
                }
            ]
        };

        var expected = """where: {value: {eq: true}}""";
        var actual = HotChocolateGraphQLClient.GetFiltering(filterRequest);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetFiltering_False()
    {
        var filterRequest = new Filter
        {
            Operator = FilterOperator.And,
            Options =
            [
                new FilterOption()
                {
                    ColumnId = "value",
                    Func = StandardFilter.False
                }
            ]
        };

        var expected = """where: {value: {eq: false}}""";
        var actual = HotChocolateGraphQLClient.GetFiltering(filterRequest);
        Assert.AreEqual(expected, actual);
    }
}