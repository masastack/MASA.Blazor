using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorComponent;
using MASA.Blazor.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OneOf;
using static MASA.Blazor.Helpers.RenderHelper;

namespace MASA.Blazor
{
    using StringNumber = OneOf<string, int>;

    public partial class MAlert : BAlert
    {
        private static readonly Dictionary<string, string> _typeIconDict = new()
        {
            { "success", "mdi-check-circle" },
            { "error", "mdi-alert" },
            { "info", "mdi-information" },
            { "warning", "mdi-exclamation" },
        };

        [Parameter]
        public AlertType? Type { get; set; }

        [Parameter]
        public StringNumber? Elevation { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        protected override Task OnInitializedAsync()
        {
            string originClass = "m-alert__icon";

            if (string.IsNullOrWhiteSpace(Icon))
            {
                if (Type.HasValue)
                {
                    var type = Type.ToString().ToLower();
                    Color = type;

                    IconContent = RenderIcon(_typeIconDict[type], originClass, ColoredBorder || Text || Outlined ? Color : "", dark: true); ;
                }
            }
            else
            {
                IconContent = RenderIcon(Icon, originClass, ColoredBorder || Text || Outlined ? Color : "", dark: true);
            }

            if (Dismissible)
            {
                DismissibleButtonContent = RenderButton(
                    (s) => RenderIcon("mdi-close-circle", sequence: s),
                    EventCallback.Factory.Create<MouseEventArgs>(this, async (args) =>
                    {
                        await VisibleChanged.InvokeAsync(false);
                    }),
                    "m-alert__dismissble", true, true, true);
            }

            return base.OnInitializedAsync();
        }

        protected override void SetComponentClass()
        {
            CssBuilder
                .Add("m-alert")
                .Add("m-sheet")
                .AddTheme(Dark || (Type.HasValue && !ColoredBorder))
                .AddIf(() => Elevation.Value.Match(
                    str => $"elevation-{str}",
                    num => $"elevation-{num}"
                    ), () => Elevation.HasValue)
                .AddFirstIf(
                    (() => "m-alert--prominent", () => Prominent),
                    (() => "m-alert--dense", () => Dense))
                .AddIf("m-alert--text", () => Text)
                .AddIf("m-alert--outlined", () => Outlined);

            StyleBuilder
                .AddIf("display: none", () => !Visible);

            WrapperCssBuilder
                .Add("m-alert__wrapper");

            ContentCssBuilder
                .Add("m-alert__content");

            BorderCssBuilder
                .Add("m-alert__border")
                .AddIf(() => Border.Value switch
                {
                    AlertBorder.Left => "m-alert__border--left",
                    AlertBorder.Right => "m-alert__border--right",
                    AlertBorder.Top => "m-alert__border--top",
                    AlertBorder.Bottom => "m-alert__border--bottom",
                    _ => "",
                }, () => Border.HasValue)
                .AddIf("m-alert__border--has-color", () => ColoredBorder)
                .AddIf(() => Type.Value.ToString().ToLower(), () => ColoredBorder && Type.HasValue);

            // 渲染颜色和变体
            if (!string.IsNullOrWhiteSpace(Color))
            {
                if (ColoredBorder)
                {
                    var (@class, style) = ColorHelper.ToCss(Color);
                    BorderCssBuilder.Add(@class);
                    BorderStyleBuilder.Add(style);
                }
                else
                {
                    if (Color.StartsWith("#"))
                    {
                        if (Outlined || Text)
                        {
                            StyleBuilder.Add($"color:{Color}");
                        }
                        else
                        {
                            StyleBuilder.Add($"background-color: {Color}");
                        }
                    }
                    else
                    {
                        if (Outlined || Text)
                        {
                            CssBuilder.Add($"{Color}--text");
                        }
                        else
                        {
                            CssBuilder.Add(Color);
                        }
                    }
                }
            }
        }
    }
}
