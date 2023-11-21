namespace Masa.Blazor;

public partial class MSplitterPane : IComponent, IAsyncDisposable
{
    [CascadingParameter] public MSplitter? Splitter { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public string? Class { get; set; }

    [Parameter] public string? Style { get; set; }

    [Parameter] public double Size { get; set; }

    [Parameter] [MassApiParameter(100)] public double Max { get; set; } = 100;

    [Parameter] public double Min { get; set; }

    private bool _initialized;

    internal double InternalSize { get; set; }

    internal int InternalIndex { get; set; }

    public void Attach(RenderHandle renderHandle)
    {
    }

    public async Task SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);

        if (!_initialized)
        {
            _initialized = true;

            await OnInitializedAsync();
        }
    }

    private async Task OnInitializedAsync()
    {
        if (Splitter == null)
        {
            return;
        }

        await Splitter.RegisterAsync(this);
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        try
        {
            if (Splitter == null)
            {
                return;
            }

            await Splitter.UnregisterAsync(this);
        }
        catch (Exception)
        {
            // ignored
        }
    }
}
