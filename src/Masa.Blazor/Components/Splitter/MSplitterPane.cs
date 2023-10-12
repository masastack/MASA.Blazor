namespace Masa.Blazor;

public partial class MSplitterPane : IComponent
{
    [CascadingParameter] public MSplitter? Splitter { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public string? Class { get; set; }

    [Parameter] public string? Style { get; set; }

    private bool _initialized;

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
}
