using Masa.Blazor.Doc.Models;
using Microsoft.AspNetCore.Components;

namespace Masa.Blazor.Doc.Components;

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