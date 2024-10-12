using System.Text.Json;
using HotChocolate.Types.Pagination;
using HotChocolateDemo;
using Masa.Blazor.Components.TemplateTable;
using Path = System.IO.Path;

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

app.MapPost("/sheet", async (Sheet sheet) =>
{
    var path = Path.Combine(Environment.CurrentDirectory, "sheet.json");
    // 文件存在则覆盖，不存在则创建
    if (File.Exists(path))
    {
        File.Delete(path);
    }

    var json = JsonSerializer.Serialize(sheet);
    await File.WriteAllTextAsync(path, json);
    return true;
});

app.Run();