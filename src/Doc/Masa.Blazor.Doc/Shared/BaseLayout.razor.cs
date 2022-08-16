using BlazorComponent;
using BlazorComponent.I18n;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System.Globalization;

namespace Masa.Blazor.Doc.Shared;

public partial class BaseLayout : IDisposable
{
    [Inject]
    public I18n I18n { get; set; }

    [Inject]
    public NavigationManager Navigation { get; set; }

    [Inject]
    public MasaBlazor MasaBlazor { get; set; }

    private static readonly CultureInfo[] SupportedCultures = { new("en-US"), new("zh-CN") };

    private string _searchBorderColor = "#00000000";
    private StringNumber _selectTab = 0;

    private string CultureIcon => $"{Culture}.png";

    public StringNumber SelectTab
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
        RedirectToOfficialWebsite(Navigation.Uri);

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
        RedirectToOfficialWebsite(e.Location);

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

    private void ShowDraw() => UpdateNav(true);

    private string T(string key) => I18n.T(key);

    public void Dispose()
    {
        Navigation.LocationChanged -= OnLocationChanged;
    }
}
