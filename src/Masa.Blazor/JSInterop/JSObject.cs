namespace BlazorComponent.Web
{
    public abstract class JSObject
    {
        public JSObject()
        {
        }
        
        public JSObject(IJSRuntime js)
        {
            JS = js;
        }

        public IJSRuntime JS { get; }

        public string? Selector { get; protected set; }
    }
}
