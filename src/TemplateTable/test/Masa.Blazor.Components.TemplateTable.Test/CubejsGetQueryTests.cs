using Masa.Blazor.Components.TemplateTable;
using Masa.Blazor.Components.TemplateTable.Abstractions;
using Masa.Blazor.Components.TemplateTable.Contracts;
using Masa.Blazor.Components.TemplateTable.Cubejs.GraphQL;

namespace Masa.Blazor.Test.TemplateTable;

[TestClass]
public class CubejsGetQueryTests
{
    [TestMethod]
    public void GetQuery_NotFilterAndSort()
    {
        var query = """
                    users {
                      name
                      age
                      avatar
                      done
                      birthday
                      favoriteBooks
                      level
                      position {
                        name
                      }
                    }
                    """;

        var expected = """
                       query Cube {
                         cube {
                           data: users {
                             name
                         age
                         avatar
                         done
                         birthday
                         favoriteBooks
                         level
                         position {
                           name
                         }
                           }
                         }
                       }
                       """;

        var (actual, _) = CubejsGraphQLClient.GetQuery(new QueryRequest { QueryBody = query });
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetQuery_WithSingleFilter()
    {
        var query = """
                    users {
                      name
                      age
                      avatar
                      done
                      birthday
                      favoriteBooks
                      level
                      position {
                        name
                      }
                    }
                    """;

        var expected = """
                       query Cube {
                         cube {
                           data: users (where: {name: {equals: "John"}}) {
                             name
                         age
                         avatar
                         done
                         birthday
                         favoriteBooks
                         level
                         position {
                           name
                         }
                           }
                         }
                       }
                       """;

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

        var (actual, _) = CubejsGraphQLClient.GetQuery(new QueryRequest
        {
            QueryBody = query,
            FilterRequest = filterRequest
        });
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetQuery_WithSingleSort()
    {
        var query = """
                    users {
                      name
                      age
                      avatar
                      done
                      birthday
                      favoriteBooks
                      level
                      position {
                        name
                      }
                    }
                    """;

        var expected = """
                       query Cube {
                         cube {
                           data: users (orderBy: {name: asc}) {
                             name
                         age
                         avatar
                         done
                         birthday
                         favoriteBooks
                         level
                         position {
                           name
                         }
                           }
                         }
                       }
                       """;

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

        var (actual, _) = CubejsGraphQLClient.GetQuery(new QueryRequest
        {
            QueryBody = query, SortRequest = sortRequest
        });

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetQuery_WithSingleFilterAndSingleSort()
    {
        var query = """
                    users {
                      name
                      age
                      avatar
                      done
                      birthday
                      favoriteBooks
                      level
                      position {
                        name
                      }
                    }
                    """;

        var expected = """
                       query Cube {
                         cube {
                           data: users (where: {name: {equals: "John"}}, orderBy: {name: asc}) {
                             name
                         age
                         avatar
                         done
                         birthday
                         favoriteBooks
                         level
                         position {
                           name
                         }
                           }
                         }
                       }
                       """;

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

        var (actual, _) = CubejsGraphQLClient.GetQuery(new QueryRequest
        {
            QueryBody = query,
            FilterRequest = filterRequest,
            SortRequest = sortRequest
        });

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetQuery_WithFilter_DateKey()
    {
        var query = """
                    users {
                      name
                      age
                      avatar
                      done
                      birthday
                      favoriteBooks
                      level
                      dateKey {
                        value
                      }
                      position {
                        name
                      }
                    }
                    """;

        var expected = """
                       query Cube {
                         cube {
                           data: users (where: {datekey: {afterDate: "2025-01-01"}}) {
                             name
                         age
                         avatar
                         done
                         birthday
                         favoriteBooks
                         level
                         dateKey {
                           value
                         }
                         position {
                           name
                         }
                           }
                         }
                       }
                       """;

        var filterRequest = new Filter
        {
            Operator = FilterOperator.And,
            Options =
            [
                new FilterOption()
                {
                    ColumnId = "datekey.value",
                    Func = StandardFilter.AfterDate,
                    Expected = "2025-01-01",
                    Type = ExpectedType.DateTime,
                }
            ]
        };

        var (actual, _) = CubejsGraphQLClient.GetQuery(new QueryRequest
        {
            QueryBody = query,
            FilterRequest = filterRequest
        });

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetQuery_WithFilter_NestObject()
    {
        var query = """
                    users {
                      name
                      age
                      avatar
                      done
                      birthday
                      favoriteBooks
                      level
                      position {
                        name
                      }
                    }
                    """;

        var expected = """
                       query Cube {
                         cube {
                           data: users (where: {position: {name: {equals: "John"}}}) {
                             name
                         age
                         avatar
                         done
                         birthday
                         favoriteBooks
                         level
                         position {
                           name
                         }
                           }
                         }
                       }
                       """;

        var filterRequest = new Filter
        {
            Operator = FilterOperator.And,
            Options =
            [
                new FilterOption()
                {
                    ColumnId = "position.name",
                    Func = StandardFilter.Equals,
                    Expected = "John",
                    Type = ExpectedType.String,
                }
            ]
        };

        var (actual, _) = CubejsGraphQLClient.GetQuery(new QueryRequest
        {
            QueryBody = query, FilterRequest = filterRequest
        });

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetQuery_WithSort_DateKey()
    {
        var query = """
                    users {
                      name
                      age
                      avatar
                      done
                      birthday
                      favoriteBooks
                      level
                      dateKey {
                        value
                      }
                      position {
                        name
                      }
                    }
                    """;

        var expected = """
                       query Cube {
                         cube {
                           data: users (orderBy: {datekey: desc}) {
                             name
                         age
                         avatar
                         done
                         birthday
                         favoriteBooks
                         level
                         dateKey {
                           value
                         }
                         position {
                           name
                         }
                           }
                         }
                       }
                       """;

        var sortRequest = new Sort
        {
            Options =
            [
                new SortOption()
                {
                    ColumnId = "datekey.value",
                    OrderBy = SortOrder.Desc,
                    Type = ExpectedType.DateTime,
                }
            ]
        };

        var (actual, _) = CubejsGraphQLClient.GetQuery(new QueryRequest
        {
            QueryBody = query,
            SortRequest = sortRequest
        });

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [DataRow(1, 10)]
    [DataRow(2, 10)]
    [DataRow(3, 5)]
    public void GetQuery_WithPagination(int pageIndex, int pageSize)
    {
        var query = """
                    users {
                      name
                      age
                      avatar
                      done
                      birthday
                      favoriteBooks
                      level
                      position {
                        name
                      }
                    }
                    """;

        var expected = $$"""
                         query Cube {
                           cube (offset: {{(pageIndex - 1) * pageSize}}, limit: {{pageSize}}) {
                             data: users {
                               name
                           age
                           avatar
                           done
                           birthday
                           favoriteBooks
                           level
                           position {
                             name
                           }
                             }
                           }
                         }
                         """;


        var (actual, _) = CubejsGraphQLClient.GetQuery(new QueryRequest
        {
            QueryBody = query,
            PageIndex = pageIndex,
            PageSize = pageSize
        });

        Assert.AreEqual(expected, actual);
    }
}