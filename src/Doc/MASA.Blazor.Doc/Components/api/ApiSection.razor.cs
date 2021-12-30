using MASA.Blazor.Doc.Models;
using MASA.Blazor.Doc.Services;
using MASA.Blazor.Doc.Utils;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor.Doc.Components;

public partial class ApiSection
{
    [Parameter]
    public string Name { get; set; }

    [Parameter]
    public string Section { get; set; }

    [CascadingParameter]
    public bool IsChinese { get; set; }

    [Parameter]
    public List<ApiColumn> ApiData { get; set; }

    private string Filter { get; set; }
}