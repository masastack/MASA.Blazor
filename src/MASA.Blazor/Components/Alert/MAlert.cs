using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using BlazorComponent;
using MASA.Blazor.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OneOf;
using static MASA.Blazor.Helpers.RenderHelper;

namespace MASA.Blazor
{
    public class MAlert : BAlert, IThemeable
    {
        [Parameter]
        public StringBoolean Icon { get; set; }

        [Parameter]
        public bool ColoredBorder { get; set; }

        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public StringNumber Elevation { get; set; }

        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public bool Prominent { get; set; }

        [Parameter]
        public StringBoolean Rounded { get; set; }

        [Parameter]
        public bool Shaped { get; set; }

        [Parameter]
        public bool Text { get; set; }

        [Parameter]
        public bool Tile { get; set; }

        /// <summary>
        /// Sets the component transition. TODO: no implementation
        /// </summary>
        [Parameter]
        public string Transition { get; set; }

        [Parameter]
        public StringNumber Height { get; set; }

        [Parameter]
        public StringNumber MaxHeight { get; set; }

        [Parameter]
        public StringNumber MaxWidth { get; set; }

        [Parameter]
        public StringNumber MinHeight { get; set; }

        [Parameter]
        public StringNumber MinWidth { get; set; }

        [Parameter]
        public StringNumber Width { get; set; }

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

        private string ComputedType => Type != AlertTypes.None ? Type.ToString().ToLower() : "";

        private string ComputedColor => Color ?? ComputedType;

        private (bool, RenderFragment) ComputedIcon()
        {
            if (Icon != null && Icon.IsT1 && Icon.AsT1 == false) return (false, null);

            if (Icon != null && Icon.IsT0 && Icon.AsT0 != null)
                return (true, builder => builder.AddContent(0, Icon.AsT0));

            var iconText = Type switch
            {
                AlertTypes.Success => "mdi-checkbox-marked-circle-outline",
                AlertTypes.Error => "mdi-alert-circle-outline",
                AlertTypes.Info => "mdi-information",
                AlertTypes.Warning => "mdi-alert-outline",
                _ => null
            };

            if (iconText == null) return (false, null);

            return (true, builder => builder.AddContent(0, iconText));
        }

        private bool HasText => Text || Outlined;

        private bool HasColoredBorder => Border != Borders.None && ColoredBorder;

        private bool HasColoredIcon => HasText || HasColoredBorder;

        private bool HasTypedBorder => ColoredBorder && Type != AlertTypes.None;

        private string IconColor => HasColoredIcon ? ComputedColor : "";

        private bool IsDarkTheme => (Type != AlertTypes.None && !ColoredBorder && !Outlined) || IsDark;

        protected override void OnParametersSet()
        {
            (IsShowIcon, IconContent) = ComputedIcon();
        }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-alert")
                        .Add("m-sheet")
                        .AddIf("m-sheet--shaped", () => Shaped)
                        .AddTheme(IsDarkTheme)
                        .AddElevation(Elevation)
                        .AddFirstIf(
                            (() => "m-alert--prominent", () => Prominent),
                            (() => "m-alert--dense", () => Dense))
                        .AddIf("m-alert--text", () => Text)
                        .AddIf("m-alert--outlined", () => Outlined)
                        .AddColor(ComputedColor, HasText, () => !ColoredBorder)
                        .AddRounded(Rounded, Tile);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf("display:none", () => !Value)
                        .AddColor(ComputedColor, HasText, () => !ColoredBorder)
                        .AddHeight(Height)
                        .AddMaxHeight(MaxHeight)
                        .AddMinHeight(MinHeight)
                        .AddWidth(Width)
                        .AddMaxWidth(MaxWidth)
                        .AddMinWidth(MinWidth);
                })
                .Apply("wrapper", cssBuilder => { cssBuilder.Add("m-alert__wrapper"); })
                .Apply("content", cssBuilder => { cssBuilder.Add("m-alert__content"); })
                .Apply("border", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-alert__border")
                        .Add(() => Border switch
                        {
                            Borders.Left => "m-alert__border--left",
                            Borders.Right => "m-alert__border--right",
                            Borders.Top => "m-alert__border--top",
                            Borders.Bottom => "m-alert__border--bottom",
                            Borders.None => "",
                            _ => throw new ArgumentOutOfRangeException(nameof(Border))
                        })
                        .AddIf("m-alert__border--has-color", () => ColoredBorder)
                        .AddIf(() => Type.ToString().ToLower(), () => HasTypedBorder)
                        .AddTextColor(Color, () => ColoredBorder);
                }, styleBuilder => { styleBuilder.AddTextColor(Color, () => ColoredBorder); });

            AbstractProvider
                .Apply(typeof(BAlertWrapper<>), typeof(BAlertWrapper<MAlert>))
                .Apply(typeof(BAlertIcon<>), typeof(BAlertIcon<MAlert>))
                .Apply<BIcon, MAlertIcon>(props =>
                {
                    props[nameof(MAlertIcon.Color)] = IconColor;
                    props[nameof(MAlertIcon.Dark)] = IsDarkTheme;
                })
                .Apply(typeof(BAlertContent<>), typeof(BAlertContent<MAlert>))
                .Apply(typeof(BAlertDismissButton<>), typeof(BAlertDismissButton<MAlert>))
                .Apply<BButton, MAlertDismissButton>("dismissible", props =>
                {
                    props[nameof(MAlertDismissButton.Color)] = IconColor;
                    props[nameof(MAlertDismissButton.Dark)] = IsDarkTheme;
                })
                .Apply<BIcon, MIcon>("dismissible", props => { props[nameof(MIcon.Dark)] = IsDarkTheme; });
        }
    }
}