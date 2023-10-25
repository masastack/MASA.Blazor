using System.Globalization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;

namespace Masa.Docs.Shared.Shared;

public partial class BaseLayout
{
    private static readonly Project s_emptyProject = new("MASA Stack",
        "https://cdn.masastack.com/stack/images/logo/MASAStack/logo.png?x-oss-process=image/resize,h_24,m_lfit",
        "https://github.com/masastack");

    private bool? _showSettings;
    private bool? _showMobileMenuList;
    private bool _hideAppBarNavIcon;
    private string? _env;
    private string? _project;
    private Project? _projectInfo;
    private CultureInfo? _culture;
    private Dictionary<string, Project> _projectMap = new();

    internal Action? OnAppBarNavIconClick { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        NavigationManager.LocationChanged += NavigationManagerOnLocationChanged;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await InitTheme();
            await InitRTL();
            await InitLang();

            _projectMap = await DocService.ReadProjectMapAsync();

            SetEnv();

            SetProject();
            UpdateProjectInfo();

            StateHasChanged();
        }
    }

    private async Task InitRTL()
    {
        var rtlStr = await LocalStorage.GetItemAsync("masablazor@rtl");
        MasaBlazor.RTL = rtlStr == "rtl";
    }

    private async Task InitTheme()
    {
        var themeStr = await LocalStorage.GetItemAsync("masablazor@theme");

        switch (themeStr)
        {
            case "light" when MasaBlazor.Theme.Dark:
            case "dark" when !MasaBlazor.Theme.Dark:
                MasaBlazor.ToggleTheme();
                break;
            case "system" or null:
                try
                {
                    var darkPrefer = await JSRuntime.InvokeAsync<bool>("isDarkPreferColor");
                    if (darkPrefer != MasaBlazor.Theme.Dark)
                    {
                        MasaBlazor.ToggleTheme();
                    }
                }
                catch (JSException)
                {
                    // ignored
                }

                break;
        }
    }

    private async Task InitLang()
    {
        var langStr = await LocalStorage.GetItemAsync("masablazor@lang");
        if (langStr is not null)
        {
            _culture = new CultureInfo(langStr);
            I18n.SetCulture(_culture);
        }
    }

    private void NavigationManagerOnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        var changed = SetProject();

        if (changed)
        {
            UpdateProjectInfo();

            InvokeAsync(StateHasChanged);
        }
    }

    private void UpdateProjectInfo()
    {
        if (!(_project is not null && _projectMap.TryGetValue(_project, out _projectInfo)))
        {
            _projectInfo = s_emptyProject;
        }
    }

    private void OnCultureChanged(string cultureName)
    {
        _culture = new CultureInfo(cultureName);
        _ = LocalStorage.SetItemAsync("masablazor@lang", cultureName);
        I18n.SetCulture(_culture);
    }

    private async Task RefreshPage()
    {
        await RefreshToSkipWaiting();
    }

    private async Task RefreshToSkipWaiting()
    {
        await JSRuntime.InvokeVoidAsync("swSkipWaiting");
    }

    private void HandleOnAppBarNavIconClick()
    {
        OnAppBarNavIconClick?.Invoke();
    }

    private void HandleOnDotClick()
    {
        _showMobileMenuList = !_showMobileMenuList;
    }

    private void HandleSettingsClick()
    {
        _showSettings = !_showSettings;
    }

    internal void HideAppBarNavIcon()
    {
        if (_hideAppBarNavIcon)
        {
            return;
        }

        _hideAppBarNavIcon = true;
        StateHasChanged();
    }

    internal void ShowAppBarNavIcon()
    {
        if (_hideAppBarNavIcon is false)
        {
            return;
        }

        _hideAppBarNavIcon = false;
        StateHasChanged();
    }

    private void SetEnv()
    {
        var uri = NavigationManager.BaseUri.Replace("http://", "").Replace("https://", "").TrimEnd('/');
        _env = uri switch
        {
            "docs.masastack.com" or "blazor.masastack.com"         => "prd_",
            "blazor-dev.masastack.com" or "docs-dev.masastack.com" => "dev_",
            _                                                      => "local_"
        };
    }

    private bool SetProject()
    {
        var segments = NavigationManager.GetSegments();
        var secondSegment = segments.ElementAtOrDefault(1);
        secondSegment = secondSegment?.TrimEnd('/');
        if (secondSegment is null || _projectMap.ContainsKey(secondSegment))
        {
            if (_project != secondSegment)
            {
                _project = secondSegment;
                return true;
            }
        }

        return false;
    }

    public void Dispose()
    {
        NavigationManager.LocationChanged -= NavigationManagerOnLocationChanged;
    }
}
