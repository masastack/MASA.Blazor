namespace Masa.Blazor;

public class MToggle<TData> : ComponentBase
{
    [Parameter] public bool Value { get; set; }

    [Parameter] public EventCallback<bool> ValueChanged { get; set; }

    [Parameter] public TData? DataOn { get; set; }

    [Parameter] public TData? DataOff { get; set; }

    [Parameter] public RenderFragment<ToggleContext<TData>>? ChildContent { get; set; }

    private async Task OnToggle()
    {
        Value = !Value;
        await ValueChanged.InvokeAsync(Value);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.AddContent(
            0,
            ChildContent?.Invoke(new ToggleContext<TData>(OnToggle, Value ? DataOn : DataOff, Value))
        );
    }
}

public record ToggleContext<TData>(Func<Task> Toggle, TData? Data, bool On);