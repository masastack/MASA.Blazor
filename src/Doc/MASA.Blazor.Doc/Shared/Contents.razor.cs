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
        private List<ContentsItem> Items { get; set; } = new();

        public ContentsItem ActiveItem { get; set; }

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
        }

        private async void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            ActiveItem = null;

            Items = await Service.GetTitlesAsync(NavigationManager.Uri);

            StateHasChanged();
        }

        private async void HandleOnClick(string href)
        {
            var hash = href.Split("#")[1];
            var id = "#" + hash;
            ActiveItem = Items?.Find(r => r.Href.Contains(hash));

            var element = await Js.InvokeAsync<Element>(JsInteropConstants.GetDomInfo, id);
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