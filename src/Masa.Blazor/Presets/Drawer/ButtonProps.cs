using BlazorComponent;

namespace Masa.Blazor;

public class ButtonProps
{
    public string Color { get; set; }
    public bool Dense { get; set; }
    public bool Depressed { get; set; }
    public StringNumber Elevation { get; set; }
    public bool Icon { get; set; }
    public bool Fab { get; set; }
    public bool Large { get; set; }
    public bool Plain { get; set; }
    public bool Rounded { get; set; }
    public bool Shaped { get; set; }
    public bool Small { get; set; }
    public bool Text { get; set; }
    public bool Tile { get; set; }
    public bool XLarge { get; set; }
    public bool XSmall { get; set; }
    public bool Ripple { get; set; } = true;
    public bool Outlined { get; set; }

    public Dictionary<string, object> ToDictionary()
    {
        return new Dictionary<string, object>()
        {
            { nameof(Color), Color },
            { nameof(Dense), Dense },
            { nameof(Depressed), Depressed },
            { nameof(Elevation), Elevation },
            { nameof(Icon), Icon },
            { nameof(Fab), Fab },
            { nameof(Large), Large },
            { nameof(Plain), Plain },
            { nameof(Rounded), Rounded },
            { nameof(Shaped), Shaped },
            { nameof(Small), Small },
            { nameof(Text), Text },
            { nameof(Tile), Tile },
            { nameof(XLarge), XLarge },
            { nameof(XSmall), XSmall },
            { nameof(Ripple), Ripple },
            { nameof(Outlined), Outlined },
        };
    }
}