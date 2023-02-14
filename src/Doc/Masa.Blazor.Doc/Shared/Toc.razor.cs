﻿using BlazorComponent;
using BlazorComponent.I18n;
using BlazorComponent.Web;
using Masa.Blazor.Doc.Components;
using Masa.Blazor.Doc.Models;
using Masa.Blazor.Doc.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using System.Globalization;
using System.Text.Json;

namespace Masa.Blazor.Doc.Shared;

public partial class Toc : OwningComponentBase<DemoService>
{
    [Inject]
    public I18n I18n { get; set; }

    [Inject]
    public IJSRuntime Js { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [CascadingParameter(Name = "Culture")]
    public CultureInfo Culture { get; set; }

    private readonly string _windowScrollEventListenerKey = "toc-window-scroll";

    private bool _disposed;

    private ContentsItem ActiveItem { get; set; }
    private List<ContentsItem> Items { get; set; } = new();

    protected override void OnInitialized()
    {
        NavigationManager.LocationChanged += OnLocationChanged;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await Js.InvokeVoidAsync(JsInteropConstants.AddHtmlElementEventListener,
                "window",
                "scroll",
                DotNetObjectReference.Create(new Invoker<object>(_ => OnScroll())),
                false,
                new EventListenerExtras(_windowScrollEventListenerKey, 17)
            );
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        await UpdateItemsAsync();
    }

    private async void HandleOnClick(string id)
    {
        ActiveItem = Items?.Find(r => r.Id.Contains(id));

        var domId = "#" + id;
        await Js.ScrollToHash(domId);
    }

    private void OnLocationChanged(object sender, LocationChangedEventArgs e)
    {
        _ = InvokeAsync(async () =>
        {
            await UpdateItemsAsync();
            StateHasChanged();
        });
    }

    private async Task OnScroll()
    {
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
                if (dom.Rect.Top - 80 >= 0)
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
    }

    private async Task UpdateItemsAsync()
    {
        if (_disposed)
        {
            return;
        }

        ActiveItem = null;
        Service.ChangeLanguage(Culture);
        Items = await Service.GetTitlesAsync(NavigationManager.Uri);
    }

    private string T(string key)
    {
        return I18n.Locale.GetValueOrDefault(key);
    }

    protected override void Dispose(bool disposing)
    {
        _disposed = true;
        NavigationManager.LocationChanged -= OnLocationChanged;
    }
}
