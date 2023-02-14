using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;

namespace Masa.Blazor.Doc.Shared;

public partial class DocLayout
{
    [Inject]
    private IJSRuntime Js { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    protected override void OnInitialized()
    {
        NavigationManager.LocationChanged += OnLocationChanged;
    }

    private async void OnLocationChanged(object sender, LocationChangedEventArgs e)
    {
        await ScrollToFragment();
    }

    private async Task ScrollToFragment()
    {
        var uri = new Uri(NavigationManager.Uri);

        var fragment = uri.Fragment;

        if (fragment.StartsWith('#') && fragment != "#")
        {
            await Js.ScrollToHash(fragment);
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await ScrollToFragment();
    }

    public void Dispose()
    {
        NavigationManager.LocationChanged -= OnLocationChanged;
    }
}