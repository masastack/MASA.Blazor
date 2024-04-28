using Masa.Blazor.Presets.PageStack;

namespace Masa.Blazor.Presets;

public class PStackPageBarInit : IComponent
{
    [CascadingParameter] protected IDefaultsProvider? DefaultsProvider { get; set; }

    [CascadingParameter] private PPageStackItem? PageStackItem { get; set; }

    [Parameter] public string? RerenderKey { get; set; }

    [Parameter] public string? Class { get; set; }

    [Parameter] public string? Style { get; set; }

    [Parameter] public string? Color { get; set; }

    [Parameter] public string? Image { get; set; }

    [Parameter] public int Height { get; set; }

    [Parameter] public bool Dense { get; set; }

    [Parameter] public bool Short { get; set; }

    [Parameter] public bool Flat { get; set; }

    [Parameter] public RenderFragment<PageStackGoBackContext>? BarContent { get; set; }

    [Parameter] public RenderFragment<PageStackGoBackContext>? GoBackContent { get; set; }

    [Parameter] public string? Title { get; set; }

    [Parameter] public bool CenterTitle { get; set; }

    [Parameter] public RenderFragment? ActionContent { get; set; }

    [Parameter] public RenderFragment<Dictionary<string, object?>>? ImageContent { get; set; }

    [Parameter] public RenderFragment? ExtensionContent { get; set; }

    [Parameter] public int ExtensionHeight { get; set; } = 48;

    [Parameter] public bool ElevateOnScroll { get; set; }

    [Parameter] public bool ShrinkOnScroll { get; set; }

    [Parameter] public bool Dark { get; set; }

    [Parameter] public bool Light { get; set; }

    private bool _init;
    private string? _prevRerenderKey;

    public void Attach(RenderHandle renderHandle)
    {
    }

    public Task SetParametersAsync(ParameterView parameters)
    {
        parameters.TryGetValue<PPageStackItem>(nameof(PageStackItem), out var pageStackItem);

        if (pageStackItem is null)
        {
            return Task.CompletedTask;
        }

        var hasRerenderKey = parameters.TryGetValue<string>(nameof(RerenderKey), out var rerenderKey);

        if (_init)
        {
            if (hasRerenderKey)
            {
                if (_prevRerenderKey != rerenderKey)
                {
                    _prevRerenderKey = rerenderKey;
                    SetParameters();
                    Rerender();
                }
            }

            return Task.CompletedTask;
        }

        _init = true;
        _prevRerenderKey = rerenderKey;

        SetParameters();

        PageStackItem!.AppBarContent = BarContent;
        PageStackItem.GoBackContent = GoBackContent;
        PageStackItem.ExtensionHeight = ExtensionHeight;
        PageStackItem.ExtensionContent = ExtensionContent;
        PageStackItem.ImageContent = ImageContent;
        PageStackItem.AppBarColor = Color;
        PageStackItem.AppBarClass = Class;
        PageStackItem.AppBarStyle = Style;
        PageStackItem.AppBarHeight = Height;
        PageStackItem.AppBarFlat = Flat;
        PageStackItem.AppBarDense = Dense;
        PageStackItem.AppBarShort = Short;
        PageStackItem.AppBarTitle = Title;
        PageStackItem.CenterTitle = CenterTitle;
        PageStackItem.AppBarImage = Image;
        PageStackItem.ElevateOnScroll = ElevateOnScroll;
        PageStackItem.ShrinkOnScroll = ShrinkOnScroll;
        PageStackItem.AppBarDark = Dark;
        PageStackItem.AppBarLight = Light;

        PageStackItem.ActionContent = ActionContent;

        Rerender();

        return Task.CompletedTask;

        void SetParameters()
        {
            if (parameters.TryGetValue<IDefaultsProvider>(nameof(DefaultsProvider), out var defaultsProvider)
                && defaultsProvider.Defaults is not null
                && defaultsProvider.Defaults.TryGetValue(nameof(PStackPageBarInit), out var dictionary)
                && dictionary is not null)
            {
                var defaults = ParameterView.FromDictionary(dictionary);
                defaults.SetParameterProperties(this);
            }

            parameters.SetParameterProperties(this);
        }
    }

    [MasaApiPublicMethod]
    public void Rerender()
    {
        PageStackItem?.Render();
    }
}