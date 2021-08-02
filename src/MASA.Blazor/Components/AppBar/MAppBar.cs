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
    public partial class MAppBar : MToolbar
    {
        [Parameter]
        public bool App { get; set; }

        [Parameter]
        public bool Fixed { get; set; }

        [Parameter]
        public StringNumber MarginTop { get; set; }

        public int? Transform { get; } = 0;

        [Parameter]
        public StringNumber Left { get; set; } = 0;

        [Parameter]
        public StringNumber Right { get; set; } = 0;

        [Parameter]
        public bool ClippedLeft { get; set; }

        [Parameter]
        public bool ClippedRight { get; set; }

        protected override void SetComponentClass()
        {
            base.SetComponentClass();

            CssProvider
                .Merge(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-app-bar")
                        .AddIf("m-app-bar--clipped", () => ClippedLeft || ClippedRight)
                        .AddIf("m-app-bar--fixed", () => !Absolute && (App || Fixed));
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf(() => $"height:{Height.ToUnit()}", () => Height != null)
                        .AddIf(() => $"margin-top:{MarginTop.ToUnit()}", () => MarginTop != null)
                        .AddIf(() => $"transform:translateY({Transform}px)", () => Transform != null)
                        .AddIf(() => $"left:{Left.ToUnit()}", () => Left != null)
                        .AddIf(() => $"right:{Right.ToUnit()}", () => Right != null);
                });

            Attributes.Add("data-booted", true);
        }
    }
}
