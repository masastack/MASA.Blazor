using BlazorComponent;

namespace Microsoft.JSInterop;

public static class JsRuntimeExtensions
{
    public static async Task ScrollToHash(this IJSRuntime js, string hash)
    {
        var element = await js.InvokeAsync<BlazorComponent.Web.Element>(JsInteropConstants.GetDomInfo, hash);

        var options = new
        {
            Top = element.AbsoluteTop - 80,
            Left = 0,
            Behavior = "smooth"
        };

        await js.InvokeVoidAsync("window.scrollTo", options);
    }
}