using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text.Json;
using HotChocolate.Types.Pagination;
using HotChocolateDemo;
using Masa.Blazor.Components.TemplateTable;
using Masa.Blazor.Components.TemplateTable.ColumnConfigs;
using Microsoft.AspNetCore.Authorization;
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

// app.MapGet("/sheet", () =>
// {
//     var queryBody = """
//                     listdetail {
//                         traceid
//                         spanid
//                         servicename
//                         resources
//                         statuscode
//                         namespace
//                         serviceversion
//                         httpstatuscode
//                         requestcontentbody
//                         url
//                         responsecontentbody
//                         datekey { value }
//                     }
//                     """;
//
//     var columns = new List<Column>
//     {
//         new Column("traceid", "Trace Id", ColumnType.Text),
//         new Column("spanid", "Span Id", ColumnType.Text),
//         new Column("servicename", "Service Name", ColumnType.Select, new SelectConfig()
//         {
//             Color = true,
//             Options =
//             [
//                 new SelectOption("Scheduler Worker", "scheduler-worker"),
//                 new SelectOption("IoT Device", "masa-iot-service-device"),
//                 new SelectOption("IoT Filter", "masa-iot-service-filter")
//             ]
//         }),
//         new Column("resources", "Resources", ColumnType.Text),
//         new Column("statuscode", "Status Code", ColumnType.Text),
//         new Column("namespace", "Namespace", ColumnType.Text),
//         new Column("serviceversion", "Service Version", ColumnType.Text),
//         new Column("httpstatuscode", "HTTP Status Code", ColumnType.Text),
//         new Column("requestcontentbody", "Request Content Body", ColumnType.Text),
//         new Column("url", "URL", ColumnType.Text),
//         new Column("responsecontentbody", "Response Content Body", ColumnType.Text),
//         new Column("datekey.value", "Date", ColumnType.Date, new DateConfig()
//         {
//             Format = "yyyy-MM-dd HH:mm:ss"
//         })
//     };
//
//     var defaultView = new View("Default View", columns.Select(c => new ViewColumn(c.Id)).ToList());
//     defaultView.Filter = new Filter()
//     {
//         Options = new List<FilterOption>()
//         {
//             new FilterOption()
//             {
//                 ColumnId = "datekey.value",
//                 Func = StandardFilter.AfterDate,
//                 Expected = DateTime.Now.Date.ToString("yyyy-MM-dd"),
//                 Type = ExpectedType.DateTime,
//                 Persistent = true
//             }
//         },
//     };
//     defaultView.Sort = new Sort()
//     {
//         Options = new List<SortOption>()
//         {
//             new SortOption
//             {
//                 ColumnId = "datekey.value",
//                 OrderBy = SortOrder.Desc,
//                 Type = ExpectedType.DateTime,
//                 Persistent = true
//             }
//         }
//     };
//
//     var sheet = new Sheet
//     {
//         QueryBody = queryBody,
//         CountField = "cnt",
//         ItemKeyName = "SpanId",
//         Columns = columns,
//         Views = [defaultView],
//         DefaultViewId = defaultView.Id,
//         ActiveViewId = defaultView.Id,
//     };
//
//     return sheet;
// });

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