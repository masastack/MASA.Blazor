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

app.MapGet("/sheet", () =>
{
    var query = new Query();
    return query.GetSheet();
});

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

app.MapPost("/views/{user-id}", async (Sheet sheet, string userId) =>
{
    var path = Path.Combine(Environment.CurrentDirectory, $"views-{userId}.json");
    // 文件存在则覆盖，不存在则创建
    if (File.Exists(path))
    {
        File.Delete(path);
    }

    var json = JsonSerializer.Serialize(sheet);
    await File.WriteAllTextAsync(path, json);
    return true;
});

app.MapGet("/views/{user-id}", (string userId) =>
{
    var path = Path.Combine(Environment.CurrentDirectory, $"views-{userId}.json");
    if (!File.Exists(path))
    {
        return [];
    }

    var json = File.ReadAllText(path);
    return JsonSerializer.Deserialize<List<View>>(json);
});

app.Run();