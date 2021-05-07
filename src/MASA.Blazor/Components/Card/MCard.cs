using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent;
using Microsoft.AspNetCore.Components;

namespace MASA.Blazor
{
    public partial class MCard : BCard
    {
        /// <summary>
        /// Whether dark theme
        /// </summary>
        [Parameter]
        public bool Dark { get; set; }

        protected override void SetComponentClass()
        {
            CssProvider
                .AsProvider<BCard>()
                .Apply(css =>
                {
                    css.Add("m-card")
                        .Add("m-sheet--outlined")
                        .AddIf("elevation-2", () => !Outlined)
                        .AddTheme(Dark);
                }, style =>
                {
                    style.AddIf(() => $"max-height: {MaxHeight.TryGetNumber().number}px", () => MaxHeight != null)
                        .AddIf(() => $"min-height: {MinHeight.TryGetNumber().number}px", () => MinHeight != null)
                        .AddIf(() => $"height: {Height.TryGetNumber().number}px", () => Height != null)
                        .AddIf(() => $"max-width: {MaxWidth.TryGetNumber().number}px", () => MaxWidth != null)
                        .AddIf(() => $"min-width: {MinWidth.TryGetNumber().number}px", () => MinWidth != null)
                        .AddIf(() => $"width: {Width.TryGetNumber().number}px", () => Width != null);
                });
        }
    }
}