﻿using BlazorComponent.Web;

namespace Masa.Blazor
{
    public partial class MProgressLinear : BProgressLinear, IProgressLinear
    {
        [Inject]
        public Document Document { get; set; } = null!;

        [Inject]
        protected MasaBlazor MasaBlazor { get; set; } = null!;

        [Parameter]
        public bool Absolute { get; set; }

        [Parameter]
        public bool Fixed { get; set; }

        [Parameter]
        public bool Query { get; set; }

        [Parameter]
        public bool Rounded { get; set; }

        [Parameter]
        public bool Striped { get; set; }

        [Parameter]
        public bool Bottom { get; set; }

        [Parameter]
        public bool Top { get; set; }

        [Parameter]
        [MasaApiParameter(4)]
        public StringNumber Height { get; set; } = 4;

        [Parameter]
        [MasaApiParameter(true)]
        public bool Active { get; set; } = true;

        [Parameter]
        public string? BackgroundColor { get; set; }

        [Parameter]
        public double? BackgroundOpacity { get; set; }

        [Parameter]
        public bool Stream { get; set; }

        [Parameter]
        [MasaApiParameter(100)]
        public double BufferValue { get; set; } = 100;

        [Parameter]
        public bool Reverse { get; set; }

        [Parameter]
        public RenderFragment<double>? ChildContent { get; set; }

        [Obsolete("Use ValueChanged instead.")]
        [Parameter]
        public EventCallback<double> OnChange { get; set; }

        [Parameter] 
        public EventCallback<double> ValueChanged { get; set; }

        private bool IsReversed => MasaBlazor.RTL != Reverse;

        protected bool IsVisible { get; set; } = true;

        public override async Task HandleOnClickAsync(MouseEventArgs args)
        {
            if (!Reactive)
            {
                return;
            }

            var el = Document.GetElementByReference(Ref);
            if (el is null) return;

            var rect = await el.GetBoundingClientRectAsync();

            var value = args.OffsetX / rect.Width * 100;
            value = Math.Round(value, 0);

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(value);
            }
            else
            {
                Value = value;
            }

            if (OnChange.HasDelegate)
            {
                await OnChange.InvokeAsync(value);
            }
            else
            {
                Value = value;
            }
        }

        protected bool Reactive => ValueChanged.HasDelegate || OnChange.HasDelegate;

        protected int NormalizedValue => NormalizeValue(Value);

        protected int NormalizedBuffer => NormalizeValue(BufferValue);

        private bool IndependentTheme => (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

#if NET8_0_OR_GREATER
            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
#endif
            if (string.IsNullOrWhiteSpace(Color))
            {
                Color = "primary";
            }
        }

        protected override void SetComponentClass()
        {
            var prefix = "m-progress-linear";

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-progress-linear")
                        .AddIf($"{prefix}--absolute", () => Absolute)
                        .AddIf($"{prefix}--fixed", () => Fixed)
                        .AddIf($"{prefix}--query", () => Query)
                        .AddIf($"{prefix}--reactive", () => Reactive)
                        .AddIf($"{prefix}--reverse", () => IsReversed)
                        .AddIf($"{prefix}--rounded", () => Rounded)
                        .AddIf($"{prefix}--striped", () => Striped)
                        .AddIf($"{prefix}--visible", () => IsVisible)
                        .AddTheme(IsDark, IndependentTheme);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf("bottom:0", () => Bottom)
                        .AddIf("top:0", () => Top)
                        .AddIf(() => $"height:{Height.ToUnit()}", () => Active)
                        .AddTextColor(Color);
                })
                .Apply("stream", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__stream")
                        .AddTextColor(Color);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf(() => $"width:{100 - NormalizedBuffer}%", () => Stream)
                        .AddTextColor(Color);
                })
                .Apply("background", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__background")
                        .AddBackgroundColor(BackgroundColor ?? Color);
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add($"opacity: {BackgroundOpacity ?? (BackgroundColor != null ? 1 : 0.3)}")
                        .Add($"{(IsReversed ? "right" : "left")}: {NormalizedValue}%")
                        .Add($"width: {Math.Max(0, NormalizedBuffer - NormalizedValue)}%")
                        .AddBackgroundColor(BackgroundColor ?? Color);
                })
                .Apply("buffer", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__buffer");
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf("height: 0", () => Active == false)
                        .AddIf($"width: {NormalizedBuffer}%", () => (Indeterminate && NormalizedBuffer != 100));
                })
                //todo this.indeterminate ? VFadeTransition : VSlideXTransition
                .Apply("determinate", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__determinate")
                        .AddBackgroundColor(Color);
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add($"width: {NormalizedValue}%")
                        .AddBackgroundColor(Color);
                })
                .Apply("indeterminate", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__indeterminate")
                        .AddIf($"{prefix}__indeterminate--active", () => Active);
                })
                .Apply("long", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__indeterminate")
                        .Add("long")
                        .AddBackgroundColor(Color);
                })
                .Apply("short", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__indeterminate")
                        .Add("short")
                        .AddBackgroundColor(Color);
                })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__content");
                });

            AbstractProvider
                .ApplyProgressLinearDefault();
        }

        private static int NormalizeValue(StringNumber value)
        {
            int value1 = value.ToInt32();

            if (value1 < 0) return 0;
            if (value1 > 100) return 100;
            return value1;
        }
    }
}
