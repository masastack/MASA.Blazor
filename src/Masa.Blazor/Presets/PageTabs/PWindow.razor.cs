#nullable enable
using Microsoft.AspNetCore.Components.Routing;

namespace Masa.Blazor.Presets;

public partial class PWindow : PatternPathComponentBase, IAsyncDisposable
{
    [CascadingParameter]
    private PPageTabs? ContainerPageTabs { get; set; }

    [Parameter]
    public PPageTabs? PageTabs { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private PatternPath? _currentAbsolutePath;

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        var patternPath = GetCurrentPatternPath();
        _currentAbsolutePath = patternPath;
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        var patternPath = GetCurrentPatternPath();
        if (!PatternPaths.Contains(patternPath))
        {
            PatternPaths.Add(patternPath);
        }

        if (ContainerPageTabs != null)
        {
            ContainerPageTabs.TabClosed += PageTabsOnTabClosed;
            ContainerPageTabs.TabReload += PageTabsOnTabReload;
            ContainerPageTabs.TabsUpdated += PageTabsOnTabsUpdated;
        }

        NavigationManager.LocationChanged += NavigationManagerOnLocationChanged;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            if (PageTabs != null)
            {
                PageTabs.TabClosed += PageTabsOnTabClosed;
                PageTabs.TabReload += PageTabsOnTabReload;
                PageTabs.TabsUpdated += PageTabsOnTabsUpdated;
            }
        }
    }

    private void PageTabsOnTabClosed(object? sender, PatternPath e)
    {
        InvokeAsync(() =>
        {
            PatternPaths.Remove(e);
            StateHasChanged();
        });
    }

    private void PageTabsOnTabReload(object? sender, PatternPath e)
    {
        InvokeAsync(() =>
        {
            PatternPaths.Remove(e);
            StateHasChanged();
            PatternPaths.Add(e);
            StateHasChanged();
        });
    }

    private void PageTabsOnTabsUpdated(object? sender, PatternPath[] paths)
    {
        InvokeAsync(() =>
        {
            PatternPaths.RemoveAll(p => !paths.Contains(p));
            StateHasChanged();
        });
    }

    private void NavigationManagerOnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        if (PatternPaths.Contains(_currentAbsolutePath!))
        {
            return;
        }

        var patternPath = GetCurrentPatternPath();
        if (patternPath.IsSelf)
        {
            var renderedAbsolutePath = PatternPaths.FirstOrDefault(p => patternPath == p);
            if (renderedAbsolutePath is not null)
            {
                InvokeAsync(StateHasChanged);
                return;
            }
            else
            {
                PatternPaths.Add(patternPath);
                InvokeAsync(StateHasChanged);
                return;
            }
        }

        InvokeAsync(() =>
        {
            PatternPaths.Add(_currentAbsolutePath!);
            StateHasChanged();
        });
    }

    public async ValueTask DisposeAsync()
    {
        if (PageTabs != null)
        {
            PageTabs.TabClosed -= PageTabsOnTabClosed;
            PageTabs.TabReload -= PageTabsOnTabReload;
            PageTabs.TabsUpdated -= PageTabsOnTabsUpdated;
        }

        if (ContainerPageTabs != null)
        {
            ContainerPageTabs.TabClosed -= PageTabsOnTabClosed;
            ContainerPageTabs.TabReload -= PageTabsOnTabReload;
            ContainerPageTabs.TabsUpdated -= PageTabsOnTabsUpdated;
        }

        NavigationManager.LocationChanged -= NavigationManagerOnLocationChanged;
    }
}
