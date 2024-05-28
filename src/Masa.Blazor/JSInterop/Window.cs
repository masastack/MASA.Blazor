using System.Text.Json.Serialization;

namespace BlazorComponent.Web
{
    public class Window : JSObject, IAsyncDisposable
    {
        public Window()
        {
        }

        public Window(IJSRuntime jsRuntime)
            : base(jsRuntime)
        {
            Selector = "window";
        }

        [JsonPropertyName("innerHeight")]
        public double InnerHeight { get; set; }

        [JsonPropertyName("innerWidth")]
        public double InnerWidth { get; set; }

        [JsonPropertyName("pageXOffset")]
        public double PageXOffset { get; set; }

        [JsonPropertyName("pageYOffset")]
        public double PageYOffset { get; set; }

        public bool IsTop { get; set; }

        public bool IsBottom { get; set; }

        internal event Func<Task>? OnResize;

        public async Task AddEventListenerAsync(string type, Func<Task> listener, OneOf<EventListenerOptions, bool> options,
            EventListenerExtras? extras = null)
        {
            await JS.InvokeVoidAsync(
                JsInteropConstants.AddHtmlElementEventListener,
                Selector,
                type,
                DotNetObjectReference.Create(new Invoker<object>(_ => { listener?.Invoke(); })),
                options.Value,
                extras);
        }

        public async Task AddResizeEventListenerAsync()
        {
            await AddEventListenerAsync("resize", HandleOnResizeAsync, false);
        }

        private async Task HandleOnResizeAsync()
        {
            if (OnResize != null)
            {
                await OnResize.Invoke();
            }
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            try
            {
                await JS.InvokeVoidAsync(JsInteropConstants.RemoveHtmlElementEventListener, Selector, "resize");
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
