using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor.Presets;

public partial class PDrawer
{
    private bool _okLoading;

    private string ComputedClass => Left ? "m-dialog--drawer-left" : "m-dialog--drawer-right";

    private string ComputedContentHeight => OnOk.HasDelegate ? "height: calc(100% - 116px)" : "height: calc(100% - 63px)";

    private string ComputedTransition => Left ? "dialog-left-transition" : "dialog-right-transition";

    private ButtonProps InternalOkButtonProps
    {
        get
        {
            var props = GetDefaultOkBtnProps();
            OkBtnProps?.Invoke(props);
            return props;
        }
    }

    private ButtonProps InternalCancelButtonProps
    {
        get
        {
            var props = GetDefaultCancelBtnProps();
            CancelBtnProps?.Invoke(props);
            return props;
        }
    }

    [Parameter]
    public RenderFragment<ActivatorProps> ActivatorContent { get; set; }

    [Parameter]
    public Action<ButtonProps> CancelBtnProps { get; set; }

    [Parameter]
    public RenderFragment CancelContent { get; set; }

    [Parameter]
    public string Class { get; set; }

    [Parameter]
    public string CancelText { get; set; } = "取消";

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    [Parameter]
    public bool Left { get; set; }

    [Parameter]
    public Action<ButtonProps> OkBtnProps { get; set; }

    [Parameter]
    public RenderFragment OkContent { get; set; }

    [Parameter]
    public string OkText { get; set; } = "确定";

    [Parameter]
    public EventCallback OnCancel { get; set; }

    [Parameter]
    public EventCallback OnOk { get; set; }

    [Parameter]
    public bool Persistent { get; set; }

    [Parameter]
    public bool Right { get; set; } = true;

    [Parameter]
    public string Title { get; set; }

    [Parameter]
    public bool Value { get; set; }

    [Parameter]
    public EventCallback<bool> ValueChanged { get; set; }

    [Parameter]
    public StringNumber Width { get; set; } = "33.33%";

    private void HandleOnCancel()
    {
        if (OnCancel.HasDelegate)
        {
            OnCancel.InvokeAsync();
        }
        else
        {
            UpdateValue(false);
        }
    }

    private async Task HandleOnOk()
    {
        _okLoading = true;

        if (OnOk.HasDelegate)
        {
            await OnOk.InvokeAsync();
        }
        else
        {
            UpdateValue(false);
        }

        _okLoading = false;
    }

    private void UpdateValue(bool value)
    {
        if (ValueChanged.HasDelegate)
        {
            ValueChanged.InvokeAsync(value);
        }
        else
        {
            Value = value;
        }
    }

    private static ButtonProps GetDefaultCancelBtnProps() => new()
    {
        Text = true,
        Color = "primary"
    };

    private static ButtonProps GetDefaultOkBtnProps() => new()
    {
        Text = true,
        Color = "primary"
    };
}