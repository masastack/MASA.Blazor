using Masa.Blazor.Components.TemplateTable.Contracts;
using Masa.Blazor.Components.TemplateTable.Cubejs.GraphQL;

namespace Masa.Blazor.Test.TemplateTable;

[TestClass]
public class CubejsGetFilteringTests
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

        var expected = """where: {name: {equals: "John"}}""";

        var actual = CubejsGraphQLClient.GetFiltering(filterRequest);
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

        var expected = """where: {AND: [{name: {equals: "John"}}, {age: {gt: "18"}}]}""";

        var actual = CubejsGraphQLClient.GetFiltering(filterRequest);
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

        var expected = """where: {OR: [{name: {equals: "John"}}, {age: {gt: "18"}}]}""";

        var actual = CubejsGraphQLClient.GetFiltering(filterRequest);
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

        var actual = CubejsGraphQLClient.GetFiltering(filterRequest);
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
        
        var expected = """where: {value: {set: false}}""";
        var actual = CubejsGraphQLClient.GetFiltering(filterRequest);
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
        
        var expected = """where: {value: {equals: "true"}}""";
        var actual = CubejsGraphQLClient.GetFiltering(filterRequest);
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
        
        var expected = """where: {value: {equals: "false"}}""";
        var actual = CubejsGraphQLClient.GetFiltering(filterRequest);
        Assert.AreEqual(expected, actual);
    }
}