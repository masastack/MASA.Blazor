using BlazorComponent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MFooter : BFooter, IThemeable
    {
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

        [Parameter]
        public bool Absolute {  get; set; }

        [Parameter]
        public bool App {  get; set;}

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public StringNumber Elevation {  get; set; }

        [Parameter]
        public bool Fixed {  get; set; }

        [Parameter]
        public StringNumber Height { get; set; } = "auto";

        [Parameter]
        public StringNumber Width { get; set; }

        [Parameter]
        public bool Inset { get; set; }

        [Parameter]
        public StringNumber MaxHeight { get; set; }

        [Parameter]
        public StringNumber MinHeight { get; set; }

        [Parameter]
        public StringNumber MaxWidth { get; set; }

        [Parameter]
        public StringNumber MinWidth { get; set; }

        [Parameter]
        public bool Padless { get; set; }

        [Parameter]
        public StringBoolean Rounded { get; set; }

        [Parameter]
        public bool Tile { get; set; }

        [Parameter]
        public int Right { get; set; }

        [Parameter]
        public int Left { get; set; }

        [Parameter]
        public int Bottom { get; set; }

        [Inject]
        public GlobalConfig GlobalConfig { get; set; }

        protected bool IsPositioned() => Absolute || Fixed || App;

        protected StringNumber ComputedLeft() => !IsPositioned() ?
            string.Empty :
            (App && Inset ? Left : 0);

        protected StringNumber ComputedRight() => !IsPositioned() ?
            string.Empty :
            (App && Inset ? Right : 0);

        protected StringNumber ComputedBottom() => !IsPositioned() ?
            string.Empty :
            (App ? Bottom : 0);

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-footer")
                        .Add("m-sheet")
                        .AddTheme(IsDark)
                        .AddBackgroundColor(Color)
                        .AddIf("m-footer--absolute", () => Absolute)
                        .AddIf("m-footer--fixed", () => !Absolute && (App || Fixed))
                        .AddIf("m-footer--padless", () => Padless)
                        .AddIf("m-footer--inset", () => Inset)
                        .AddElevation(Elevation)
                        .AddRounded(Rounded, Tile);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddBackgroundColor(Color)
                        .AddHeight(Height)
                        .AddWidth(Width)
                        .AddMaxHeight(MaxHeight)
                        .AddMaxWidth(MaxWidth)
                        .AddMinHeight(MinHeight)
                        .AddMinWidth(MinWidth)
                        .Add($"left:{ComputedLeft().ToUnit()}")
                        .Add($"right:{ComputedRight().ToUnit()}")
                        .Add($"bottom:{ComputedBottom().ToUnit()}");
                });
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var _documentElement = await JsInvokeAsync<BlazorComponent.Web.Element>(JsInteropConstants.GetDomInfo, Ref);
                UpdateApplication(_documentElement?.ClientHeight ?? 0);
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        protected void UpdateApplication(double clientHeight)
        {
            var val = Height.ToDouble() > 0 ? Height.ToDouble() : clientHeight;
            if (Inset)
                GlobalConfig.Application.InsetFooter = val;
            else
                GlobalConfig.Application.Footer = val;
        }

    }
}
