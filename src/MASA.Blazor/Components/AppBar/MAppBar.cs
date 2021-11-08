using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OneOf;
using Element = BlazorComponent.Web.Element;

namespace MASA.Blazor
{
    public partial class MAppBar : MToolbar, IScrollable, IThemeable
    {
        private string elevateClass = "m-app-bar--hide-shadow";

        private string collapseClass = "m-toolbar--collapse";

        private string hideScrollClass = "";

        private string invertedClass = "m-app-bar--hide-shadow";

        private string shrinkClass = "v-app-bar--shrink-on-scroll";

        //状态记录标识
        private bool isElevated, isCollapsed, isHided, isShrinked, isInverted, shouldUpdateState;

        [Parameter]
        public bool App { get; set; }

        [Parameter]
        public bool Fixed { get; set; }

        [Parameter]
        public StringNumber MarginTop { get; set; }

        public int? Transform { get; private set; } = 0;

        [Parameter]
        public StringNumber Left { get; set; } = 0;

        [Parameter]
        public StringNumber Right { get; set; } = 0;

        [Parameter]
        public bool ClippedLeft { get; set; }

        [Parameter]
        public bool ClippedRight { get; set; }

        [Parameter]
        public bool CollapseOnScroll { get; set; }

        [Parameter]
        public string ScrollTarget { get; set; }

        [Parameter]
        public bool ElevateOnScroll { get; set; }

        [Parameter]
        public bool FadeImgOnScroll { get; set; }

        [Parameter]
        public bool HideOnScroll { get; set; }

        [Parameter]
        public bool InvertedScroll { get; set; }

        [Parameter]
        public bool ShrinkOnScroll { get; set; }

        [Parameter]
        public int ScrollThreshold { get; set; } = 20;

        [Parameter]
        public bool ScrollOffScreen { get; set; }

        [Parameter]
        public bool Value { get; set; } = true;

        [Inject]
        public DomEventJsInterop DomEventJsInterop { get; set; }

        [Inject]
        public GlobalConfig GlobalConfig { get; set; }

        private bool _isActive => Value;

        #region  IScrollable
        public int CurrentScroll { get; private set; }

        public int SavedScroll { get; private set; }

        public void Scrolling()
        {
            if (ElevateOnScroll)
            {

                if (CurrentScroll <= ScrollThreshold && isElevated)
                {
                    elevateClass = "m-app-bar--hide-shadow";
                    isElevated = false;
                    shouldUpdateState = true;
                }

                if (CurrentScroll > ScrollThreshold && !isElevated)
                {
                    elevateClass = "m-app-bar--is-scrolled";
                    isElevated = true;
                    shouldUpdateState = true;
                }

            }

            if (!Collapse && CollapseOnScroll)
            {
                if (CurrentScroll <= ScrollThreshold && isCollapsed)
                {
                    collapseClass = "m-toolbar--collapse";
                    shouldUpdateState = true;
                    isCollapsed = false;
                }
                if (CurrentScroll > ScrollThreshold && !isCollapsed)
                {
                    collapseClass = "m-toolbar--collapse m-toolbar--collapsed m-app-bar--is-scrolled";
                    shouldUpdateState = true;
                    isCollapsed = true;
                }

            }

            if (HideOnScroll)
            {
                if (CurrentScroll <= ScrollThreshold && isHided)
                {
                    hideScrollClass = "";
                    Transform = 0;
                    shouldUpdateState = true;
                    isHided = false;
                }
                if (CurrentScroll > ScrollThreshold && !isHided)
                {
                    hideScrollClass = "m-app-bar--hide-shadow m-app-bar--is-scrolled";
                    Transform = -ComputedHeight.ToInt32();
                    shouldUpdateState = true;
                    isHided = true;
                }
            }

            if (InvertedScroll)
            {
                if (CurrentScroll <= ScrollThreshold && isInverted)
                {
                    Transform = -ComputedHeight.ToInt32();
                    invertedClass = "m-app-bar--hide-shadow";
                    shouldUpdateState = true;
                    isInverted = false;
                }
                if (CurrentScroll > ScrollThreshold && !isInverted)
                {
                    Transform = 0;
                    invertedClass = "m-app-bar--is-scrolled";
                    shouldUpdateState = true;
                    isInverted = true;
                }

            }

            if (ShrinkOnScroll)
            {
                if (CurrentScroll <= ScrollThreshold && isShrinked)
                {
                    shrinkClass = "m-app-bar--shrink-on-scroll";
                    Prominent = true;
                    shouldUpdateState = true;
                    isShrinked = false;
                }
                if (CurrentScroll > ScrollThreshold && !isShrinked)
                {
                    shrinkClass = "m-app-bar--is-scrolled m-app-bar--shrink-on-scroll";
                    Prominent = false;
                    shouldUpdateState = true;
                    isShrinked = true;
                }
            }

            if (shouldUpdateState)
            {
                InvokeStateHasChanged();
                shouldUpdateState = false;
            }
        }
        #endregion

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            if (InvertedScroll)
            {
                Transform = -ComputedHeight.ToInt32();
            }

