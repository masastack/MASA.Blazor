namespace Masa.Blazor.Popup.Components;

public class AlertParameters : Alert
{
    public Dictionary<string, object> ToDictionary()
    {
        return new Dictionary<string, object>()
        {
            [nameof(Bottom)] = Bottom,
            [nameof(Centered)] = Centered,
            [nameof(Color)] = Color,
            [nameof(ContentClass)] = ContentClass,
            [nameof(Dark)] = Dark,
            [nameof(Elevation)] = Elevation,
            [nameof(Height)] = Height,
            [nameof(Left)] = Left,
            [nameof(Light)] = Light,
            [nameof(MultiLine)] = MultiLine,
            [nameof(OnClosed)] = OnClosed,
            [nameof(Outlined)] = Outlined,
            [nameof(Right)] = Right,
            [nameof(Rounded)] = Rounded,
            [nameof(Shaped)] = Shaped,
            [nameof(Text)] = Text,
            [nameof(Tile)] = Tile,
            [nameof(Timeout)] = Timeout,
            [nameof(Top)] = Top,
            [nameof(Vertical)] = Vertical,
            [nameof(Width)] = Width,

            [nameof(ActionProps)] = ActionProps,
            [nameof(ActionText)] = ActionText,
            [nameof(Content)] = Content,
            [nameof(OnAction)] = OnAction,

            [nameof(Icon)] = Icon,
            [nameof(IconColor)] = IconColor,
            [nameof(Type)] = Type,
            [nameof(Attributes)] = Attributes,
        };
    }

    internal void MapTo(Alert component)
    {
        component.Bottom ??= Bottom;
        component.Centered ??= Centered;
        component.Color ??= Color;
        component.ContentClass ??= ContentClass;
        component.Dark ??= Dark;
        component.Elevation ??= Elevation;
        component.Height ??= Height;
        component.Left ??= Left;
        component.Light ??= Light;
        component.MultiLine ??= MultiLine;

        if (!component.OnClosed.HasDelegate)
        {
            component.OnClosed = OnClosed;
        }

        component.Outlined ??= Outlined;
        component.Right ??= Right;
        component.Rounded ??= Rounded;
        component.Shaped ??= Shaped;
        component.Text ??= Text;
        component.Tile ??= Tile;
        component.Timeout ??= Timeout;
        component.Top ??= Top;
        component.Vertical ??= Vertical;
        component.Width ??= Width;

        component.Content ??= Content;
        component.ActionProps ??= ActionProps;
        component.ActionText ??= ActionText;
        component.OnAction ??= OnAction;

        component.Icon ??= Icon;
        component.IconColor ??= IconColor;
        component.Type ??= Type;
        component.Attributes ??= Attributes;
    }
}