namespace Masa.Blazor.Components.Xgplayer.Plugins;

public class XgplayerPluginBase : IComponent
{
    [CascadingParameter] protected IXgplayer? Player { get; set; }

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
        if (Player == null)
        {
            return;
        }

        await Player.ConfigPluginAsync(this);
    }
}
