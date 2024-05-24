using Masa.Blazor.Components.Transition;
using StyleBuilder = Masa.Blazor.Core.StyleBuilder;

namespace Masa.Blazor;

public partial class MSystemBar : MasaComponentBase, IThemeable, ITransitionIf
{
    [Inject] public MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter] public string? Color { get; set; }

    [Parameter]
    public StringNumber? Height
    {
        get => GetValue<StringNumber>();
        set => SetValue(value);
    }

    [Parameter] public bool LightsOut { get; set; }

    [Parameter]
    public bool Window
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    [Parameter] public bool Absolute { get; set; }

    [Parameter]
    public bool App
    {
        get => GetValue<bool>();
        set => SetValue(value);
    }

    [Parameter] public bool Fixed { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

    [Parameter] public bool If { get; set; } = true;

    [Parameter] public bool Dark { get; set; }

    [Parameter] public bool Light { get; set; }

    [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

    public bool IsDark => Dark ? true : (Light ? false : CascadingIsDark);

    protected virtual async Task HandleOnClickAsync(MouseEventArgs args)
    {
        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync();
        }
    }

    private StringNumber ComputedHeight => Height?.ToString() != null
        ? (Regex.IsMatch(Height.ToString()!, "^[0-9]*$") ? Height.ToInt32() : Height)
        : (Window ? 32 : 24);

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher.Watch<bool>(nameof(App), (_, prev) =>
            {
                if (prev)
                {
                    RemoveApplication(true);
                }
                else
                {
                    CallUpdate();
                }
            }, immediate: true)
            .Watch<bool>(nameof(Window), CallUpdate)
            .Watch<StringNumber>(nameof(Height), CallUpdate);
    }

    private bool IndependentTheme =>
        (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

#if NET8_0_OR_GREATER
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
        }
#endif
    
    private Block _block = new("m-system-bar");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block
            .Modifier(LightsOut)
            .And(Absolute)
            .And("fixed", !Absolute && (App || Fixed))
            .And(Window)
            .AddTheme(IsDark, IndependentTheme)
            .AddBackgroundColor(Color)
            .GenerateCssClasses();
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        return StyleBuilder.Create().AddBackgroundColor(Color).AddHeight(ComputedHeight).GenerateCssStyles();
    }

    private async void CallUpdate()
    {
        await NextTickIf(async () => { await UpdateApplicationAsync(); }, () => Ref.Context is null);
    }

    private async Task UpdateApplicationAsync()
    {
        if (!App)
        {
            return;
        }

        var height = ComputedHeight.ToDouble();
        MasaBlazor.Application.Bar = height > 0 ? height : await GetClientHeightAsync();
    }

    private async Task<double> GetClientHeightAsync()
    {
        if (Ref.Context == null)
        {
            return 0;
        }

        var element = await Js.InvokeAsync<BlazorComponent.Web.Element>(JsInteropConstants.GetDomInfo, Ref);
        return element?.ClientHeight ?? 0;
    }

    private void RemoveApplication(bool force = false)
    {
        if (!force && !App)
        {
            return;
        }

        MasaBlazor.Application.Bar = 0;
    }

    protected override ValueTask DisposeAsyncCore()
    {
        RemoveApplication();
        return base.DisposeAsyncCore();
    }
}