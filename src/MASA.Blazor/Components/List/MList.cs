using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;
using OneOf;

namespace MASA.Blazor
{
    public partial class MList : BList, IThemeable
    {
        /// <summary>
        /// Removes elevation (box-shadow) and adds a thin border.
        /// </summary>
        [Parameter]
        public bool Outlined { get; set; }

        /// <summary>
        /// Provides an alternative active style for v-list-item
        /// </summary>
        [Parameter]
        public bool Shaped { get; set; }

        [Parameter]
        public StringNumber Elevation { get; set; }

        /// <summary>
        /// Lowers max height of list tiles
        /// </summary>
        [Parameter]
        public bool Dense { get; set; }

        /// <summary>
        /// Disables all children v-list-item components
        /// </summary>
        [Parameter]
        public bool Disabled { get; set; }

        /// <summary>
        /// Remove the highlighted background on active v-list-items
        /// </summary>
        [Parameter]
        public bool Flat { get; set; }

        /// <summary>
        /// An alternative styling that reduces v-list-item width and rounds the corners. Typically used with v-navigation-drawer
        /// </summary>
        [Parameter]
        public bool Nav { get; set; }

        /// <summary>
        /// Rounds the v-list-item edges
        /// </summary>
        [Parameter]
        public bool Rounded { get; set; }

        /// <summary>
        /// Removes top padding. Used when previous sibling is a header
        /// </summary>
        [Parameter]
        public bool Subheader { get; set; }

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

        /// <summary>
        /// Sets the height for the component.
        /// </summary>
        [Parameter]
        public StringNumber Height { get; set; }

        /// <summary>
        /// Sets the maximum height for the component.
        /// </summary>
        [Parameter]
        public StringNumber MinHeight { get; set; }

        /// <summary>
        /// Sets the minimum width for the component.
        /// </summary>
        [Parameter]
        public StringNumber MinWidth { get; set; }

        /// <summary>
        /// Sets the maximum height for the component.
        /// </summary>
        [Parameter]
        public StringNumber MaxHeight { get; set; }

        /// <summary>
        /// Sets the maximum width for the component.
        /// </summary>
        [Parameter]
        public StringNumber MaxWidth { get; set; }

        /// <summary>
        /// Sets the width for the component.
        /// </summary>
        [Parameter]
        public StringNumber Width { get; set; }

        [CascadingParameter]
        public BNavigationDrawer NavigationDrawer { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter]
        public IThemeable Themeable { get; set; }

        public bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return Themeable != null && Themeable.IsDark;
            }
        } 

        public override List<BListItem> Items { get; set; } = new List<BListItem>();

        protected override void SetComponentClass()
        {
            var prefix = "m-list";

            CssProvider
                .Apply<BList>(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-sheet")
                        .AddIf("m-sheet--outlined", () => Outlined)
                        .AddIf("m-sheet--shaped", () => Shaped)
                        .AddTheme(IsDark)
                        .Add("m-list")
                        .AddIf(() => $"elevation-{Elevation.Value}", () => Elevation != null)
                        .AddIf($"{prefix}--dense", () => Dense)
                        .AddIf($"{prefix}--disabled", () => Disabled)
                        .AddIf($"{prefix}--flat", () => Flat)
                        .AddIf($"{prefix}--nav", () => Nav || NavigationDrawer != null)
                        .AddIf($"{prefix}--rounded", () => Rounded)
                        .AddIf($"{prefix}--subheader", () => Subheader)
                        .AddIf($"{prefix}--two-line", () => TwoLine)
                        .AddIf($"{prefix}--three-line", () => ThreeLine);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddHeight(Height)
                        .AddWidth(Width)
                        .AddMinWidth(MinWidth)
                        .AddMaxWidth(MaxWidth)
                        .AddMinHeight(MinHeight)
                        .AddMaxHeight(MaxHeight);
                });
        }

        public override void Select(BListItem selectItem)
        {
            var groups = new List<BListGroup>();
            var group = new BListGroup();

            foreach (MListItem item in Items)
            {
                if (item != selectItem)
                {
                    item.Deactive();

                    if (item.ListGroup != null && !groups.Contains(item.ListGroup))
                    {
                        groups.Add(item.ListGroup);
                    }
                }
                else
                {
                    item.Active();

                    if (item.ListGroup != null)
                    {
                        group = item.ListGroup;
                    }
                }
            }

            groups.Remove(group);
            groups.ForEach(r => r.DeActive());
        }
    }
}
