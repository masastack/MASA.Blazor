namespace Masa.Blazor.Popup.Components;

public class ConfirmParameters : Confirm
{
    public Dictionary<string, object> ToDictionary()
    {
        return new Dictionary<string, object>
        {
            [nameof(ActionsClass)] = ActionsClass,
            [nameof(ActionsStyle)] = ActionsStyle,
            [nameof(CancelProps)] = CancelProps,
            [nameof(CancelText)] = CancelText,
            [nameof(Content)] = Content,
            [nameof(ContentClass)] = ContentClass,
            [nameof(ContentStyle)] = ContentStyle,
            [nameof(OnOk)] = OnOk,
            [nameof(OkProps)] = OkProps,
            [nameof(OkText)] = OkText,
            [nameof(Title)] = Title,
            [nameof(TitleClass)] = TitleClass,
            [nameof(TitleStyle)] = TitleStyle,

            [nameof(Icon)] = Icon,
            [nameof(IconColor)] = IconColor,
            [nameof(Type)] = Type,
            [nameof(Attributes)] = Attributes,
        };
    }

    internal void MapTo(Confirm component)
    {
        component.ActionsClass ??= ActionsClass;
        component.ActionsStyle ??= ActionsStyle;
        component.CancelProps ??= CancelProps;
        component.CancelText ??= CancelText;
        component.Content ??= Content;
        component.ContentClass ??= ContentClass;
        component.ContentStyle ??= ContentStyle;
        component.OkProps ??= OkProps;
        component.OkText ??= OkText;
        component.OnOk ??= OnOk;
        component.Title ??= Title;
        component.TitleClass ??= TitleClass;
        component.TitleStyle ??= TitleStyle;

        component.Icon ??= Icon;
        component.IconColor ??= IconColor;
        component.Type ??= Type;
        component.Attributes ??= Attributes;
    }
}