using BlazorComponent;
using BlazorComponent.I18n;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System.Globalization;
using Microsoft.JSInterop;

namespace Masa.Blazor.Doc.Shared;

public partial class BaseLayout : IDisposable
{
    [Inject]
    public I18n I18n { get; set; }

    [Inject]
    public NavigationManager Navigation { get; set; }

    [Inject]
    public MasaBlazor MasaBlazor { get; set; }

    [Inject]
    public IJSRuntime Js { get; set; }

    private static readonly CultureInfo[] SupportedCultures = { new("en-US"), new("zh-CN") };

    private StringNumber _selectTab = 0;

    private string CultureIcon => $"{Culture}.png";

    private StringNumber SelectTab
    {
        get
        {
            var relativePath = Navigation.ToBaseRelativePath(Navigation.Uri);

            if (relativePath.Equals("", StringComparison.OrdinalIgnoreCase) ||
                relativePath.Equals("index.html", StringComparison.OrdinalIgnoreCase))
            {
                return 0;
            }

            if (relativePath.Equals("about/meet-the-team", StringComparison.OrdinalIgnoreCase))
            {
                return 2;
            }

            return 1;
        }
        set
        {
            if (value != null && value.AsT1 != 3 && value.AsT1 != 4)
            {
                _selectTab = value;
            }
        }
    }

    public bool Drawer { get; set; } = true;

    public bool ShowSetting { get; set; }

    public bool Temporary { get; set; } = true;

    public CultureInfo Culture { get; set; }

    public void UpdateNav(bool drawer, bool temporary = true)
    {
        Drawer = drawer;
        Temporary = temporary;
    }

    private void TurnLanguage()
    {
        Culture = Equals(Culture, SupportedCultures[0]) ? SupportedCultures[1] : SupportedCultures[0];

        I18n.SetCulture(Culture);
    }

    protected override void OnInitialized()
    {
#if (DEBUG == false)
        RedirectToOfficialWebsite(Navigation.Uri);
#endif

        var culture = I18n.Culture ?? CultureInfo.CurrentCulture;
        if (SupportedCultures.Contains(culture))
        {
            Culture = culture;
        }
        else
        {
            Culture = SupportedCultures[0];
            I18n.SetCulture(Culture);
        }

        Navigation.LocationChanged += OnLocationChanged;
        MasaBlazor.Breakpoint.OnUpdate += BreakpointOnOnUpdate;
    }

    private async Task BreakpointOnOnUpdate()
    {
        await InvokeAsync(async () =>
        {
            // invoke StateHasChanged() to ensure that #docsearch-mobile has been rendered. 
            StateHasChanged();

            await Js.InvokeVoidAsync("addDoSearch", MasaBlazor.Breakpoint.Mobile);
        });
    }

    private void RedirectToOfficialWebsite(string uri)
    {
        var relativePath = Navigation.ToBaseRelativePath(uri);
        if (relativePath == "")
        {
            Navigation.NavigateTo("https://www.masastack.com/blazor");
        }
    }

    private void OnLocationChanged(object sender, LocationChangedEventArgs e)
    {
#if (DEBUG == false)
        RedirectToOfficialWebsite(e.Location);
#endif

        var selectTab = SelectTab;
        if (e.Location.Contains("meet-the-team"))
            SelectTab = 2;
        else if (e.Location != Navigation.BaseUri)
            SelectTab = 1;

        if (selectTab != _selectTab && MasaBlazor.Breakpoint.Mobile)
        {
            _ = InvokeAsync(StateHasChanged);
        }
    }

    private void NavigateToServer()
    {
        const string baseUri = "https://blazor.masastack.com/";
        var absoluteUri = baseUri + Navigation.ToBaseRelativePath(Navigation.Uri);

        Navigation.NavigateTo(absoluteUri);
    }

    private void NavigateToWasm()
    {
        const string baseUri = "https://blazor-wasm.masastack.com/";
        var absoluteUri = baseUri + Navigation.ToBaseRelativePath(Navigation.Uri);

        Navigation.NavigateTo(absoluteUri);
    }

    private void ShowDraw() => UpdateNav(true);

    private string T(string key) => I18n.T(key);

    public void Dispose()
    {
        Navigation.LocationChanged -= OnLocationChanged;
        MasaBlazor.Breakpoint.OnUpdate -= BreakpointOnOnUpdate;
    }
}
