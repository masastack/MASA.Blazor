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
    public partial class MAlert : BAlert, IThemeable
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
        public StringNumber Elevation { get; set; }

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

        public string ComputedColor => Color ?? Type?.ToString().ToLower();

        protected override Task OnInitializedAsync()
        {
            string originClass = "m-alert__icon";

            if (string.IsNullOrWhiteSpace(Icon))
            {
                if (Type.HasValue)
                {
                    var type = Type.ToString().ToLower();
                    Color = type;

                    IconContent = RenderIcon(_typeIconDict[type], originClass, ColoredBorder || Text || Outlined ? Color : "", dark: true);
                }
            }
            else
            {
                IconContent = RenderIcon(Icon, originClass, ColoredBorder || Text || Outlined ? ComputedColor : "", dark: true);
            }

            if (Dismissible)
            {
                DismissibleButtonContent = RenderButton(
                    (s) => RenderIcon("mdi-close-circle", sequence: s, color: Color),
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
            CssProvider
                .AsProvider<BAlert>()
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-alert")
                        .Add("m-sheet")
                        .AddTheme(IsDark, Type.HasValue && !ColoredBorder)
                        .AddElevation(Elevation)
                        .AddFirstIf(
                            (() => "m-alert--prominent", () => Prominent),
                            (() => "m-alert--dense", () => Dense))
                        .AddIf("m-alert--text", () => Text)
                        .AddIf("m-alert--outlined", () => Outlined)
                        .AddColor(ComputedColor, Outlined || Text, () => !ColoredBorder);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf("display: none", () => !Visible)
                        .AddColor(ComputedColor, Outlined || Text, () => !ColoredBorder);
                })
                .Apply("wrap", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-alert__wrapper");
                })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-alert__content");
                })
                .Apply("border", cssBuilder =>
                {
                    cssBuilder
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
                        .AddIf(() => Type.Value.ToString().ToLower(), () => ColoredBorder && Type.HasValue)
                        .AddTextColor(Color, () => ColoredBorder);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddTextColor(Color, () => ColoredBorder);
                });


        }
    }
}
