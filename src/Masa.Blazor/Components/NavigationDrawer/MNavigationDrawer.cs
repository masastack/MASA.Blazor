﻿using System.ComponentModel;

namespace Masa.Blazor
{
    public class MNavigationDrawer : BNavigationDrawer, INavigationDrawer
    {
        [Inject]
        public MasaBlazor? MasaBlazor { get; set; }

        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        [Parameter]
        public bool Bottom { get; set; }

        [Parameter]
        public bool Clipped { get; set; }

        [Parameter]
        public bool DisableResizeWatcher { get; set; }

        [Parameter]
        public bool DisableRouteWatcher { get; set; }

        [Parameter]
        public bool Floating { get; set; }

        [Parameter]
        public StringNumber? Height
        {
            get => GetValue<StringNumber>(App ? "100vh" : "100%");
            set => SetValue(value);
        }

        [Parameter]
        [ApiDefaultValue(56)]
        public StringNumber? MiniVariantWidth { get; set; } = 56;

        [Parameter]
        public bool Right { get; set; }

        [Parameter]
        public bool Touchless { get; set; }

        [Parameter]
        [ApiDefaultValue("256px")]
        public StringNumber Width { get; set; } = "256px";

        [Parameter]
        public string? Color { get; set; }

        [Parameter]
        public OneOf<Breakpoints, double>? MobileBreakpoint
        {
            get => GetValue(MasaBlazor?.Breakpoint.MobileBreakpoint);
            set => SetValue(value);
        }

        [Parameter]
        public string? OverlayColor { get; set; }

        [Parameter]
        public StringNumber? OverlayOpacity { get; set; }

        [Parameter]
        public bool Absolute { get; set; }

        [Parameter]
        public bool Fixed { get; set; }

        [Parameter]
        public RenderFragment? AppendContent { get; set; }

        [Parameter]
        public RenderFragment? PrependContent { get; set; }

        private readonly string[] _applicationProperties = new string[]
        {
            "Bottom", "Footer", "Bar", "Top"
        };

        protected StringNumber? ComputedMaxHeight
        {
            get
            {
                if (!HasApp) return null;

                var computedMaxHeight = MasaBlazor!.Application.Bottom + MasaBlazor.Application.Footer + MasaBlazor.Application.Bar;

                if (!Clipped) return computedMaxHeight;

                return computedMaxHeight + MasaBlazor.Application.Top;
            }
        }

        protected StringNumber ComputedTop
        {
            get
            {
                if (!HasApp)
                {
                    return 0;
                }

                var computedTop = MasaBlazor!.Application.Bar;
                computedTop += Clipped ? MasaBlazor.Application.Top : 0;

                return computedTop;
            }
        }

        protected StringNumber ComputedTransform
        {
            get
            {
                if (IsActive)
                {
                    return 0;
                }

                if (IsBottom)
                {
                    return 100;
                }

                return Right ? 100 : -100;
            }
        }

        protected StringNumber? ComputedWidth => IsMiniVariant ? MiniVariantWidth : Width;

        protected bool HasApp => App && (!IsMobile && !Temporary);

        protected bool IsBottom => Bottom && IsMobile;

        protected bool IsMiniVariant => (!ExpandOnHover && MiniVariant) || (ExpandOnHover && !IsMouseover);

        protected int ZIndex { get; set; }

        protected override bool IsMobileBreakpoint
        {
            get
            {
                if (MasaBlazor == null) return false;

                var mobile = MasaBlazor.Breakpoint.Mobile;
                var width = MasaBlazor.Breakpoint.Width;
                var name = MasaBlazor.Breakpoint.Name;
                var mobileBreakpoint = MasaBlazor.Breakpoint.MobileBreakpoint;

                if (Equals(mobileBreakpoint.Value, MobileBreakpoint?.Value))
                {
                    return mobile;
                }

                return mobileBreakpoint.IsT1 ? width < mobileBreakpoint.AsT1 : name == mobileBreakpoint.AsT0;
            }
        }

        protected bool ReactsToResize => !DisableResizeWatcher && !Stateless;

        protected bool ReactsToMobile => App && !DisableResizeWatcher && !Permanent && !Stateless && !Temporary;

        protected bool ReactsToRoute => !DisableRouteWatcher && !Stateless && (Temporary || IsMobile);

