using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace MASA.Blazor
{
    public partial class MBanner : BBanner, IBanner
    {
        protected bool IsSticky => Sticky || App;

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
        public bool App { get; set; }

        [Parameter]
        public string Icon { get; set; }

        [Parameter]
        public string IconColor { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public StringNumber Elevation { get; set; } = "0";

        [Parameter]
        public bool SingleLine { get; set; }

        [Parameter]
        public bool Sticky { get; set; }

        [Parameter]
        public RenderFragment IconContent { get; set; }

        [Parameter]
        public RenderFragment<Action> ActionsContent { get; set; }

        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnIconClick { get; set; }

        /// <summary>
        /// This should be down in next version
        /// </summary>
        public bool Mobile { get; }

        public bool HasIcon => !string.IsNullOrWhiteSpace(Icon) || IconContent != null;

        public RenderFragment ComputedActionsContent => ActionsContent == null ? null : ActionsContent(() => { Value = false; ValueChanged.InvokeAsync(Value); });

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-banner")
                        .Add("m-sheet")
                        .AddIf($"elevation-{Elevation.ToInt32()}", () => Elevation.ToInt32() > 0)
                        .AddIf("m-banner--has-icon", () => HasIcon)
                        .AddIf("m-banner--is-mobile", () => Mobile)
                        .AddIf("m-banner--single-line", () => SingleLine)
                        .AddIf("m-banner--sticky", () => IsSticky)
                        .AddBackgroundColor(Color)
                        .AddTheme(IsDark);
                }, styleBuilder =>
                {
                    styleBuilder
                        .Add($"top:0")
                        .AddIf(() => $"position:sticky", () => IsSticky)
                        .AddIf(() => $"zIndex:1", () => IsSticky);
                })
                .Apply("wrapper", cssAction: cssBuilder =>
                {
                    cssBuilder.Add("m-banner__wrapper");
                })
                .Apply("content", cssAction: cssBuilder =>
                {
                    cssBuilder.Add("m-banner__content");
                })
                .Apply("text", cssAction: cssBuilder =>
                {
                    cssBuilder.Add("m-banner__text");
                })
                .Apply("actions", cssAction: cssBuilder =>
                {
                    cssBuilder
                        .Add("m-banner__actions");
                });

            AbstractProvider
                .ApplyBannerDefault()
                .Apply<BAvatar, MAvatar>(props =>
                {
                    props[nameof(Class)] = "m-banner__icon";
                    props[nameof(MAvatar.Color)] = Color;
                    props[nameof(MAvatar.Size)] = (StringNumber)40;
                    props["onclick"] = EventCallback.Factory.Create<MouseEventArgs>(this, HandleOnIconClickAsync);
                })
                .Apply<BIcon, MIcon>(props =>
                {
                    props[nameof(MIcon.Size)] = (StringNumber)28;
                    props[nameof(MIcon.Color)] = IconColor;
                });
        }

        protected virtual async Task HandleOnIconClickAsync(MouseEventArgs args)
        {
            if (OnIconClick.HasDelegate)
            {
                await OnIconClick.InvokeAsync(args);
            }
        }
    }
}
