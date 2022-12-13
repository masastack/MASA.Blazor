namespace Masa.Docs.Core.Components;

public class ColorChip : MChip
{
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (Color is null) return;

        if (Color.StartsWith("#"))
        {
            Style ??= string.Empty;

            if (!Style.Contains("color"))
            {
                Style += $"color:{Color};";
            }

            var rgba = GenRgba(Color, 0.2f);

            if (!Style.Contains("background-color"))
            {
                Style += $"background-color:{rgba};";
            }
        }
        else
        {
            Class ??= string.Empty;

            if (!Class.Contains("lighten-5"))
            {
                Class += $"lighten-5 {Color}--text";
            }
        }
    }

    private string GenRgba(string hex, float opacity)
    {
        var color = System.Drawing.ColorTranslator.FromHtml(hex);
        var r = Convert.ToInt16(color.R);
        var g = Convert.ToInt16(color.G);
        var b = Convert.ToInt16(color.B);

        return $"rgba({r},{g},{b},{opacity})";
    }
}