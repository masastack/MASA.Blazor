using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MASA.Blazor.Presets;

public class Action
{
    public string Color { get; set; }
    
    public bool Disabled { get; set; }
    
    public string Icon { get; set; }

    public string Label { get; set; }

    public EventCallback<MouseEventArgs> OnClick { get; set; }

    public bool Visible { get; set; } = true;
}