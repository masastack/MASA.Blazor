using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using Element = BlazorComponent.Web.Element;

namespace Masa.Blazor
{
    public partial class MMenu : BMenu, IThemeable
    {
        public override string AttachedSelector => Attach ?? ".m-application";

        protected override void SetComponentClass()
        {
            Transition ??= "m-menu-transition";
            Origin ??= "top left";

            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-menu");
                })
                .Apply("content", cssBuilder =>
                {
                    cssBuilder.Add("m-menu__content")
                        .AddIf("m-menu__content--auto", () => Auto)
                        .AddIf("m-menu__content--fixed", () => ActivatorFixed)
                        .AddIf("menuable__content__active", () => IsActive)
                        .AddRounded(Tile ? "0" : Rounded)
                        .Add(ContentClass)
                        .AddTheme(IsDark);
                }, styleBuilder =>
                {
                    styleBuilder
                        .AddIf($"max-height:{CalculatedMaxHeight}", () => CalculatedMaxHeight != null)
                        .AddIf($"min-width:{CalculatedMinWidth}", () => CalculatedMinWidth != null)
                        .AddIf($"max-width:{CalculatedMaxWidth}", () => CalculatedMaxWidth != null)
                        .Add($"top:{CalculatedTop}")
                        .Add($"left:{CalculatedLeft}")
                        .Add($"transform-origin:{Origin}")
                        .Add($"z-index:{InternalZIndex}")
                        .Add(ContentStyle);
                });
        }

        protected override Task MoveContentToAsync()
        {
            return JsInvokeAsync(JsInteropConstants.AddElementTo, ContentRef, AttachedSelector);
        }
    }
}