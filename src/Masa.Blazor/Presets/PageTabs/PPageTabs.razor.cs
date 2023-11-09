using System.Collections.Immutable;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;

namespace Masa.Blazor.Presets;

public partial class PPageTabs : PatternPathComponentBase
{
    [Inject]
    private I18n I18n { get; set; } = null!;

    [Inject]
    private IPopupService PopupService { get; set; } = null!;

    [CascadingParameter]
    private PPageTabsProvider? PageTabsProvider { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public string? TabClass { get; set; }

    [Parameter]
    public RenderFragment<PageTabPathValue>? TabIconContent { get; set; }

    [Parameter]
    public Func<PageTabPathValue, TabOptions?>? TabOptions { get; set; }

    #region Parameters from MTabs

    [Parameter]
    [MassApiParameter(true)]
    public bool Ripple { get; set; } = true;

    [Parameter]
    public string? ActiveClass { get; set; }

    [Parameter]
    public string? BackgroundColor { get; set; }

    [Parameter]
    public string? Color { get; set; }

    [Parameter]
    public StringNumber? Height { get; set; }

    [Parameter]
    public bool HideSlider { get; set; }

    [Parameter]
    public string? SliderColor { get; set; }

    [Parameter]
    [MassApiParameter(2)]
    public StringNumber? SliderSize { get; set; } = 2;

    [Parameter]
    public bool Dark { get; set; }

    #endregion

    [Parameter]
    public string? ReloadTabText { get; set; }

    [Parameter]
    public string? CloseTabsToTheLeftText { get; set; }

    [Parameter]
    public string? CloseTabsToTheRightText { get; set; }

    [Parameter]
    public string? CloseOtherTabsText { get; set; }

    [Parameter]
    [MassApiParameter("/")]
    public string NoDataPath
    {
        get => _noDataPath ?? "/";
        set => _noDataPath = value;
    }

    [Parameter]
    [MassApiParameter("$close")]
    public string? CloseIcon { get; set; } = "$close";

    [Parameter]
    [MassApiParameter(true)]
    public bool AskBeforeClosing { get; set; } = true;

    [Parameter]
    public Func<string, Task<bool>>? OnClose { get; set; }

    protected readonly List<PatternPath> PatternPaths = new();

    private bool _menuValue;
    private string? _noDataPath;
    private string? _previousPath;
    private PatternPath? _contextmenuPath;

    private double _positionX;
    private double _positionY;

    private MTabs? _tabs;

    internal EventHandler<PatternPath>? TabClosed;
    internal EventHandler<PatternPath>? TabReload;
    internal EventHandler<PatternPath[]>? TabsUpdated;

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

        var pathPattern = GetCurrentPatternPath();
        PatternPaths.Add(pathPattern);

        if (PageTabsProvider != null)
        {
            PageTabsProvider.TabTitleChanged += PageTabsProviderOnTabTitleChanged;
        }

        NavigationManager.LocationChanged += NavigationManagerOnLocationChanged;
    }

    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider.Apply(css => css.Add("p-page-tabs"))
                   .Apply("tab", css => css.Add("p-page-tab").Add(TabClass))
                   .Apply("tab-title", css => css.Add("p-page-tab__title"))
                   .Apply("tab-close", css => css.Add("p-page-tab__close"));
    }

    [MasaApiPublicMethod]
    public void CloseAllTabs(bool disableAutoNavigation = false)
    {
        PatternPaths.Clear();
        TabsUpdated?.Invoke(this, Array.Empty<PatternPath>());
        PageTabsProvider?.RemoveAllPathTitles();

        if (!disableAutoNavigation)
        {
            NavigationManager.NavigateTo(NoDataPath);
        }
    }

    [MasaApiPublicMethod]
    public void CloseCurrentTab(bool disableAutoNavigation = false)
    {
        var current = GetCurrentPatternPath();

        if (!disableAutoNavigation)
        {
            NavigateToNextIfCurrentClosing(current);
        }

        CloseTab(current);
    }

    [MasaApiPublicMethod]
    public void CloseTabs(Regex pattern, bool disableAutoNavigation = false)
    {
        var paths = PatternPaths.Where(p => pattern.IsMatch(p.AbsolutePath)).ToArray();

        foreach (var path in paths)
        {
            PageTabsProvider?.RemovePathTitles(path.AbsolutePath);
            PatternPaths.Remove(path);
        }

        if (!disableAutoNavigation)
        {
            var current = GetCurrentPatternPath();
            var exists = paths.Contains(current);
            if (exists)
            {
                // simple implementation: just try to navigate to the first tab
                var nextPath = PatternPaths.FirstOrDefault()?.AbsolutePath;
                nextPath ??= NoDataPath;

                NavigationManager.NavigateTo(nextPath);
            }
        }

        TabsUpdated?.Invoke(this, PatternPaths.ToArray());

        NextTick(() => { _tabs?.CallSlider(); });
    }

    [MasaApiPublicMethod]
    public void CloseOtherTabs()
    {
        var current = GetCurrentPatternPath();
        CloseOtherTabs(current);
    }

    private void PageTabsProviderOnTabTitleChanged(object? sender, string e)
    {
        InvokeAsync(() =>
        {
            NextTick(() => { _tabs?.CallSlider(); });

            StateHasChanged();
        });
    }

    private void NavigationManagerOnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        var currentPatternPath = GetCurrentPatternPath();
        var currentPath = currentPatternPath.AbsolutePath;

        // only the path is changed, not the query string
        if (_previousPath == currentPath)
        {
            return;
        }

        if (currentPatternPath.IsSelf)
        {
            var renderedPathPattern = PatternPaths.FirstOrDefault(p => currentPatternPath == p);
            if (renderedPathPattern is not null)
            {
                renderedPathPattern.UpdatePath(NavigationManager.GetAbsolutePath());
                InvokeAsync(StateHasChanged);
            }
        }

        if (!PatternPaths.Contains(currentPatternPath))
        {
            PatternPaths.Add(currentPatternPath);
        }

        _previousPath = currentPath;

        InvokeAsync(StateHasChanged);
    }

    private string GetTabTitle(PatternPath patternPath, TabOptions? tabOptions)
    {
        if (PageTabsProvider != null && PageTabsProvider.PathTitles.TryGetValue(patternPath.AbsolutePath, out var titleBuilder) &&
            titleBuilder.Invoke() is { } title && !string.IsNullOrWhiteSpace(title))
        {
            return title;
        }

        return tabOptions?.Title ?? DefaultTabTitle(patternPath.AbsolutePath);
    }

    private string DefaultTabTitle(string absolutePath)
    {
        if (absolutePath == "/")
        {
            return absolutePath;
        }

        return absolutePath.TrimEnd('/').Split('/').Last();
    }

    private async Task HandleOnCloseTab(PatternPath patternPath, string tabTitle)
    {
        if (AskBeforeClosing)
        {
            bool isConfirmed;
            if (OnClose != default)
            {
                isConfirmed = await OnClose.Invoke(tabTitle);
            }
            else
            {
                isConfirmed = await PopupService.ConfirmAsync(I18n.T("$masaBlazor.pageTabs.closeTab"),
                    I18n.T("$masaBlazor.pageTabs.closeTabConfirm", args: tabTitle), AlertTypes.Warning);
            }

            if (!isConfirmed) return;
        }

        var current = GetCurrentPatternPath();
        if (current == patternPath)
        {
            NavigateToNextIfCurrentClosing(current);
        }

        CloseTab(patternPath);
    }

    private void HandleOnReloadTab()
    {
        if (_contextmenuPath is null) return;
        TabReload?.Invoke(this, _contextmenuPath);
    }

    private void HandleOnCloseTabsToTheLeft()
    {
        if (_contextmenuPath is null) return;

        var currentPath = GetCurrentPatternPath();

        var index = PatternPaths.FindIndex(p => p == _contextmenuPath);

        var closingAbsolutePaths = PatternPaths.Take(index + 1).Select(p => p.AbsolutePath).ToArray();
        PageTabsProvider?.RemovePathTitles(closingAbsolutePaths);

        PatternPaths.RemoveRange(0, index);

        if (!PatternPaths.Contains(currentPath))
        {
            NavigationManager.NavigateTo(_contextmenuPath!.AbsolutePath);
        }

        TabsUpdated?.Invoke(this, PatternPaths.ToArray());

        NextTick(() => { _tabs?.CallSlider(); });
    }

    private void HandleOnCloseTabsToTheRight()
    {
        if (_contextmenuPath is null) return;

        var currentPath = GetCurrentPatternPath();

        var index = PatternPaths.FindIndex(p => p == _contextmenuPath);
        var startIndex = index + 1;
        var count = PatternPaths.Count - startIndex;

        var closingAbsolutePaths = PatternPaths.Skip(startIndex).Take(count).Select(p => p.AbsolutePath).ToArray();
        PageTabsProvider?.RemovePathTitles(closingAbsolutePaths);

        PatternPaths.RemoveRange(startIndex, count);

        if (!PatternPaths.Contains(currentPath))
        {
            NavigationManager.NavigateTo(_contextmenuPath!.AbsolutePath);
        }

        TabsUpdated?.Invoke(this, PatternPaths.ToArray());

        NextTick(() => { _tabs?.CallSlider(); });
    }

    private void HandleOnCloseOtherTabs()
    {
        if (_contextmenuPath is null) return;

        CloseOtherTabs(_contextmenuPath);
    }

    private async Task HandleOnContextmenu(MouseEventArgs args, PatternPath patternPath)
    {
        _contextmenuPath = patternPath;
        _menuValue = false;
        await Task.Delay(16);

        _menuValue = true;
        _positionX = args.ClientX;
        _positionY = args.ClientY;
    }

    private void CloseTab(PatternPath patternPath)
    {
        PatternPaths.Remove(patternPath);
        TabClosed?.Invoke(this, patternPath);
        PageTabsProvider?.RemovePathTitles(patternPath.AbsolutePath);

        NextTick(() => { _tabs?.CallSlider(); });
    }

    private void CloseOtherTabs(PatternPath current)
    {
        var closingAbsolutePath = PatternPaths.Where(p => p != current).Select(p => p.AbsolutePath).ToArray();
        PageTabsProvider?.RemovePathTitles(closingAbsolutePath);

        PatternPaths.RemoveAll(p => p != current);

        NavigationManager.NavigateTo(current.AbsolutePath);

        TabsUpdated?.Invoke(this, PatternPaths.ToArray());

        NextTick(() => { _tabs?.CallSlider(); });
    }

    private void NavigateToNextIfCurrentClosing(PatternPath current)
    {
        var index = PatternPaths.IndexOf(current);

        var nextPath = index == PatternPaths.Count - 1
            ? PatternPaths.ElementAtOrDefault(PatternPaths.Count - 2)?.AbsolutePath
            : PatternPaths.ElementAtOrDefault(index + 1)?.AbsolutePath;

        nextPath ??= NoDataPath;

        NavigationManager.NavigateTo(nextPath);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (PageTabsProvider != null)
        {
            PageTabsProvider.TabTitleChanged -= PageTabsProviderOnTabTitleChanged;
        }

        NavigationManager.LocationChanged -= NavigationManagerOnLocationChanged;
    }
}
