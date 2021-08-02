using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using OneOf;

namespace MASA.Blazor
{
    public partial class MSnackbar : BSnackbar, IThemeable
    {
        private bool _isActive;

        [Parameter]
        public bool Absolute { get; set; }

        [Parameter]
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;

                if (_isActive && Timeout > 0)
                {
                    if (Timer == null)
                    {
                        Timer = new Timer(Timeout);
                        Timer.Elapsed += Timer_Elapsed;
                    }

                    Timer.Enabled = true;
                }
            }
        }

        [Parameter]
        public EventCallback<bool> IsActiveChanged { get; set; }

        [Parameter]
        public EventCallback OnClosed { get; set; }

        [Parameter]
        public bool Bottom { get; set; }

        [Parameter]
        public bool Centered { get; set; }

        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public bool Left { get; set; }

        [Parameter]
        public bool MultiLine { get; set; }

        [Parameter]
        public bool Right { get; set; }

        [Parameter]
        public bool Text { get; set; }

        [Parameter]
        public bool Top { get; set; }

        [Parameter]
        public bool Vertical { get; set; }

        [Parameter]
        public bool Shaped { get; set; }

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
        public int Timeout { get; set; } = 5000;

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public StringNumber Elevation { get; set; }

        [Parameter]
        public bool Tile { get; set; }

        [Parameter]
        public StringBoolean Rounded { get; set; }

        protected Timer Timer { get; set; }

        protected override void SetComponentClass()
        {
            var prefix = "m-snack";
            CssProvider
                .AsProvider<BSnackbar>()
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add(prefix)
                        .AddIf($"{prefix}--absolute", () => Absolute)
                        .AddIf($"{prefix}--active", () => IsActive)
                        .AddIf($"{prefix}--bottom", () => Bottom || !Top)
                        .AddIf($"{prefix}--centered", () => Centered)
                        .AddIf($"{prefix}--has-background", () => !Text && !Outlined)
                        .AddIf($"{prefix}--left", () => Left)
                        .AddIf($"{prefix}--multi-line", () => MultiLine && !Vertical)
                        .AddIf($"{prefix}--right", () => Right)
                        .AddIf($"{prefix}--text", () => Text)
                        .AddIf($"{prefix}--top", () => Top)
                        .AddIf($"{prefix}--vertical", () => Vertical);
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add("padding-bottom: 0px")
                        .Add("padding-top: 64px");
                })
                .Apply("wrap", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__wrapper")
                        .Add("m-sheet")
                        .AddIf("m-sheet--outlined", () => Outlined)
                        .AddIf("m-sheet--shaped", () => Shaped)
                        .AddBackgroundColor(Color, () => !Text && !Outlined)
                        .AddTextColor(Color, () => Text || Outlined)
                        .AddRounded(Tile, Rounded)
                        .AddElevation(Elevation)
                        .AddTheme(IsDark);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf("display:none", () => !IsActive);
                })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__content");
                })
                .Apply("action", cssBuilder =>
                {
                    cssBuilder
                        .Add($"{prefix}__action");
                });

            AbstractProvider
                .Apply<BButton, MSnackbarButton>(props =>
                {
                    props[nameof(MSnackbarButton.Text)] = true;
                    props[nameof(MSnackbarButton.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, async () =>
                    {
                        IsActive = false;
                        Timer.Stop();
                        if (IsActiveChanged.HasDelegate)
                        {
                            await IsActiveChanged.InvokeAsync(_isActive);
                        }
                        if (OnClosed.HasDelegate)
                        {
                            await OnClosed.InvokeAsync();
                        }
                    });
                });
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            IsActive = false;
            if (IsActiveChanged.HasDelegate)
            {
                InvokeAsync(() => IsActiveChanged.InvokeAsync(_isActive));
            }
            if (OnClosed.HasDelegate)
            {
                InvokeAsync(() => OnClosed.InvokeAsync());
            }

            Timer.Enabled = false;
            InvokeStateHasChanged();
        }
    }
}
