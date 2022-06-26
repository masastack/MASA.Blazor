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
        
    private string _searchBorderColor = "#00000000";
    private bool _isShowMiniLogo = true;
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
    
    public string Culture { get; set; }

    public void UpdateNav(bool drawer, bool temporary = true)
    {
        Drawer = drawer;
        Temporary = temporary;
    }

    private void TurnLanguage()
    {
        Culture = Culture == "en-US" ? "zh-CN" : "en-US";
        I18n.SetCulture(Culture);
    }

    protected override void OnInitialized()
    {
        Console.WriteLine($"{DateTime.Now.ToLongTimeString()} I18n.Language:{I18n.Culture} CurrentCulture:{CultureInfo.CurrentCulture.Name}");
        
        Culture = I18n.Culture ?? CultureInfo.CurrentCulture.Name;
        Navigation.LocationChanged += OnLocationChanged;
    }

    private void OnLocationChanged(object sender, LocationChangedEventArgs e)
    {
        var isShowMiniLogo = _isShowMiniLogo;

        if (e.Location == Navigation.BaseUri)
            _isShowMiniLogo = true;
        else
            _isShowMiniLogo = false;

        var selectTab = SelectTab;
        if (e.Location.Contains("meet-the-team"))
            SelectTab = 2;
        else if (e.Location != Navigation.BaseUri)
            SelectTab = 1;

        if ((isShowMiniLogo != _isShowMiniLogo || selectTab != _selectTab) && MasaBlazor.Breakpoint.Mobile)
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