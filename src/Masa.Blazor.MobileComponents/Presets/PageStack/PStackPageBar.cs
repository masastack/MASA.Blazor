using Masa.Blazor.Presets.PageStack;

namespace Masa.Blazor.Presets;

public class PStackPageBar : IComponent
{
    [CascadingParameter] protected IDefaultsProvider? DefaultsProvider { get; set; }

    [CascadingParameter] private PPageStackItem? PageStackItem { get; set; }

    [Parameter] public string? Class { get; set; }

    [Parameter] public string? Style { get; set; }

    [Parameter] public bool Collapse { get; set; }

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

    [Parameter] public bool ElevateOnScroll { get; set; } = true;

    [Parameter] public bool ShrinkOnScroll { get; set; }

    [Parameter] public string? Theme { get; set; }

    /// <summary>
    /// Determines whether the bar overlaps the content below it.
    /// </summary>
    [Parameter] public bool Overlap { get; set; }

    [Parameter] public StringNumber? Elevation { get; set; }

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

        SetParameters();
        Rerender();

        return Task.CompletedTask;

        void SetParameters()
        {
            if (parameters.TryGetValue<IDefaultsProvider>(nameof(DefaultsProvider), out var defaultsProvider)
                && defaultsProvider.Defaults is not null
                && defaultsProvider.Defaults.TryGetValue(nameof(PStackPageBar), out var dictionary)
                && dictionary is not null)
            {
                var defaults = ParameterView.FromDictionary(dictionary);
                defaults.SetParameterProperties(this);
            }

            parameters.SetParameterProperties(this);

            PageStackItem!.Visible = true;
            PageStackItem.AppBarContent = BarContent;
            PageStackItem.GoBackContent = GoBackContent;
            PageStackItem.ExtensionHeight = ExtensionHeight;
            PageStackItem.ExtensionContent = ExtensionContent;
            PageStackItem.ImageContent = ImageContent;
            PageStackItem.AppBarColor = Color;
            PageStackItem.AppBarClass = Class;
            PageStackItem.Elevation = Elevation;
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
            PageStackItem.AppBarTheme = Theme;
            PageStackItem.ActionContent = ActionContent;
            PageStackItem.Overlap = Overlap;
            PageStackItem.AppBarCollapse = Collapse;
        }
    }

    private void Rerender()
    {
        PageStackItem?.Render();
    }
}