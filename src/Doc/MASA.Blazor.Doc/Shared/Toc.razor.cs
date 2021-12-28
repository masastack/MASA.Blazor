using System.Globalization;
using System.Text.Json;
using BlazorComponent;
using BlazorComponent.Web;
using MASA.Blazor.Doc.Components;
using MASA.Blazor.Doc.Models;
using MASA.Blazor.Doc.Services;
using MASA.Blazor.Doc.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using Element = BlazorComponent.Web.Element;

namespace MASA.Blazor.Doc.Shared;

public partial class Toc : OwningComponentBase<DemoService>
{
    private bool _disposed;
    private CancellationTokenSource _cancellationTokenSource;

    private ContentsItem ActiveItem { get; set; }

    private List<ContentsItem> Items { get; set; } = new();

    [Inject]
    public DomEventJsInterop DomEventJsInterop { get; set; }

    [Inject]
    public GlobalConfigs GlobalConfig { get; set; }

    [Inject]
    public IJSRuntime Js { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [CascadingParameter]
    public bool IsChinese { get; set; }

    protected override void OnInitialized()
    {
        NavigationManager.LocationChanged += OnLocationChanged;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            DomEventJsInterop.AddEventListener("window", "scroll", OnScroll, false);
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        Service.ChangeLanguage(GlobalConfig.Language ?? CultureInfo.CurrentCulture.Name);
        Items = await Service.GetTitlesAsync(NavigationManager.Uri);
    }

    private async void HandleOnClick(string id)
    {
        ActiveItem = Items?.Find(r => r.Id.Contains(id));

        var domId = "#" + id;
        await Js.ScrollToHash(domId);
    }

    private void OnLocationChanged(object sender, LocationChangedEventArgs e)
    {
        _ = UpdateItemsAsync();
    }

    private void OnScroll(JsonElement obj)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();

        _ = Task.Run(async () =>
        {
            await Task.Delay(300, _cancellationTokenSource.Token);

            var window = await Js.InvokeAsync<Window>(JsInteropConstants.GetWindow);
            
            // API page
            var doms = await Js.InvokeAsync<List<Dom>>(JsInteropConstants.GetBoundingClientRects, ".markdown-body > div section section");
            
            // Other pages
            if (!doms.Any())
            {
                doms = await Js.InvokeAsync<List<Dom>>(JsInteropConstants.GetBoundingClientRects, ".markdown-body > section section");
            }

            var id = "";
            if (window.IsTop || window.IsBottom)
            {
                if (doms.Count > 0)
                {
                    id = window.IsTop ? doms[0].Id : doms[^1].Id;
                }
            }
            else
            {
                foreach (var dom in doms)
                {
                    if (dom.Rect.Top - 80 > 0)
                    {
                        id = dom.Id;
                        break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(id))
            {
                ActiveItem = Items?.Find(r => r.Id.Contains(id));
                await InvokeAsync(StateHasChanged);
            }
        });
    }

    private async Task UpdateItemsAsync()
    {
        if (_disposed)
        {
            return;
        }

        ActiveItem = null;
        Service.ChangeLanguage(GlobalConfig.Language ?? CultureInfo.CurrentCulture.Name);
        Items = await Service.GetTitlesAsync(NavigationManager.Uri);

        await InvokeAsync(StateHasChanged);
    }

    protected override void Dispose(bool disposing)
    {
        _disposed = true;
        NavigationManager.LocationChanged -= OnLocationChanged;
    }
}