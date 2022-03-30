using Masa.Blazor.Doc.Models;
using Microsoft.AspNetCore.Components;

namespace Masa.Blazor.Doc.Components;

public partial class ApiTable
{
    private static readonly Dictionary<string, string[]> HEADERS = new()
    {
        { "options", new[] { "name", "type", "default", "description" } },
        { "contents", new[] { "name", "type", "description" } },
        { "props", new[] { "name", "type", "default", "description" } },
        { "events", new[] { "name", "type", "description" } },
        { "sass", new[] { "name", "default", "description" } },
        { "functions", new[] { "name", "signature", "description" } },
        { "modifiers", new[] { "name", "type", "description" } },
        { "argument", new[] { "type", "description" } },
    };

    [Parameter]
    public string Class { get; set; }

    [EditorRequired]
    [Parameter]
    public string Field { get; set; }

    [Parameter]
    public string Filter { get; set; }

    [Parameter]
    public string Name { get; set; }

    [Parameter]
    public List<ApiColumn> ApiData { get; set; }

    private string[] Headers => HEADERS.TryGetValue(Field, out var value) ? value : Array.Empty<string>();

    private List<Dictionary<string, string>> Items
    {
        get
        {
            var results = !string.IsNullOrWhiteSpace(Filter)
                ? ApiData.Where(item =>
                        item.Name.Contains(Filter, StringComparison.OrdinalIgnoreCase) ||
                        (item.Description ?? "").Contains(Filter, StringComparison.OrdinalIgnoreCase) ||
                        (item.Type ?? "").Contains(Filter, StringComparison.OrdinalIgnoreCase))
                    .ToList()
                : ApiData;

            List<Dictionary<string, string>> items = new();

            foreach (var apiColumn in results)
            {
                Dictionary<string, string> dic = new()
                {
                    ["name"] = apiColumn.Name,
                    ["type"] = apiColumn.Type,
                    ["default"] = apiColumn.Default,
                    ["description"] = apiColumn.Description
                };

                items.Add(dic);
            }

            return items;
        }
    }
}