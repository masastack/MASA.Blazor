namespace Masa.Blazor.Components.TemplateTable.Actions;

public record RowActionsContext
{
    private CancellationTokenSource? _cts;

    internal event Action? StateChanged;

    internal List<RowActionBase> DisplayActions { get; set; } = [];

    internal List<RowActionBase> MenuActions { get; set; } = [];

    internal int PlaceholderCount => DisplayActions.Count + (MenuActions.Count > 0 ? 1 : 0);

    // 32 is the padding of the actions column
    // 1 is the border of the actions column
    internal int Width => PlaceholderCount * 28 + 32 + 1;

    internal async Task RegisterActionAsync(RowActionBase action)
    {
        if (action.Menu)
        {
            MenuActions.Add(action);
        }
        else
        {
            DisplayActions.Add(action);
        }

        _cts?.Cancel();
        _cts = new CancellationTokenSource();

        try
        {
            await Task.Delay(100, _cts.Token);
            StateChanged?.Invoke();
        }
        catch (TaskCanceledException)
        {
            // ignore
        }
    }
}