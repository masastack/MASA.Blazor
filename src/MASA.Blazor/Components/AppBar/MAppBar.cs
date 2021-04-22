using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;
using OneOf;

namespace MASA.Blazor
{
    using StringNumber = OneOf<string, int>;

    public partial class MAppBar : BAppBar
    {
        protected bool IsExtended { get; set; }

        [Parameter]
        public bool Absolute { get; set; }
        [Parameter]
        public bool Bottom { get; set; }
        [Parameter]
        public bool Collapse { get; set; }
        [Parameter]
        public bool Dense { get; set; }
        [Parameter]
        public bool Flat { get; set; }
        [Parameter]
        public bool Floating { get; set; }
        [Parameter]
        public bool Prominent { get; set; }
        [Parameter]
        public bool Dark { get; set; }
        [Parameter]
        public bool App { get; set; }
        [Parameter]
        public bool Fixed { get; set; }

        /// <summary>
        /// Sets the height of the navigation drawer
        /// </summary>
        [Parameter]
        public StringNumber Height { get; set; } = "56px";

        [Parameter]
        public string MarginTop { get; set; }

        /// <summary>
        /// This should be down in next version
        /// </summary>
        public int? MaxHeight { get; }

        public int? Transform { get; } = 0;

        /// <summary>
        /// Sets the width for the component.
        /// </summary>
        [Parameter]
        public StringNumber Width { get; set; }

        [Parameter]
        public StringNumber Left { get; set; } = 0;

        [Parameter]
        public StringNumber Right { get; set; } = 0;

        public override void SetComponentClass()
        {
            var prefix = "m-toolbar";

            CssBuilder
                .Add("m-toolbar")
                .AddIf($"{prefix}--absolute", () => Absolute)
                .AddIf($"{prefix}--bottom", () => Bottom)
                .AddIf($"{prefix}--collapse", () => Collapse)
                .AddIf($"{prefix}--collapsed", () => Collapse)
                .AddIf($"{prefix}--dense", () => Dense)
                .AddIf($"{prefix}--extended", () => IsExtended)
                .AddIf($"{prefix}--flat", () => Flat)
                .AddIf($"{prefix}--floating", () => Floating)
                .AddIf($"{prefix}--prominent", () => Prominent)
                .Add("m-sheet")
                .AddTheme(Dark)
                .Add("m-app-bar")
                .AddIf("m-app-bar--fixed", () => !Absolute && (App || Fixed));

            StyleBuilder
                .Clear()
                .AddIf(() => $"height:{Height.Value}", () => Height.Value != null)
                .AddIf(() => $"margin-top:{MarginTop}", () => MarginTop != null)
                .AddIf(() => $"transform:translateY({Transform}px)", () => Transform != null)
                .AddIf(() => $"left:{Left.Value}", () => Left.Value != null)
                .AddIf(() => $"right:{Right.Value}", () => Right.Value != null);

            ContentCssBuilder
                .Clear()
                .Add("m-toolbar__content");

            ContentStyleBuilder
                .Clear()
                .Add($"height:{Height.Value}");

            Attributes.Add("data-booted", true);
        }
    }
}
