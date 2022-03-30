namespace Masa.Blazor.Presets;

public class BlockTextTag
{
    public string Class { get; set; }

    public string Color { get; set; }

    public bool Outlined { get; set; }

    public string Text { get; set; }

    public string TextColor { get; set; }

    public BlockTextTag()
    {
    }

    public BlockTextTag(string text, string color)
    {
        Text = text;
        Color = color;
    }

    public BlockTextTag(string text, string color, bool outlined)
    {
        Text = text;
        Color = color;
        Outlined = outlined;
    }

    public BlockTextTag(string text, string color, string textColor)
    {
        Text = text;
        Color = color;
        TextColor = textColor;
    }
}