            if (ShrinkOnScroll)
            {
                Dense = false;
                Flat = false;
                Prominent = true;
            }

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-app-bar")
                        .AddIf("m-app-bar--clipped", () => ClippedLeft || ClippedRight)
                        .AddIf("m-app-bar--fixed", () => !Absolute && (App || Fixed))
                        .AddIf(collapseClass, () => !Collapse && CollapseOnScroll)
                        .AddIf("m-app-bar--elevate-on-scroll", () => ElevateOnScroll)
                        .AddIf(elevateClass, () => ElevateOnScroll)
                        .AddIf(hideScrollClass, () => HideOnScroll)
                        .AddIf(invertedClass, () => InvertedScroll)
                        .AddIf(shrinkClass, () => ShrinkOnScroll);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf(() => $"height:{Height.ToUnit()}", () => Height != null)
                        .AddIf(() => $"margin-top:{MarginTop.ToUnit()}", () => MarginTop != null)
                        .AddIf(() => $"transform:translateY({Transform}px)", () => Transform != null)
                        .AddIf(() => $"left:{Left.ToUnit()}", () => Left != null)
                        .AddIf(() => $"right:{Right.ToUnit()}", () => Right != null);
                });

            Attributes.Add("data-booted", true);
        }

        protected async override Task OnFirstAfterRenderAsync()
        {
            if (!string.IsNullOrWhiteSpace(ScrollTarget))
            {
                DomEventJsInterop.AddEventListener<Element>($"#{ScrollTarget}", "scroll", (e) =>
                {

                    SavedScroll = CurrentScroll;
                    CurrentScroll = (int)e.ScrollTop;

                    //todo  ScrollThreshold的作用需要明确 是滚动距离触发scrolling的值还是改变appbar状态scrolltop的值 还是只针对shrink-on-scroll有效
                    //if (Math.Abs(CurrentScroll - SavedScroll) > ScrollThreshold)//(IScrollable)this.ComputedScrollThreshold() 
                    Scrolling();

                }, false);
            }
            await base.OnFirstAfterRenderAsync();
        }

        protected override void Dispose(bool disposing)
        {
            if (!string.IsNullOrWhiteSpace(ScrollTarget))
            {
                DomEventJsInterop.RemoveEventListener<Element>($"#{ScrollTarget}", "scroll", e =>
                {

                });
            }

            base.Dispose(disposing);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                UpdateApplication();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        protected void UpdateApplication()
        {
            var val = InvertedScroll ? 0 : ComputedHeight.ToDouble() + ComputedTransform();
            if (!Bottom)
                GlobalConfig.Application.Top = val;
            else
                GlobalConfig.Application.Bottom = val;
        }

        protected double ComputedTransform()
        {
            if (!CanScroll() || _isActive ||
                (ElevateOnScroll && CurrentScroll == 0 && _isActive))
                return 0;

            var scrollOffScreen = ScrollOffScreen ? ComputedHeight.ToDouble() : ComputedContentHeightAppBar();

            return Bottom ? scrollOffScreen : -scrollOffScreen;
        }

        protected bool CanScroll()
        {
            return
                InvertedScroll ||
                ElevateOnScroll ||
                HideOnScroll ||
                CollapseOnScroll ||
                GlobalConfig.Application.IsBooted ||
                !Value;
        }

        protected double ComputedContentHeightAppBar()
        {
            if (!ShrinkOnScroll)
                return ComputedContentHeight.ToInt32();

            var min = Dense ? 48 : 56;
            var max = ComputedOriginalHeightAppBar();

            return min + (max - min) * ScrollRatio();
        }

        protected double ComputedOriginalHeightAppBar()
        {
            var height = ComputedContentHeight.ToDouble();
            if (isElevated)
                height += ExtensionHeight.ToDouble();

            return height;
        }

        protected double ScrollRatio()
        {
            var threshold = ComputedScrollThreshold();

            return Math.Max((threshold - CurrentScroll) / threshold, 0);
        }

        protected double ComputedScrollThreshold()
        {
            if (ScrollThreshold > 0)
                return ScrollThreshold;

            return ComputedOriginalHeightAppBar() - (Dense ? 48 : 56);
        }
    }
}
