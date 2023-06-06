using Masa.Blazor.Presets;
using Microsoft.AspNetCore.Components;

namespace Masa.Blazor.Playground.Components;

public partial class Diagram
{
    [Parameter] public string? Title { get; set; }

    [Parameter] public string? Icon { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public EventCallback OnDblClick { get; set; }
}
