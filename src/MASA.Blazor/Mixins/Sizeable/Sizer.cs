namespace MASA.Blazor;

public class Sizer : ISizeable
{
    private readonly Dictionary<string, string> _sizeMap = new()
    {
        { nameof(XSmall), "12px" },
        { nameof(Small), "16px" },
        { "Default", "24px" },
        { nameof(Medium), "28px" },
        { nameof(Large), "36px" },
        { nameof(XLarge), "40px" },
    };

    public Sizer()
    {
    }

    public Sizer(ISizeable sizeable)
    {
        Large = sizeable.Large;
        Small = sizeable.Small;
        XLarge = sizeable.XLarge;
        XSmall = sizeable.XSmall;
    }

    public bool Large { get; set; }
    public bool Small { get; set; }
    public bool XLarge { get; set; }
    public bool XSmall { get; set; }

    bool Medium => !Small && !XSmall && !Large && !XLarge;

    public string SizeableClasses
    {
        get
        {
            var medium = Medium;

            var css = string.Empty;

            if (XSmall)
            {
                css += "m-size--x-small ";
            }

            if (Small)
            {
                css += "m-size--small ";
            }

            if (medium)
            {
                css += "m-size--default ";
            }

            if (Large)
            {
                css += "m-size--large ";
            }

            if (XLarge)
            {
                css += "m-size--x-large ";
            }

            return css[..^1];
        }
    }

    public string GetSize()
    {
        var sizes = new Dictionary<string, bool>()
        {
            { nameof(XSmall), XSmall },
            { nameof(Small), Small },
            { nameof(Medium), Medium },
            { nameof(Large), Large },
            { nameof(XLarge), XLarge },
        };

        var key = sizes.FirstOrDefault(item => item.Value).Key;

        if (key != null && _sizeMap.TryGetValue(key, out var px))
        {
            return px;
        }

        return null;
    }
}