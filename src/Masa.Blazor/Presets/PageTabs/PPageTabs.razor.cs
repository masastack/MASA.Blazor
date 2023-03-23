﻿#nullable enable
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
    public RenderFragment<PageTabPathValue>? TabContent { get; set; }

    [Parameter]
    public string? TabClass { get; set; }

    [Parameter]
    [ApiDefaultValue(true)]
    public bool Ripple { get; set; } = true;

    [Parameter]
    public string? ActiveClass { get; set; }

    [Parameter]
    public bool HideSlider { get; set; }

    [Parameter]
    public string? SliderColor { get; set; }

    [Parameter]
    public string? ReloadTabText { get; set; }

    [Parameter]
    public string? CloseTabsToTheLeftText { get; set; }

    [Parameter]
    public string? CloseTabsToTheRightText { get; set; }

    [Parameter]
    public string? CloseOtherTabsText { get; set; }

    [Parameter]
    public string? NoDataPath { get; set; } = "/";

    private bool _menuValue;
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
                   .Apply("tab", css => css.Add("p-page-tab"));
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
        var pathPattern = GetCurrentPatternPath();
        if (!pathPattern.IsSelf && PatternPaths.Any(p => p.AbsolutePath.Equals(pathPattern.AbsolutePath, StringComparison.OrdinalIgnoreCase)))
        {
            return;
        }

        if (pathPattern.IsSelf)
        {
            var renderedPathPattern = PatternPaths.FirstOrDefault(p => pathPattern == p);
            if (renderedPathPattern is not null)
            {
                renderedPathPattern.UpdatePath(NavigationManager.GetAbsolutePath());
                InvokeAsync(StateHasChanged);
                return;
            }
        }

        InvokeAsync(() =>
        {
            PatternPaths.Add(pathPattern);
            StateHasChanged();
        });
    }

    private string DefaultTabTitle(string absolutePath)
    {
        if (absolutePath == "/")
        {
            return absolutePath;
        }

        return absolutePath.TrimEnd('/').Split('/').Last();
    }

    private async Task HandleOnCloseTab(PatternPath patternPath)
    {
        var isConfirmed = await PopupService.ConfirmAsync(I18n.T("$masaBlazor.pageTabs.closeTab"),
            I18n.T("$masaBlazor.pageTabs.closeTabConfirm", args: patternPath.AbsolutePath), AlertTypes.Warning);
        if (!isConfirmed) return;

        var currentPathPattern = GetCurrentPatternPath();

        if (currentPathPattern == patternPath)
        {
            var index = PatternPaths.IndexOf(patternPath);

            var nextPath = index == PatternPaths.Count - 1
                ? PatternPaths.ElementAtOrDefault(PatternPaths.Count - 2)?.AbsolutePath
                : PatternPaths.ElementAtOrDefault(index + 1)?.AbsolutePath;

            nextPath ??= NoDataPath;

            NavigationManager.NavigateTo(nextPath);
        }

        PatternPaths.Remove(patternPath);
        TabClosed?.Invoke(this, patternPath);
        PageTabsProvider?.RemovePathTitles(patternPath.AbsolutePath);

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

    private void CloseTabsToTheRight()
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

    private void CloseOtherTabs()
    {
        if (_contextmenuPath is null) return;

        var closingAbsolutePath = PatternPaths.Where(p => p != _contextmenuPath).Select(p => p.AbsolutePath).ToArray();
        PageTabsProvider?.RemovePathTitles(closingAbsolutePath);

        PatternPaths.RemoveAll(p => p != _contextmenuPath);

        NavigationManager.NavigateTo(_contextmenuPath!.AbsolutePath);

        TabsUpdated?.Invoke(this, PatternPaths.ToArray());
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
