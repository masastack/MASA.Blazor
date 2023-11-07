using Microsoft.AspNetCore.Components.Web;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Masa.Blazor
{
    public partial class MSnackbar : BSnackbar, IThemeable, ISnackbar
    {
        [Parameter]
        public bool Absolute { get; set; }

        [Parameter]
        public bool Value { get; set; }

        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }

        [Parameter]
        public EventCallback OnClosed { get; set; }

        [Parameter]
        public bool Bottom { get; set; }

        [Parameter]
        public bool Centered { get; set; }

        [Parameter]
        public bool Left { get; set; }

        [Parameter]
        public bool MultiLine { get; set; }

        [Parameter]
        public bool Right { get; set; }

        [Parameter]
        public bool Top { get; set; }

        [Parameter]
        public bool Vertical { get; set; }

        [Parameter]
        public bool Shaped { get; set; }

        [Parameter]
        [MassApiParameter(5000)]
        public int Timeout { get; set; } = 5000;

        [Parameter]
        public string? Color { get; set; }

        [Parameter]
        public StringNumber? Elevation { get; set; }

        [Parameter]
        public bool Tile { get; set; }

        [Parameter]
        public StringBoolean? Rounded { get; set; }

        [Parameter]
        public string? Action { get; set; }

        [Parameter]
        public RenderFragment? ActionContent { get; set; }

        private const string ROOT_CSS = "m-snack";
        internal const string ROOT_CSS_SELECTOR = $".{ROOT_CSS}";

        private Timer? Timer { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            Transition ??= "m-snack-transition";

            if (Value && Timeout > 0)
            {
                if (Timer == null)
                {
                    Timer = new Timer(Timeout);
                    Timer.Elapsed += Timer_Elapsed;
                }

                Timer.Enabled = true;
            }
        }

        protected override void SetComponentClass()
        {
            var rootCss = ROOT_CSS;
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(rootCss)
                        .AddIf($"{rootCss}--absolute", () => Absolute)
                        .AddIf($"{rootCss}--active", () => Value)
                        .AddIf($"{rootCss}--bottom", () => Bottom || !Top)
                        .AddIf($"{rootCss}--centered", () => Centered)
                        .AddIf($"{rootCss}--has-background", () => !Text && !Outlined)
                        .AddIf($"{rootCss}--left", () => Left)
                        .AddIf($"{rootCss}--multi-line", () => MultiLine && !Vertical)
                        .AddIf($"{rootCss}--right", () => Right)
                        .AddIf($"{rootCss}--text", () => Text)
                        .AddIf($"{rootCss}--top", () => Top)
                        .AddIf($"{rootCss}--vertical", () => Vertical);
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add("padding-bottom: 0px")
                        .Add("padding-top: 64px");
                })
                .Apply("wrap", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{rootCss}__wrapper")
                        .Add("m-sheet")
                        .AddIf("m-sheet--outlined", () => Outlined)
                        .AddIf("m-sheet--shaped", () => Shaped)
                        .AddBackgroundColor(Color, () => !Text && !Outlined)
                        .AddTextColor(Color, () => Text || Outlined)
                        .AddRounded(Rounded, Tile)
                        .AddElevation(Elevation)
                        .AddTheme(IsDark);
                }, styleBuilder =>
                {
                    styleBuilder.AddBackgroundColor(Color);
                    styleBuilder.AddTextColor(Color, () => Text || Outlined);
                })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{rootCss}__content")
                        .Add(ContentClass);
                })
                .Apply("action", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{rootCss}__action");
                });

            AbstractProvider
                .ApplySnackbarDefault()
                .Apply<BButton, MButton>(attrs =>
                {
                    attrs[nameof(Class)] = "m-snack__btn";
                    attrs[nameof(MButton.Text)] = true;
                    attrs[nameof(MButton.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, async () =>
                    {
                        Value = false;
                        Timer?.Stop();
                        if (ValueChanged.HasDelegate)
                        {
                            await ValueChanged.InvokeAsync(Value);
                        }

                        if (OnClosed.HasDelegate)
                        {
                            await OnClosed.InvokeAsync();
                        }
                    });
                });
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            Value = false;
            if (ValueChanged.HasDelegate)
            {
                InvokeAsync(() => ValueChanged.InvokeAsync(Value));
            }

            if (OnClosed.HasDelegate)
            {
                InvokeAsync(() => OnClosed.InvokeAsync());
            }

            Timer!.Enabled = false;
            InvokeStateHasChanged();
        }
    }
}
