namespace Masa.Blazor.Swiper.Modules;

public abstract class SwiperModuleBase : IComponent
{
    [CascadingParameter] protected MSwiper? Swiper { get; set; }

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
        if (Swiper == null)
        {
            return;
        }

        await Swiper.AddModuleAsync(this);
    }
}
