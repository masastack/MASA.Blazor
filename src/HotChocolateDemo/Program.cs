using System.Security.Cryptography;
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

app.MapPost("/views/{userId}", async (View view, string userId) =>
{
    var path = Path.Combine(Environment.CurrentDirectory, $"views-{userId}.json");
    List<View> views = [];
    if (File.Exists(path))
    {
        var json = File.ReadAllText(path);
        views = JsonSerializer.Deserialize<List<View>>(json);
    }

    var got = views.FirstOrDefault(v => v.Id == view.Id);
    if (got is not null)
    {
        views.Remove(got);
    }

    views.Add(view);
    var json2 = JsonSerializer.Serialize(views);
    await File.WriteAllTextAsync(path, json2);
});

app.MapGet("/views/{userId}", (string userId) =>
{
    var path = Path.Combine(Environment.CurrentDirectory, $"views-{userId}.json");
    if (!File.Exists(path))
    {
        return [];
    }

    var json = File.ReadAllText(path);
    return JsonSerializer.Deserialize<List<View>>(json);
});

app.MapDelete("/views/{userId}/{viewId}", (string userId, Guid viewId) =>
{
    var path = Path.Combine(Environment.CurrentDirectory, $"views-{userId}.json");
    if (!File.Exists(path))
    {
        return false;
    }

    var json = File.ReadAllText(path);
    var views = JsonSerializer.Deserialize<List<View>>(json);
    var view = views.FirstOrDefault(v => v.Id == viewId);
    if (view is null)
    {
        return false;
    }

    views.Remove(view);
    var json2 = JsonSerializer.Serialize(views);
    File.WriteAllText(path, json2);
    return true;
});

app.Run();