namespace MASA.Blazor;

public interface ISizeable
{
    bool Large { get; set; }

    bool Small { get; set; }

    bool XLarge { get; set; }

    bool XSmall { get; set; }

    bool Medium => !Small && !XSmall && !Large && !XLarge;

    public string SizeableClasses()
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