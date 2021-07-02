using BlazorComponent;
using BlazorComponent.Doc.CLI.Models;
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

namespace MASA.Blazor.Doc.Shared
{
    public partial class Contents
    {
        public List<ContentsItem> Items { get; set; }

        protected ContentsItem ActiveItem { get; set; }

        [Inject]
        public DomEventJsInterop DomEventJsInterop { get; set; }

        [Inject]
        public IJSRuntime Js { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private Timer _timer;

        protected override void OnInitialized()
        {
            NavigationManager.LocationChanged += OnLocationChanged;
            if (_timer == null)
            {
                _timer = new Timer();
                _timer.Interval = 300;
                _timer.Elapsed += _timer_Elapsed;
            }
        }

        private async void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            ActiveItem = null;
            Items = await Service.GetTitlesAsync(NavigationManager.Uri);
            StateHasChanged();
        }

        private async void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();

            var window = await Js.InvokeAsync<Window>(JsInteropConstants.GetWindow);
            var doms = await Js.InvokeAsync<List<Dom>>(JsInteropConstants.GetBoundingClientRects, ".markdown-body> section>section");

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
                    if (dom.Rect.Top - 48 > 0)
                    {
                        id = dom.Id;
                        break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(id))
            {
                ActiveItem = Items?.Find(r => r.Href.Contains(id));
                await InvokeAsync(StateHasChanged);
            }
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
            if (!_timer.Enabled)
            {
                _timer.Start();
            }
            else
            {
                _timer.Stop();
                _timer.Start();
            }
        }

        public async void HandleOnClick(string href)
        {
            var hash = href.Split("#")[1];
            var id = "#" + hash;
            ActiveItem = Items?.Find(r => r.Href.Contains(hash));

            var element = await Js.InvokeAsync<HtmlElement>(JsInteropConstants.GetDomInfo, id);
            var options = new
            {
                Top = element.OffsetTop,
                Left = 0,
                Behavior = "smooth"
            };
            await Js.InvokeVoidAsync("window.scrollTo", options);
        }
    }
}
