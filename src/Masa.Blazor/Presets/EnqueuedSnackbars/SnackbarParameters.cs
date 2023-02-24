namespace Masa.Blazor.Presets.EnqueuedSnackbars;

public class SnackbarParameters : Snackbar
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

            [nameof(Type)] = Type,
        };
    }

    internal void MapTo(Snackbar component)
    {
        component.Color ??= Color;
        component.ContentClass ??= ContentClass;
        component.Elevation ??= Elevation;
        component.Height ??= Height;

        if (!component.OnClosed.HasDelegate)
        {
            component.OnClosed = OnClosed;
        }

        component.Rounded ??= Rounded;
        component.Width ??= Width;

        component.Content ??= Content;
        component.ActionProps ??= ActionProps;
        component.ActionText ??= ActionText;
        component.OnAction ??= OnAction;

        component.Type = Type;
    }
}