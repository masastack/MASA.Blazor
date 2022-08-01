namespace Masa.Blazor;

public static class JsInitVariables
{
    /// <summary>
    /// Initialize the width and height of the page when it is first loaded
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    [JSInvokable]
    public static void InitWidthAndHeight(double width, double height)
    {
        MasaBlazorVariables.Width = width;
        MasaBlazorVariables.Height = height;
    }
}
