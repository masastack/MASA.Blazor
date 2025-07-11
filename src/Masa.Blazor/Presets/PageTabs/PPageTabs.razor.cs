﻿using Microsoft.AspNetCore.Components.Routing;

namespace Masa.Blazor.Presets;

public partial class PPageTabs
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
    [MasaApiParameter(true)]
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
    [MasaApiParameter(2)]
    public StringNumber? SliderSize { get; set; } = 2;

    [Parameter]
    public bool Dark { get; set; }

    #endregion

    [Parameter]
    [MasaApiParameter("$masaBlazor.pageTabs.closeTab")]
    public string? CloseTabText { get; set; }

    [Parameter]
    [MasaApiParameter("$masaBlazor.pageTabs.reloadTab")]
    public string? ReloadTabText { get; set; }

    [Parameter]
    [MasaApiParameter("$masaBlazor.pageTabs.pinTab", "v1.10.0")]
    public string? PinTabText { get; set; }

    [Parameter]
    [MasaApiParameter("$masaBlazor.pageTabs.unpinTab", "v1.10.0")]
    public string? UnpinTabText { get; set; }

    [Parameter]
    [MasaApiParameter("$masaBlazor.pageTabs.closeTabsToTheLeft")]
    public string? CloseTabsToTheLeftText { get; set; }

    [Parameter]
    [MasaApiParameter("$masaBlazor.pageTabs.closeTabsToTheRight")]
    public string? CloseTabsToTheRightText { get; set; }

    [Parameter]
    [MasaApiParameter("$masaBlazor.pageTabs.closeOtherTabs")]
    public string? CloseOtherTabsText { get; set; }

    [Parameter]
    [MasaApiParameter("/")]
    public string NoDataPath
    {
        get => _noDataPath ?? "/";
        set => _noDataPath = value;
    }

    [Parameter]
    [MasaApiParameter("$close")]
    public string? CloseIcon { get; set; } = "$close";

    [Parameter]
    [MasaApiParameter(true)]
    public bool AskBeforeClosing { get; set; } = true;

    [Parameter]
    public Func<string, Task<bool>>? OnClose { get; set; }

    [Parameter]
    [MasaApiParameter(ReleasedIn = "v1.10.0")]
    public IEnumerable<string>? FixedTabs { get; set; } = [];

    private static readonly Block _tabBlock = new("p-page-tab");

    private readonly List<PatternPath> PatternPaths = new();

    private bool _menuValue;
    private string? _noDataPath;
    private string? _previousPath;
    private PatternPath? _contextmenuPath;
    private CancellationTokenSource? _middleClickCancellationTokenSource;

    private double _positionX;
    private double _positionY;

    private MTabs? _tabs;

    private PatternPath? _currentPatternPath;
    internal EventHandler<PatternPath>? TabClosed;
    internal EventHandler<PatternPath>? TabReload;
    internal EventHandler<PatternPath[]>? TabsUpdated;

    private bool ContextmenuFixed => _contextmenuPath?.Options.Fixed is true;

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        PinTabText = I18n.T("$masaBlazor.pageTabs.pinTab");
        UnpinTabText = I18n.T("$masaBlazor.pageTabs.unpinTab");
        CloseTabText = I18n.T("$masaBlazor.pageTabs.closeTab");
        ReloadTabText = I18n.T("$masaBlazor.pageTabs.reloadTab");
        CloseTabsToTheLeftText = I18n.T("$masaBlazor.pageTabs.closeTabsToTheLeft");
        CloseTabsToTheRightText = I18n.T("$masaBlazor.pageTabs.closeTabsToTheRight");
        CloseOtherTabsText = I18n.T("$masaBlazor.pageTabs.closeOtherTabs");

        await base.SetParametersAsync(parameters);
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (FixedTabs != null)
        {
            foreach (var fixedTab in FixedTabs)
            {
                var selfPatternRegex = CachedSelfPatternRegexes.FirstOrDefault(r => r.IsMatch(fixedTab));
                PatternPaths.Add(selfPatternRegex is null
                    ? new PatternPath(fixedTab, isFixed: true)
                    : new PatternPath(selfPatternRegex.ToString(), fixedTab, isFixed: true));
            }
        }

        _currentPatternPath = GetCurrentPatternPath();
        if (!PatternPaths.Contains(_currentPatternPath))
        {
            PatternPaths.Add(_currentPatternPath);
        }

        if (PageTabsProvider != null)
        {
            PageTabsProvider.TabTitleChanged += PageTabsProviderOnTabTitleChanged;
        }

        NavigationManager.LocationChanged += NavigationManagerOnLocationChanged;
    }

    internal override PatternPath GetCurrentPatternPath()
    {
        var pathPattern = base.GetCurrentPatternPath();
        var tabOptions = TabOptions?.Invoke(new PageTabPathValue(pathPattern.AbsolutePath, false));
        pathPattern.Options = tabOptions ?? new();
        return pathPattern;
    }

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return "p-page-tabs";
    }

    [MasaApiPublicMethod]
    public void CloseAllTabs(bool disableAutoNavigation = false)
    {
        var toClosePatternPaths = PatternPaths.Where(p => p.IsCloseable);
        PatternPaths.RemoveAll(p => toClosePatternPaths.Contains(p));
        TabsUpdated?.Invoke(this, PatternPaths.ToArray());
        var closingAbsolutePaths = toClosePatternPaths.Select(p => p.AbsolutePath).ToArray();
        PageTabsProvider?.RemovePathTitles(closingAbsolutePaths);

        if (!disableAutoNavigation)
        {
            if (PatternPaths.Count > 0)
                NavigationManager.NavigateTo(PatternPaths[0].AbsolutePath);
            else
                NavigationManager.NavigateTo(NoDataPath);
        }
    }

    [MasaApiPublicMethod]
    public void CloseCurrentTab(bool disableAutoNavigation = false)
    {
        var current = GetCurrentPatternPath();

        if (!current.IsCloseable)
        {
            return;
        }

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
                // simple implementation: try to navigate to the first tab
                var nextPath = PatternPaths.FirstOrDefault()?.AbsolutePath;
                nextPath ??= NoDataPath;

                NavigationManager.NavigateTo(nextPath);
            }
        }

        TabsUpdated?.Invoke(this, PatternPaths.ToArray());
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
            _tabs?.CallSliderAfterRender();

            StateHasChanged();
        });
    }

    private void NavigationManagerOnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        _currentPatternPath = GetCurrentPatternPath();
        var currentPath = _currentPatternPath.AbsolutePath;

        // only the path is changed, not the query string
        if (_previousPath == currentPath)
        {
            return;
        }

        if (_currentPatternPath.IsSelf)
        {
            var renderedPathPattern = PatternPaths.FirstOrDefault(p => _currentPatternPath == p);
            if (renderedPathPattern is not null)
            {
                renderedPathPattern.UpdatePath(NavigationManager.GetAbsolutePath());
                InvokeAsync(StateHasChanged);
            }
        }

        if (!PatternPaths.Contains(_currentPatternPath))
        {
            PatternPaths.Add(_currentPatternPath);
        }

        _previousPath = currentPath;

        InvokeAsync(StateHasChanged);
    }

    private string GetTabTitle(PatternPath patternPath)
    {
        if (PageTabsProvider != null && PageTabsProvider.PathTitles.TryGetValue(patternPath.AbsolutePath, out var titleBuilder) &&
            titleBuilder.Invoke() is { } title && !string.IsNullOrWhiteSpace(title))
        {
            return title;
        }

        return patternPath.Options?.Title ?? DefaultTabTitle(patternPath.AbsolutePath);
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
        if (!patternPath.IsCloseable)
        {
            return;
        }

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

    private void ToggleFixed()
    {
        if (_contextmenuPath is null)
        {
            return;
        }

        var currentPatternPath = PatternPaths.FirstOrDefault(u => u == _contextmenuPath);
        if (currentPatternPath is null)
        {
            return;
        }

        var toggleToFixed = !currentPatternPath.Options.Fixed;
        var lastIndex = PatternPaths.FindLastIndex(u => u.Options.Fixed);
        var index = PatternPaths.IndexOf(currentPatternPath);

        if (lastIndex == index)
        {
            currentPatternPath.Options.Fixed = toggleToFixed;
        }
        else
        {
            PatternPaths.Remove(currentPatternPath);
            currentPatternPath.Options.Fixed = toggleToFixed;

            int insertIndex;
            if (currentPatternPath.Options.Fixed)
            {
                insertIndex = lastIndex + 1;
                if (insertIndex >= PatternPaths.Count)
                {
                    insertIndex = PatternPaths.Count;
                }
            }
            else
            {
                insertIndex = lastIndex;
            }

            PatternPaths.Insert(insertIndex, currentPatternPath);
        }

        _tabs?.CallSliderAfterRender();
    }

    private void HandleOnReloadTab()
    {
        if (_contextmenuPath is null) return;
        TabReload?.Invoke(this, _contextmenuPath);
    }

    private async Task HandleOnCloseTabOnMenu()
    {
        if (_contextmenuPath is null) return;
        await HandleOnCloseTab(_contextmenuPath, GetTabTitle(_contextmenuPath));
    }

    private void HandleOnCloseTabsToTheLeft()
    {
        if (_contextmenuPath is null) return;

        var currentPath = GetCurrentPatternPath();

        var index = PatternPaths.FindIndex(p => p == _contextmenuPath);

        var toClosePatternPaths = PatternPaths.Take(index).Where(p => p.IsCloseable).ToList();
        var closingAbsolutePaths = toClosePatternPaths.Select(p => p.AbsolutePath).ToArray();
        PageTabsProvider?.RemovePathTitles(closingAbsolutePaths);

        PatternPaths.RemoveAll(p => toClosePatternPaths.Contains(p));

        if (!PatternPaths.Contains(currentPath))
        {
            NavigationManager.NavigateTo(_contextmenuPath!.AbsolutePath);
        }

        TabsUpdated?.Invoke(this, PatternPaths.ToArray());
    }

    private void HandleOnCloseTabsToTheRight()
    {
        if (_contextmenuPath is null) return;

        var currentPath = GetCurrentPatternPath();

        var index = PatternPaths.FindIndex(p => p == _contextmenuPath);
        var startIndex = index + 1;
        var count = PatternPaths.Count - startIndex;

        var toClosePatternPaths = PatternPaths.Skip(startIndex).Take(count).Where(p => p.IsCloseable).ToList();
        var closingAbsolutePaths = toClosePatternPaths.Select(p => p.AbsolutePath).ToArray();
        PageTabsProvider?.RemovePathTitles(closingAbsolutePaths);

        PatternPaths.RemoveAll(p => toClosePatternPaths.Contains(p));

        if (!PatternPaths.Contains(currentPath))
        {
            NavigationManager.NavigateTo(_contextmenuPath!.AbsolutePath);
        }

        TabsUpdated?.Invoke(this, PatternPaths.ToArray());
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

    private async Task HandleOnAuxclick(MouseEventArgs args, PatternPath patternPath, string tabTitle)
    {
        if (args.Button == 1)
        {
            // HACK: I don't know why this event would be triggered twice,
            // so I use a cancellation token to cancel the previous task

            _middleClickCancellationTokenSource?.Cancel();
            _middleClickCancellationTokenSource = new CancellationTokenSource();

            await RunTaskInMicrosecondsAsync(() => HandleOnCloseTab(patternPath, tabTitle), 100, _middleClickCancellationTokenSource.Token);
        }
    }

    private void CloseTab(PatternPath patternPath)
    {
        PatternPaths.Remove(patternPath);
        TabClosed?.Invoke(this, patternPath);
        PageTabsProvider?.RemovePathTitles(patternPath.AbsolutePath);
    }

    private void CloseOtherTabs(PatternPath current)
    {
        var toClosePatternPaths = PatternPaths.Where(p => p != current && p.IsCloseable).ToList();
        var closingAbsolutePath = toClosePatternPaths.Select(p => p.AbsolutePath).ToArray();
        PageTabsProvider?.RemovePathTitles(closingAbsolutePath);

        PatternPaths.RemoveAll(p => toClosePatternPaths.Contains(p));

        NavigationManager.NavigateTo(current.AbsolutePath);

        TabsUpdated?.Invoke(this, PatternPaths.ToArray());
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

    protected override ValueTask DisposeAsyncCore()
    {
        if (PageTabsProvider != null)
        {
            PageTabsProvider.TabTitleChanged -= PageTabsProviderOnTabTitleChanged;
        }

        NavigationManager.LocationChanged -= NavigationManagerOnLocationChanged;

        return base.DisposeAsyncCore();
    }
}