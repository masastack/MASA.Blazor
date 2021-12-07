using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;

namespace MASA.Blazor.Doc.Shared;

public partial class DocLayout
{
    [Inject]
    private IJSRuntime Js { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await ScrollToFragment();

        NavigationManager.LocationChanged += OnLocationChanged;
    }

    private async void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        await ScrollToFragment();
    }

    private async Task ScrollToFragment()
    {
        var uri = new Uri(NavigationManager.Uri);

        var fragment = uri.Fragment;

        if (fragment.StartsWith('#'))
        {
            await Js.ScrollToHash(fragment);
        }
    }
    
    public void Dispose()
    {
        NavigationManager.LocationChanged -= OnLocationChanged;
    }
}