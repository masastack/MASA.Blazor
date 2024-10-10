using HotChocolate.Types.Pagination;
using HotChocolateDemo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddFiltering()
    .AddSorting()
    .SetPagingOptions(new PagingOptions()
    {
        IncludeTotalCount = true
    });

var app = builder.Build();

app.MapGraphQL();
app.MapGet("/", () => "Hello World!");

app.Run();