        protected override bool IsFullscreen => MasaBlazor != null && MasaBlazor.Breakpoint.SmAndDown;

        public override IEnumerable<string> DependentSelectors
            => base.DependentSelectors.Concat(new[] { MSnackbar.ROOT_CSS_SELECTOR, PEnqueuedSnackbars.ROOT_CSS_SELECTOR }).Distinct();

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Init();

            MasaBlazor!.Breakpoint.OnUpdate += OnBreakpointOnUpdate;

            if (Value == null && ValueChanged.HasDelegate)
            {
                var val = !MasaBlazor.Breakpoint.Mobile && !Temporary;
                _ = ValueChanged.InvokeAsync(val);
            }

            if (App)
            {
                MasaBlazor.Application.HasNavigationDrawer = !Temporary;
            }

            MasaBlazor.Application.PropertyChanged += ApplicationPropertyChanged;

            NavigationManager!.LocationChanged += OnLocationChanged;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await UpdateApplicationAsync();

                ZIndex = await GetActiveZIndexAsync();

                StateHasChanged();
            }
        }

        protected override void RegisterWatchers(PropertyWatcher watcher)
        {
            base.RegisterWatchers(watcher);

            watcher
                .Watch<bool?>(nameof(Value), val =>
                {
                    if (Permanent)
                    {
                        return;
                    }

                    if (val == null)
                    {
                        Init();
                        return;
                    }

                    if (val != IsActive)
                    {
                        IsActive = val.Value;
                    }
                })
                .Watch<bool>(nameof(IsActive), val =>
                {
                    if (ValueChanged.HasDelegate)
                    {
                        ValueChanged.InvokeAsync(val);
                    }
                    else
                    {
                        Value = val;
                    }

                    // OverlayRef is not null in the next tick.
                    NextTick(async () =>
                    {
                        if (val)
                        {
                            if (ShowOverlay)
                            {
                                await HideScroll();
                            }
                        }
                        else
                        {
                            if (!ShowOverlay)
                            {
                                await ShowScroll();
                            }
                        }
                    });

                    //We will remove this when mixins applicationable finished
                    _ = UpdateApplicationAsync();
                })
                .Watch<bool>(nameof(ExpandOnHover), val => { UpdateMiniVariant(val, false); })
                .Watch<bool>(nameof(IsMouseover), val => { UpdateMiniVariant(!val); });
        }

        private async void OnLocationChanged(object? sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        {
            if (ReactsToRoute && CloseConditional())
            {
                IsActive = false;
                await ValueChanged.InvokeAsync(false);
                await InvokeStateHasChangedAsync();
            }
        }

        private async void OnBreakpointOnUpdate(object? sender, BreakpointChangedEventArgs e)
        {
            if (!e.MobileChanged)
            {
                return;
            }

            if (!ReactsToResize || !ReactsToMobile)
            {
                return;
            }

            NextTick(async () =>
            {
                //When window resize,we should update ZIndex for Overlay 
                ZIndex = await GetActiveZIndexAsync();
                StateHasChanged();
            });

            IsActive = !IsMobile;

            await InvokeStateHasChangedAsync();
        }

        private Task<int> GetActiveZIndexAsync() => JsInvokeAsync<int>(JsInteropConstants.GetZIndex, Ref);

        private void ApplicationPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (_applicationProperties.Contains(e.PropertyName))
            {
                InvokeStateHasChanged();
            }
        }

        private void UpdateMiniVariant(bool val, bool shouldRender = true)
        {
            if (ExpandOnHover && MiniVariant != val)
            {
                if (MiniVariantChanged.HasDelegate)
                {
                    MiniVariantChanged.InvokeAsync(val);
                }
                else
                {
                    if (shouldRender)
                    {
                        StateHasChanged();
                    }
                }
            }
        }

        private void Init()
        {
            if (Permanent)
            {
                IsActive = true;
            }
            else if (Stateless || Value != null)
            {
                IsActive = Value!.Value;
            }
            else if (!Temporary)
            {
                IsActive = !IsMobile;
            }
        }

        protected override void SetComponentClass()
        {
            var prefix = "m-navigation-drawer";

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-navigation-drawer")
                        .AddIf($"{prefix}--absolute", () => Absolute)
                        .AddIf($"{prefix}--bottom", () => Bottom)
                        .AddIf($"{prefix}--clipped", () => Clipped)
                        .AddIf($"{prefix}--close", () => !IsActive)
                        .AddIf($"{prefix}--fixed", () => !Absolute && (App || Fixed))
                        .AddIf($"{prefix}--floating", () => Floating)
                        .AddIf($"{prefix}--is-mobile", () => IsMobile)
                        .AddIf($"{prefix}--is-mouseover", () => IsMouseover)
                        .AddIf($"{prefix}--mini-variant", () => IsMiniVariant)
                        .AddIf($"{prefix}--custom-mini-variant", () => MiniVariantWidth?.ToString() != "56")
                        .AddIf($"{prefix}--open", () => IsActive)
                        .AddIf($"{prefix}--open-on-hover", () => ExpandOnHover)
                        .AddIf($"{prefix}--right", () => Right)
                        .AddIf($"{prefix}--temporary", () => Temporary)
                        .AddTheme(IsDark)
                        .AddBackgroundColor(Color);
                }, styleBuilder =>
                {
                    var translate = IsBottom ? "translateY" : "translateX";
                    styleBuilder
                        .AddHeight(Height)
                        .Add(() => $"top:{(!IsBottom ? ComputedTop.ToUnit() : "auto")}")
                        .AddIf(() => $"max-height:calc(100% - {ComputedMaxHeight.ToUnit()})", () => ComputedMaxHeight != null)
                        .AddIf(() => $"transform:{translate}({ComputedTransform.ToUnit("%")})", () => ComputedTransform != null)
                        .AddWidth(ComputedWidth)
                        .AddBackgroundColor(Color);
                })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__content");
                })
                .Apply("border", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__border");
                })
                .Apply("prepend", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__prepend");
                })
                .Apply("append", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__append");
                })
                .Apply("image", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__image");
                });

            Attributes.Add("data-booted", "true");

            AbstractProvider
                .ApplyNavigationDrawerDefault()
                .Apply(typeof(IImage), typeof(MImage), attrs =>
                {
                    attrs[nameof(MImage.Src)] = Src;
                    attrs[nameof(MImage.Height)] = (StringNumber)"100%";
                    attrs[nameof(MImage.Width)] = (StringNumber)"100%";
                })
                .Apply<BOverlay, MOverlay>(attrs =>
                {
                    attrs[nameof(MOverlay.ZIndex)] = ZIndex;
                    attrs[nameof(MOverlay.Absolute)] = !Fixed;
                    attrs[nameof(MOverlay.Value)] = ShowOverlay;
                });
        }

        protected override async void CallUpdate()
        {
            base.CallUpdate();

            await UpdateApplicationAsync();
        }

        protected async Task UpdateApplicationAsync()
        {
            if (!App)
            {
                return;
            }

            var val = (!IsActive || IsMobile || Temporary)
                ? 0
                : (ComputedWidth.ToDouble() <= 0 ? await GetClientWidthAsync() : ComputedWidth.ToDouble());

            if (Right)
                MasaBlazor!.Application.Right = val;
            else
                MasaBlazor!.Application.Left = val;
        }

        private async Task<double> GetClientWidthAsync()
        {
            if (Ref.Context == null)
            {
                return 0;
            }

            var element = await JsInvokeAsync<BlazorComponent.Web.Element>(
                JsInteropConstants.GetDomInfo, Ref);
            return element.ClientWidth;
        }

        public override async Task HandleOnClickAsync(MouseEventArgs e)
        {
            if (MiniVariantChanged.HasDelegate)
            {
                await MiniVariantChanged.InvokeAsync(false);
            }
        }

        protected override void Dispose(bool disposing)
        {
            RemoveApplication();
            MasaBlazor!.Breakpoint.OnUpdate -= OnBreakpointOnUpdate;
            MasaBlazor.Application.PropertyChanged -= ApplicationPropertyChanged;
            NavigationManager!.LocationChanged -= OnLocationChanged;
        }

        private void RemoveApplication()
        {
            if (!App)
            {
                return;
            }

            if (Right)
                MasaBlazor!.Application.Right = 0;
            else
                MasaBlazor!.Application.Left = 0;
        }
    }
}
