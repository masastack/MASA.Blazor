#nullable enable
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;

namespace Masa.Blazor.Presets;

public partial class PPageTabs : NextTickComponentBase
{
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private I18n I18n { get; set; } = null!;

    [Inject]
    private IPopupService PopupService { get; set; } = null!;

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public RenderFragment<PageTabPathValue>? TabContent { get; set; }

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    [Parameter]
    public string? TabClass { get; set; }

    [Parameter]
    [ApiDefaultValue(true)]
    public bool Ripple { get; set; } = true;

    [Parameter]
    public bool HideSlider { get; set; }

    [Parameter]
    public string? ReloadTabText { get; set; }

    [Parameter]
    public string? CloseTabsToTheLeftText { get; set; }

    [Parameter]
    public string? CloseTabsToTheRightText { get; set; }

    [Parameter]
    public string? CloseOtherTabsText { get; set; }

    [Parameter]
    public IEnumerable<string>? SelfPaths { get; set; }

    private readonly List<string> _absolutePaths = new();

    private bool _menuValue;
    private string? _contextmenuPath;

    private double _positionX;
    private double _positionY;

    private MTabs? _tabs;

    internal EventHandler<string>? TabClosed;
    internal EventHandler<string>? TabReload;
    internal EventHandler<string[]>? TabsUpdated;

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        ReloadTabText = I18n.T("$masaBlazor.pageTabs.reloadTab");
        CloseTabsToTheLeftText = I18n.T("$masaBlazor.pageTabs.closeTabsToTheLeft");
        CloseTabsToTheRightText = I18n.T("$masaBlazor.pageTabs.closeTabsToTheRight");
        CloseOtherTabsText = I18n.T("$masaBlazor.pageTabs.closeOtherTabs");

        await base.SetParametersAsync(parameters);
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        var absolutePath = NavigationManager.GetAbsolutePath();
        if (!_absolutePaths.Contains(absolutePath))
        {
            _absolutePaths.Add(absolutePath);
        }

        NavigationManager.LocationChanged += NavigationManagerOnLocationChanged;
    }

    private void NavigationManagerOnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        var absolutePath = NavigationManager.GetAbsolutePath();

        FormatSelfPaths();
        if (SelfPaths!.Any(p => absolutePath.StartsWith(p)))
        {
            // TODO: 如果是Self的就永远只有一个tab
        }

        if (_absolutePaths.Contains(absolutePath))
        {
            return;
        }

        InvokeAsync(() =>
        {
            _absolutePaths.Add(absolutePath);
            StateHasChanged();
        });
    }

    private async Task HandleOnCloseTab(string path)
    {
        var isConfirmed = await PopupService.ConfirmAsync("Close tab", "Are you sure?");
        if (!isConfirmed) return;

        var currentPath = NavigationManager.GetAbsolutePath();

        if (currentPath == path)
        {
            var index = _absolutePaths.IndexOf(path);

            var nextPath = index == _absolutePaths.Count - 1
                ? _absolutePaths.ElementAtOrDefault(_absolutePaths.Count - 2)
                : _absolutePaths.ElementAtOrDefault(index + 1);

            nextPath ??= "/";

            NavigationManager.NavigateTo(nextPath);
        }

        _absolutePaths.Remove(path);
        TabClosed?.Invoke(this, path);

        NextTick(() => { _tabs?.CallSlider(); });
    }

    private void ReloadTab()
    {
        if (_contextmenuPath is null) return;
        TabReload?.Invoke(this, _contextmenuPath);
    }

    private void CloseTabsToTheLeft()
    {
        if (_contextmenuPath is null) return;

        var currentPath = NavigationManager.GetAbsolutePath();

        var index = _absolutePaths.FindIndex(p => p == _contextmenuPath);
        _absolutePaths.RemoveRange(0, index);

        if (!_absolutePaths.Contains(currentPath))
        {
            NavigationManager.NavigateTo(_contextmenuPath);
        }

        TabsUpdated?.Invoke(this, _absolutePaths.ToArray());

        NextTick(() => { _tabs?.CallSlider(); });
    }

    private void CloseTabsToTheRight()
    {
        if (_contextmenuPath is null) return;

        var currentPath = NavigationManager.GetAbsolutePath();

        var index = _absolutePaths.FindIndex(p => p == _contextmenuPath);
        var startIndex = index + 1;
        _absolutePaths.RemoveRange(startIndex, _absolutePaths.Count - startIndex);

        if (!_absolutePaths.Contains(currentPath))
        {
            NavigationManager.NavigateTo(_contextmenuPath);
        }

        TabsUpdated?.Invoke(this, _absolutePaths.ToArray());

        NextTick(() => { _tabs?.CallSlider(); });
    }

    private void CloseOtherTabs()
    {
        if (_contextmenuPath is null) return;

        _absolutePaths.RemoveAll(p => p != _contextmenuPath);

        NavigationManager.NavigateTo(_contextmenuPath);

        TabsUpdated?.Invoke(this, _absolutePaths.ToArray());
    }

    private async Task HandleOnContextmenu(MouseEventArgs args, string path)
    {
        _contextmenuPath = path;
        _menuValue = false;
        await Task.Delay(16);

        _menuValue = true;
        _positionX = args.ClientX;
        _positionY = args.ClientY;
    }

    private void FormatSelfPaths()
    {
        SelfPaths = SelfPaths is null
            ? new List<string>()
            : SelfPaths.Select(sp => "/" + sp.Trim('/') + "/").ToList();
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        NavigationManager.LocationChanged -= NavigationManagerOnLocationChanged;
    }
}
