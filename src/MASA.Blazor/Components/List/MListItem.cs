using System;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;

namespace MASA.Blazor
{
    public partial class MListItem : BListItem
    {
        /// <summary>
        /// Lowers max height of list tiles
        /// </summary>
        [Parameter]
        public bool Dense { get; set; }

        /// <summary>
        /// Disables the component
        /// </summary>
        [Parameter]
        public bool Disabled { get; set; }

        /// <summary>
        /// If set, the list tile will not be rendered as a link even if it has to/href prop or @click handler
        /// </summary>
        [Parameter]
        public bool Inactive { get; set; }

        /// <summary>
        /// Allow text selection inside v-list-item. This prop uses user-select
        /// </summary>
        [Parameter]
        public bool Selectable { get; set; }

        /// <summary>
        /// Increases list-item height for two lines. This prop uses line-clamp and is not supported in all browsers.
        /// </summary>
        [Parameter]
        public bool TwoLine { get; set; }

        /// <summary>
        /// Increases list-item height for three lines. This prop uses line-clamp and is not supported in all browsers.
        /// </summary>
        [Parameter]
        public bool ThreeLine { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool IsActive { get; set; }

        [CascadingParameter]
        public BList List { get; set; }

        [CascadingParameter]
        public BNavigationDrawer NavigationDrawer { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Href) && MatchUrl(Href, e.Location) && List != null)
            {
                List.Select(this);
            }
        }

        private bool MatchUrl(string href, string location)
        {
            var url = NavigationManager.ToAbsoluteUri(href);
            var matched = string.Equals(url.ToString(), location, StringComparison.OrdinalIgnoreCase);

            return matched;
        }

        internal void Deactive()
        {
            IsActive = false;
            StateHasChanged();
        }

        internal void Active()
        {
            IsActive = true;
            StateHasChanged();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (List != null && List.Items.IndexOf(this) == -1)
            {
                List.Items.Add(this);
                NavigationManager.LocationChanged += OnLocationChanged;
            }
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (!string.IsNullOrEmpty(Href) && MatchUrl(Href, NavigationManager.Uri))
            {
                Active();
            }
        }

        protected override void SetComponentClass()
        {
            var prefix = "m-list-item";
            CssProvider
                .Apply<BListItem>(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-list-item")
                        .AddIf($"{prefix}--dense", () => Dense)
                        .AddIf($"{prefix}--disabled", () => Disabled)
                        .AddIf($"{prefix}--selectable", () => Selectable)
                        .AddIf($"{prefix}--two-line", () => TwoLine)
                        .AddIf($"{prefix}--three-line", () => ThreeLine)
                        .AddIf($"{prefix}--link", () => Link && !Inactive)
                        .AddIf($"{prefix}--active", () => Link && Group?.Value == Key || IsActive)
                        .AddTheme(Dark);
                });
        }

        protected override async Task HandleOnClick(MouseEventArgs args)
        {
            if (NavigationManager != null && !string.IsNullOrEmpty(Href))
            {
                NavigationManager.NavigateTo(Href);
            }

            if (Group != null)
            {
                if (Group.ValueChanged.HasDelegate)
                {
                    await Group.ValueChanged.InvokeAsync(Key);
                }
                else
                {
                    await Group.ChangeValue(Key);
                }
            }

            await base.HandleOnClick(args);
        }

        protected override void Dispose(bool disposing)
        {
            List?.Items?.Remove(this);
            NavigationManager.LocationChanged -= OnLocationChanged;

            base.Dispose(disposing);
        }
    }
}
