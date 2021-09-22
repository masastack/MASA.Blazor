using BlazorComponent;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public class MSlider : MInput<double>, ISlider
    {
        [Parameter]
        public bool Vertical { get; set; }

        [Inject]
        public Document Document { get; set; }

        [Parameter]
        public double Max { get; set; } = 100;

        [Parameter]
        public double Min { get; set; } = 0;

        [Parameter]
        public double Step { get; set; } = 1;

        [Parameter]
        public List<string> TickLabels { get; set; } = new();

        [Parameter]
        public StringBoolean Ticks { get; set; } = false;

        [Inject]
        public GlobalConfig GlobalConfig { get; set; }

        [Parameter]
        public string TrackColor { get; set; }

        [Parameter]
        public string TrackFillColor { get; set; }

        [Parameter]
        public double TickSize { get; set; } = 2;

        [Parameter]
        public StringBoolean ThumbLabel { get; set; }

        [Parameter]
        public RenderFragment<double> ThumbLabelContent { get; set; }

        RenderFragment ISlider.ThumbLabelContent
        {
            get
            {
                var val = RoundValue(InternalValue);

                if (ThumbLabelContent != null)
                {

                    return ThumbLabelContent(val);
                }

                return builder =>
                {
                    builder.OpenElement(0, "span");
                    builder.AddContent(1, val);
                    builder.CloseElement();
                };
            }
        }

        [Parameter]
        public string ThumbColor { get; set; }

        [Parameter]
        public StringNumber ThumbSize { get; set; } = 32;

        [Parameter]
        public EventCallback<FocusEventArgs> OnFocus { get; set; }

        [Parameter]
        public EventCallback<FocusEventArgs> OnBlur { get; set; }

        [Parameter]
        public EventCallback<double> OnChange { get; set; }

        [Parameter]
        public bool InverseLabel { get; set; }

        public bool IsActive { get; set; }

        public bool NoClick { get; set; }

        public ElementReference ThumbElement { get; set; }

        public ElementReference TrackElement { get; set; }

        public bool ThumbPressed { get; set; }

        public double StartOffset { get; set; }

        public double OldValue { get; set; }

        public CancellationTokenSource MouseCancellationTokenSource { get; set; }

        public HtmlElement App { get; set; }

        public Dictionary<string, object> InputAttrs { get; } = new();

        public string TrackTransition
        {
            get
            {
                if (ThumbPressed)
                {
                    if (ShowTicks || Step > 0)
                    {
                        return "0.1s cubic-bezier(0.25, 0.8, 0.5, 1)";
                    }
                    else
                    {
                        return "none";
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public bool ShowTicks
        {
            get
            {
                return TickLabels.Count > 0 || (!IsDisabled && Step > 0 && Ticks != false);
            }
        }

        public double InputWidth
        {
            get
            {
                return (RoundValue(InternalValue) - Min) / (Max - Min) * 100;
            }
        }

        public double StepNumeric
        {
            get
            {
                return Step > 0 ? Step : 0;
            }
        }

        public string ComputedTrackColor
        {
            get
            {
                if (IsDisabled)
                {
                    return null;
                }

                if (TrackColor != null)
                {
                    return TrackColor;
                }

                if (IsDark)
                {
                    return ValidationState;
                }

                return string.IsNullOrEmpty(ValidationState) ? "primary lighten-3" : ValidationState;
            }
        }

        public string ComputedTrackFillColor
        {
            get
            {
                if (IsDisabled)
                {
                    return null;
                }

                if (TrackFillColor != null)
                {
                    return TrackFillColor;
                }

                return string.IsNullOrEmpty(ValidationState) ? ComputedColor : ValidationState;
            }
        }

        public string ComputedThumbColor
        {
            get
            {
                if (ThumbColor != null)
                {
                    return ThumbColor;
                }

                return string.IsNullOrEmpty(ValidationState) ? ComputedColor : ValidationState;
            }
        }

        public double NumTicks
        {
            get
            {
                return Math.Ceiling((Max - Min) / StepNumeric);
            }
        }

        public bool ShowThumbLabel
        {
            get
            {
                return !IsDisabled && ((ThumbLabel != null && ThumbLabel != false) || ThumbLabelContent != null);
            }
        }

        public Dictionary<string, object> ThumbAttrs => new()
        {
            { "role", "slider" },
            { "tabindex", IsDisabled ? -1 : 0 }
        };

        double ISlider.InternalValue => InternalValue;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (App == null)
            {
                App = Document.QuerySelector("[data-app]");
            }
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (OnChange.HasDelegate)
            {
                ValueChanged = OnChange;
            }
        }

        public async Task HandleOnSliderClickAsync(MouseEventArgs args)
        {
            if (NoClick)
            {
                NoClick = false;
                return;
            }

            await ThumbElement.FocusAsync();

            await HandleOnMouseMoveAsync(args);
        }

        public async Task HandleOnSliderMouseDownAsync(ExMouseEventArgs args)
        {
            OldValue = InternalValue;
            IsActive = true;

            if (args.Target.Class.Contains("m-slider__thumb-container"))
            {
                ThumbPressed = true;
                var container = Document.QuerySelector($"#{Id} .m-slider__thumb-container");
                var domRect = await container.GetBoundingClientRectAsync();
                StartOffset = Vertical ? (args.ClientY - (domRect.Top + domRect.Height / 2)) : (args.ClientX - (domRect.Left + domRect.Width / 2));
            }
            else
            {
                StartOffset = 0;

                MouseCancellationTokenSource?.Cancel();
                MouseCancellationTokenSource = new CancellationTokenSource();

                _ = Task.Run(async () =>
                  {
                      await Task.Delay(300, MouseCancellationTokenSource.Token);
                      ThumbPressed = true;

                      InvokeStateHasChanged();
                  });
            }

            await HandleOnMouseMoveAsync(args);
            await App.AddEventListenerAsync("mousemove", CreateEventCallback<MouseEventArgs>(HandleOnMouseMoveAsync), false);
            await App.AddEventListenerAsync("mouseup", CreateEventCallback<MouseEventArgs>(HandleOnSliderMouseUpAsync), new EventListenerOptions
            {
                Capture = true,
                Once = true
            });
        }

        private EventCallback<T> CreateEventCallback<T>(Func<T, Task> func)
        {
            return EventCallback.Factory.Create(this, func);
        }

        public async Task HandleOnSliderMouseUpAsync(MouseEventArgs args)
        {
            MouseCancellationTokenSource?.Cancel();
            ThumbPressed = false;
            await App.RemoveEventListenerAsync("mousemove");

            IsActive = false;
        }

        public async Task HandleOnMouseMoveAsync(MouseEventArgs args)
        {
            if (args.Type == "mousemove")
            {
                ThumbPressed = true;
            }

            InternalValue = RoundValue(await ParseMouseMoveAsync(args));
        }

        private async Task<double> ParseMouseMoveAsync(MouseEventArgs args)
        {
            var track = Document.QuerySelector(TrackElement);
            var rect = await track.GetBoundingClientRectAsync();

            var tractStart = Vertical ? rect.Top : rect.Left;
            var trackLength = Vertical ? rect.Height : rect.Width;
            var clickOffset = Vertical ? args.ClientY : args.ClientX;

            var clickPos = Math.Min(Math.Max((clickOffset - tractStart - StartOffset) / trackLength, 0), 1);
            if (Vertical)
            {
                clickPos = 1 - clickPos;
            }
            //TODO:rtl

            return Min + clickPos * (Max - Min);
        }

        private double RoundValue(double value)
        {
            if (StepNumeric == 0)
            {
                return value;
            }

            var trimmedStep = Step.ToString().Trim();
            var decimals = trimmedStep.IndexOf('.') > -1
                ? (trimmedStep.Length - trimmedStep.IndexOf('.') - 1)
                : 0;
            var offset = Min % StepNumeric;

            var newValue = Math.Round((value - offset) / StepNumeric) * StepNumeric + offset;
            return Math.Round(Math.Min(newValue, Max), decimals);
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-input__slider")
                        .AddIf("m-input__slider--vertical", () => Vertical)
                        .AddIf("m-input__slider--inverse-label", () => InverseLabel);
                })
                .Apply("slider", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-slider")
                        .AddIf("m-slider--horizontal", () => !Vertical)
                        .AddIf("m-slider--vertical", () => Vertical)
                        .AddIf("m-slider--focused", () => IsFocused)
                        .AddIf("m-slider--active", () => IsActive)
                        .AddIf("m-slider--disabled", () => IsDisabled)
                        .AddIf("m-slider--readonly", () => IsReadonly)
                        .AddTheme(IsDark);
                })
                .Apply("track-container", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-slider__track-container");
                })
                .Apply("track-background", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-slider__track-background")
                        .AddBackgroundColor(ComputedTrackColor);
                }, styleBuilder =>
                {
                    var startDir = Vertical ? GlobalConfig.RTL ? "bottom" : "top" : GlobalConfig.RTL ? "left" : "right";
                    var endDir = Vertical ? "height" : "width";

                    var start = "0px";
                    var end = IsDisabled ? $"calc({100 - InputWidth}% - 10px)" : $"calc({100 - InputWidth}%)";

                    styleBuilder
                        .AddBackgroundColor(ComputedTrackColor)
                        .AddTransition(TrackTransition)
                        .Add($"{startDir}:{start}")
                        .Add($"{endDir}:{end}");
                })
                .Apply("track-fill", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-slider__track-fill")
                        .AddBackgroundColor(ComputedTrackFillColor);
                }, styleBuilder =>
                {
                    var startDir = Vertical ? "bottom" : "left";
                    var endDir = Vertical ? "top" : "right";
                    var valueDir = Vertical ? "height" : "width";

                    var start = GlobalConfig.RTL ? "auto" : "0";
                    var end = GlobalConfig.RTL ? "0" : "auto";
                    var value = IsDisabled ? $"calc({InputWidth}% - 10px)" : $"{InputWidth}%";

                    styleBuilder
                        .AddBackgroundColor(ComputedTrackFillColor)
                        .AddTransition(TrackTransition)
                        .Add($"{startDir}:{start}")
                        .Add($"{endDir}:{end}")
                        .Add($"{valueDir}:{value}");
                })
                .Apply("ticks-container", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-slider__ticks-container")
                        .AddIf("m-slider__ticks-container--always-show", () => Ticks == "always" || TickLabels.Count > 0);
                })
                .Apply("tick", cssBuilder =>
                {
                    var width = cssBuilder.Index * (100 / NumTicks);
                    var filled = GlobalConfig.RTL ? (100 - InputWidth) < width : width < InputWidth;

                    cssBuilder
                        .Add("m-slider__tick")
                        .AddIf("m-slider__tick--filled", () => filled);
                }, styleBuilder =>
                {
                    var direction = Vertical ? "bottom" : (GlobalConfig.RTL ? "right" : "left");
                    var offsetDirection = Vertical ? (GlobalConfig.RTL ? "left" : "right") : "top";
                    var width = styleBuilder.Index * (100 / NumTicks);

                    styleBuilder
                        .AddWidth(TickSize)
                        .AddHeight(TickSize)
                        .Add($"{direction}:calc({width}% - {TickSize / 2}px)")
                        .Add($"{offsetDirection}:calc(50% - {TickSize / 2}px)");
                })
                .Apply("tick-label", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-slider__tick-label");
                })
                .Apply("thumb-container", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-slider__thumb-container")
                        .AddIf("m-slider__thumb-container--active", () => IsActive)
                        .AddIf("m-slider__thumb-container--focused", () => IsFocused)
                        .AddIf("m-slider__thumb-container--show-label", () => ShowThumbLabel)
                        .AddTextColor(ComputedThumbColor);
                }, styleBuilder =>
                {
                    var direction = Vertical ? "top" : "left";
                    var value = GlobalConfig.RTL ? 100 - InputWidth : InputWidth;
                    value = Vertical ? 100 - value : value;

                    styleBuilder
                        .AddTransition(TrackTransition)
                        .Add($"{direction}:{value}%")
                        .AddTextColor(ComputedThumbColor);
                })
                .Apply("thumb", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-slider__thumb")
                        .AddBackgroundColor(ComputedThumbColor);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddBackgroundColor(ComputedThumbColor);
                })
                .Apply("thumb-label-container", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-slider__thumb-label-container");
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf("display:none", () => !(IsFocused || IsActive || ThumbLabel == "always"));
                })
                .Apply("thumb-label", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-slider__thumb-label")
                        .AddBackgroundColor(ComputedThumbColor);
                }, styleBuilder =>
                {
                    var transform = Vertical
                        ? $"translateY(20%) translateY({(ThumbSize.ToInt32() / 3) - 1}px) translateX(55%) rotate(135deg)"
                        : "translateY(-20%) translateY(-12px) translateX(-50%) rotate(45deg)";
                    styleBuilder
                        .AddHeight(ThumbSize)
                        .AddWidth(ThumbSize)
                        .Add($"transform:{transform}")
                        .AddBackgroundColor(ComputedThumbColor);
                });

            AbstractProvider
                .ApplySliderDefault();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                GlobalConfig.OnRTLChange += OnRTLChange;
            }
        }

        private void OnRTLChange(bool rtl)
        {
            InvokeStateHasChanged();
        }

        public async Task HandleOnFocusAsync(FocusEventArgs args)
        {
            IsFocused = true;
            if (OnFocus.HasDelegate)
            {
                await OnFocus.InvokeAsync(args);
            }
        }

        public async Task HandleOnBlurAsync(FocusEventArgs args)
        {
            IsFocused = false;
            if (OnBlur.HasDelegate)
            {
                await OnBlur.InvokeAsync(args);
            }
        }

        public async Task HandleOnKeyDownAsync(KeyboardEventArgs args)
        {
            if (!IsInteractive)
            {
                return;
            }

            var value = ParseKeyDown(args, InternalValue);

            if (value == null || value.AsT2 < Min || value.AsT2 > Max)
            {
                return;
            }

            InternalValue = RoundValue(value.AsT2);
            if (OnChange.HasDelegate)
            {
                await OnChange.InvokeAsync(InternalValue);
            }
        }

        private StringNumber ParseKeyDown(KeyboardEventArgs args, double value)
        {
            if (!IsInteractive)
            {
                return null;
            }

            var keyCodes = new string[] { "pageup", "pagedown", "end", "home", "left", "right", "down", "up" };
            var directionCodes = new string[] { "left", "right", "down", "up" };
            if (!keyCodes.Contains(args.Code))
            {
                return null;
            }

            var step = StepNumeric == 0 ? 1 : StepNumeric;
            var steps = Max - Min / step;
            if (directionCodes.Contains(args.Code))
            {
                var increase = GlobalConfig.RTL ? new string[] { "left", "up" } : new string[] { "right", "up" };
                var direction = increase.Contains(args.Code) ? 1 : -1;
                var multiplier = args.ShiftKey ? 3 : (args.CtrlKey ? 2 : 1);

                value += (direction * step * multiplier);
            }
            else if (args.Code == "home")
            {
                value = Min;
            }
            else if (args.Code == "end")
            {
                value = Max;
            }
            else
            {
                var direction = args.Code == "pagedown" ? 1 : -1;
                value = value - (direction * step * (steps > 100 ? steps / 10 : 10));
            }

            return value;
        }
    }
}
