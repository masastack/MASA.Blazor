using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OneOf;

namespace MASA.Blazor
{
    using StringNumber = OneOf<string, int>;

    public partial class MAlert : BAlert
    {
        #region SVGs

        private const string WARNING_SVG = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\" role=\"img\" aria-hidden=\"true\" class=\"m-icon__svg\"><path d=\"M12,2L1,21H23M12,6L19.53,19H4.47M11,10V14H13V10M11,16V18H13V16\"></path></svg>";
        private const string INFO_SVG = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\" role=\"img\" aria-hidden=\"true\" class=\"m-icon__svg\"><path d=\"M11,9H13V7H11M12,20C7.59,20 4,16.41 4,12C4,7.59 7.59,4 12,4C16.41,4 20,7.59 20,12C20,16.41 16.41,20 12,20M12,2A10,10 0 0,0 2,12A10,10 0 0,0 12,22A10,10 0 0,0 22,12A10,10 0 0,0 12,2M11,17H13V11H11V17Z\"></path></svg>";
        private const string ERROR_SVG = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\" role=\"img\" aria-hidden=\"true\" class=\"m-icon__svg\"><path d=\"M8.27,3L3,8.27V15.73L8.27,21H15.73C17.5,19.24 21,15.73 21,15.73V8.27L15.73,3M9.1,5H14.9L19,9.1V14.9L14.9,19H9.1L5,14.9V9.1M11,15H13V17H11V15M11,7H13V13H11V7\"></path></svg>";
        private const string SUCCESS_SVG = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\" role=\"img\" aria-hidden=\"true\" class=\"m-icon__svg\"><path d=\"M12,2C17.52,2 22,6.48 22,12C22,17.52 17.52,22 12,22C6.48,22 2,17.52 2,12C2,6.48 6.48,2 12,2M11,16.5L18,9.5L16.59,8.09L11,13.67L7.91,10.59L6.5,12L11,16.5Z\"></path></svg>";
        private const string DISMISSIBLE_SVG = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\" role=\"img\" aria-hidden=\"true\" class=\"m-icon__svg\"><path d=\"M12,2C17.53,2 22,6.47 22,12C22,17.53 17.53,22 12,22C6.47,22 2,17.53 2,12C2,6.47 6.47,2 12,2M15.59,7L12,10.59L8.41,7L7,8.41L10.59,12L7,15.59L8.41,17L12,13.41L15.59,17L17,15.59L13.41,12L17,8.41L15.59,7Z\"></path></svg>";

        #endregion

        [Parameter]
        public AlertType? Type { get; set; }

        [Parameter]
        public StringNumber? Elevation { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        protected override Task OnInitializedAsync()
        {
            string type = null;
            string originClass ="m-alert__icon";

            if (string.IsNullOrWhiteSpace(Icon))
            {
                if (Type.HasValue)
                {
                    switch (Type.Value)
                    {
                        case AlertType.Success:
                            type = "success";
                            IconContent = RenderIcon(SUCCESS_SVG, type, ColoredBorder, originClass);
                            break;
                        case AlertType.Info:
                            type = "info";
                            IconContent = RenderIcon(INFO_SVG, type, ColoredBorder, originClass);
                            break;
                        case AlertType.Warning:
                            type = "warning";
                            IconContent = RenderIcon(WARNING_SVG, type, ColoredBorder, originClass);
                            break;
                        case AlertType.Error:
                            type = "error";
                            IconContent = RenderIcon(ERROR_SVG, type, ColoredBorder, originClass);
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                IconContent = RenderIcon(Icon, type, ColoredBorder, originClass);
            }

            if (Dismissible)
            {
                DismissibleButtonContent = RenderDismissibleButton(type, ColoredBorder);
            }

            return base.OnInitializedAsync();
        }

        private RenderFragment RenderIcon(string icon, string type, bool coloredBorder, string originClass, int sequence = 0) => builder =>
        {
            builder.OpenComponent(sequence++, typeof(MIcon));

            var @class = originClass;
            if (coloredBorder)
                @class += $" {type}--text";
            builder.AddAttribute(sequence++, "Class", @class);

            builder.AddAttribute(sequence++, "Dark", true);

            builder.AddAttribute(sequence++, "ChildContent", (RenderFragment)((builder) => builder.AddContent(sequence++, icon)));

            builder.CloseComponent();
        };

        private RenderFragment RenderDismissibleButton(string type, bool coloredBorder) => builder =>
        {
            var sequence = 0;

            builder.OpenComponent(sequence++, typeof(MButton));

            builder.AddAttribute(sequence++, nameof(MButton.Class), "m-alert__dismissble");
            builder.AddAttribute(sequence++, nameof(MButton.Icon), true);
            builder.AddAttribute(sequence++, nameof(MButton.Small), true);
            builder.AddAttribute(sequence++, nameof(MButton.Dark), true);

            builder.AddAttribute(sequence++, nameof(MButton.Click), EventCallback.Factory.Create<MouseEventArgs>(this, async (args) =>
            {
                Visible = false;
                await VisibleChanged.InvokeAsync(false);
            }));

            builder.AddAttribute(sequence++, nameof(MButton.ChildContent), RenderIcon(DISMISSIBLE_SVG, type, coloredBorder, "", sequence++));

            builder.CloseComponent();
        };

        public override void SetComponentClass()
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
                .AddIf(() => Type.Value switch
                {
                    AlertType.Info => "info",
                    AlertType.Success => "success",
                    AlertType.Warning => "warning",
                    AlertType.Error => "error",
                    _ => ""
                }, () => !ColoredBorder && Type.HasValue)
                .AddIf("m-alert--text", () => Text);

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
                .AddIf(() => Type.Value switch
                {
                    AlertType.Info => "info",
                    AlertType.Success => "success",
                    AlertType.Warning => "warning",
                    AlertType.Error => "error",
                    _ => ""
                }, () => ColoredBorder && Type.HasValue);

            // 渲染颜色和变体
            if (!string.IsNullOrWhiteSpace(Color))
            {
                if (ColoredBorder)
                {
                    var color_variant = Color.Split(" ");
                    var color = color_variant[0];
                    if (!string.IsNullOrWhiteSpace(color))
                    {
                        if (color.StartsWith("#"))
                        {
                            BorderStyleBuilder.Add($"color: {color}");
                        }
                        else
                        {
                            BorderCssBuilder.Add($"{color}--text");
                        }

                        if (color_variant.Length == 2)
                        {
                            var variant = color_variant[1];
                            // TODO: 是否需要正则表达式验证格式，Vuetify没有
                            // {darken|lighten|accent}-{1|2}

                            BorderCssBuilder.AddIf($"text--{variant}", () => !string.IsNullOrWhiteSpace(variant));
                        }
                    }
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
