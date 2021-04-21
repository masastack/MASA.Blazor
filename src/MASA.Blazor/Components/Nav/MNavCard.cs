using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;
namespace MASA.Blazor
{
    public partial class MNavCard : BNavCard
    {
        [Parameter]
        public bool Outlined { get; set; }
        [Parameter]
        public bool Shaped { get; set; }
        [Parameter]
        public bool Dark { get; set; }
        [Parameter]
        public bool Flat { get; set; }
        [Parameter]
        public bool Hover { get; set; }
        [Parameter]
        public bool Link { get; set; }
        [Parameter]
        public bool Loading { get; set; }
        [Parameter]
        public bool Disabled { get; set; }
        [Parameter]
        public bool Raised { get; set; }

        /// <summary>
        /// Sets the height for the component.
        /// </summary>
        [Parameter]
        public int? Height { get; set; }

        /// <summary>
        /// Sets the maximum height for the component.
        /// </summary>
        [Parameter]
        public int? MinHeight { get; set; }

        /// <summary>
        /// Sets the minimum width for the component.
        /// </summary>
        [Parameter]
        public int? MinWidth { get; set; }

        /// <summary>
        /// Sets the maximum height for the component.
        /// </summary>
        [Parameter]
        public int? MaxHeight { get; set; }

        /// <summary>
        /// Sets the maximum width for the component.
        /// </summary>
        [Parameter]
        public int? MaxWidth { get; set; }

        /// <summary>
        /// Sets the width for the component.
        /// </summary>
        [Parameter]
        public int? Width { get; set; }

        public override void SetComponentClass()
        {
            var prefix = "m-card";
            CssBuilder
                .Add("m-sheet")
                .AddIf("m-sheet--outlined", () => Outlined)
                .AddIf("m-sheet--shaped", () => Shaped)
                .AddTheme(Dark)
                .Add("m-card")
                .AddIf($"{prefix}--flat", () => Flat)
                .AddIf($"{prefix}--hover", () => Hover)
                .AddIf($"{prefix}--link", () => Link)
                .AddIf($"{prefix}--loading", () => Loading)
                .AddIf($"{prefix}--disabled", () => Disabled)
                .AddIf($"{prefix}--raised", () => Raised);

            StyleBuilder
                .AddIf(() => $"height:{Height.Value}px", () => Height != default)
                .AddIf(() => $"minHeight:{MinHeight.Value}px", () => MinHeight != default)
                .AddIf(() => $"minWidth:{MinWidth.Value}px", () => MinWidth != default)
                .AddIf(() => $"maxHeight:{MaxHeight.Value}px", () => MaxHeight != default)
                .AddIf(() => $"maxWidth:{MaxWidth.Value}px", () => MaxWidth != default)
                .AddIf(() => $"width:{Width.Value}px", () => Width != default);
        }
    }
}
