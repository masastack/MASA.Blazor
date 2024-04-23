namespace Masa.Blazor.Presets.PageStack;

public class StackNavigationManager : NavigationManager
{
    private readonly IJSRuntime _jsRuntime;

    public StackNavigationManager(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public void Push(string uri)
    {
        NavigateTo(uri);
    }

    public void Pop()
    {
        _ = _jsRuntime.InvokeVoidAsync(JsInteropConstants.HistoryBack);
    }

    public void Replace(string uri)
    {
        NavigateTo(uri, replace: true);
    }
}