#nullable enable
namespace Masa.Blazor.Popup.Components;

public class AlertingPopupComponentBase : PopupComponentBase
{
    [Parameter] public string? Icon { get; set; }

    [Parameter] public string? IconColor { get; set; }

    [Parameter] public AlertTypes? Type { get; set; }

    protected virtual string? ComputedIcon
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(Icon))
            {
                return Icon;
            }

            return Type switch
            {
                AlertTypes.Success => "mdi-check-circle-outline",
                AlertTypes.Error => "mdi-close-circle-outline",
                AlertTypes.Info => "mdi-information-outline",
                AlertTypes.Warning => "mdi-alert-circle-outline",
                _ => null
            };
        }
    }

    protected virtual string? ComputedIconColor
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(IconColor))
            {
                return IconColor;
            }

            return Type switch
            {
                AlertTypes.Success => "success",
                AlertTypes.Info => "info",
                AlertTypes.Warning => "warning",
                AlertTypes.Error => "error",
                _ => null
            };
        }
    }
}