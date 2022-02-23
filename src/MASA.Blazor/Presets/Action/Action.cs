using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Masa.Blazor.Presets;

public class Action
{
    public string Color { get; set; }
    
    public bool Dark { get; set; }
    
    public bool Disabled { get; set; }

    public bool Depressed { get; set; }
    
    public string Icon { get; set; }

    public string Label { get; set; }
    
    public bool Large { get; set; }
    
    public bool Light { get; set; }

    public EventCallback<MouseEventArgs> OnClick { get; set; }

    public bool Outlined { get; set; }

    public bool Plain { get; set; }

    public bool Rounded { get; set; }

    public bool Small { get; set; }

    public bool Text { get; set; }

    public bool Tile { get; set; }
    
    public string Tip { get; set; }

    public bool Visible { get; set; } = true;

    public bool XSmall { get; set; }

    public bool XLarge { get; set; }
}