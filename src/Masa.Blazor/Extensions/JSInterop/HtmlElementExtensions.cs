using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace BlazorComponent.Web
{
    public static class HtmlElementExtensions
    {
        internal static async Task<double?> GetScrollHeightWithoutHeight(this HtmlElement htmlElement)
        {
            var jsonElement = await htmlElement.JS.InvokeAsync<JsonElement>(JsInteropConstants.ScrollHeightWithoutHeight, htmlElement.Selector);
            return jsonElement.ValueKind == JsonValueKind.Number ? jsonElement.GetDouble() : null;
        }

        internal static async Task UpdateWindowTransitionAsync(this HtmlElement htmlElement, bool isActive, ElementReference? item = null)
        {
            await htmlElement.JS.InvokeVoidAsync(JsInteropConstants.UpdateWindowTransition, htmlElement.Selector, isActive, item);
        }
    }
}
