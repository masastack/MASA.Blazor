using BlazorComponent;
using BlazorComponent.Doc.Models;
using MASA.Blazor.Doc.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;
using Element = BlazorComponent.Web.Element;

namespace MASA.Blazor.Doc.Shared
{
    public partial class Contents
    {
        private List<ContentsItem> _items { get; set; } = new();
        private CancellationTokenSource _cancellationTokenSource;
        private bool _disposed;

        protected ContentsItem ActiveItem { get; set; }

        [Inject]
        public DomEventJsInterop DomEventJsInterop { get; set; }

        [Inject]
        public IJSRuntime Js { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

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

        private void OnScroll(JsonElement obj)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            _ = Task.Run(async () =>
            {
                await Task.Delay(300, _cancellationTokenSource.Token);

                var window = await Js.InvokeAsync<Window>(JsInteropConstants.GetWindow);
                var doms = await Js.InvokeAsync<List<Dom>>(JsInteropConstants.GetBoundingClientRects, ".markdown-body> section section");

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
                        if (dom.Rect.Top - 64 > 0)
                        {
                            id = dom.Id;
                            break;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(id))
                {
                    ActiveItem = _items?.Find(r => r.Id.Contains(id));
                    await InvokeAsync(StateHasChanged);
                }
            });
        }

        private async void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            if (_disposed)
            {
                return;
            }

            ActiveItem = null;
            _items = await Service.GetTitlesAsync(NavigationManager.Uri);

            StateHasChanged();
        }

        private async void HandleOnClick(string id)
        {
            var domId = "#" + id;
            ActiveItem = _items?.Find(r => r.Id.Contains(id));

            var element = await Js.InvokeAsync<Element>(JsInteropConstants.GetDomInfo, domId);
            var options = new
            {
                Top = element.OffsetTop,
                Left = 0,
                Behavior = "smooth"
            };
            await Js.InvokeVoidAsync("window.scrollTo", options);
        }

        protected override void Dispose(bool disposing)
        {
            _disposed = true;
            NavigationManager.LocationChanged -= OnLocationChanged;
        }
    }
}