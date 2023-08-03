namespace Masa.Blazor;

public class MonacoEditorJSModule : JSModule
{
    public MonacoEditorJSModule(IJSRuntime js) : base(js, "./_content/Masa.Blazor/js/proxies/monaco-editor-proxy.js")
    {
    }

    public async ValueTask<IJSObjectReference> Init<T>(string id, object options, DotNetObjectReference<T> dotNetObjectReference) where T : class
    {
        return await InvokeAsync<IJSObjectReference>("init", id, options, dotNetObjectReference);
    }

    public async ValueTask ColorizeElement(string id, object options)
    {
        await InvokeVoidAsync("colorizeElement", id, options);
    }

    public async ValueTask AddCommand<T>(IJSObjectReference id, int keybinding, DotNetObjectReference<T> dotNetObjectReference, string method) where T : class
    {
        await InvokeVoidAsync("addCommand", id, keybinding, dotNetObjectReference, method);
    }

    public async Task UpdateOptions(IJSObjectReference id, object options)
    {
        await InvokeVoidAsync("updateOptions", id, options);
    }

    public async ValueTask DefineTheme(string name, StandaloneThemeData themeData)
    {
        await InvokeVoidAsync("defineTheme", name, themeData);
    }

    public async Task<string> GetValue(IJSObjectReference id)
    {
        return await InvokeAsync<string>("getValue", id);
    }

    public async Task SetValue(IJSObjectReference id, string? value)
    {
        await InvokeVoidAsync("setValue", id, value);
    }

    public async Task SetTheme(string theme)
    {
        await InvokeVoidAsync("setTheme", theme);
    }

    public async Task<TextModelOptions[]> GetModels()
    {
        return await InvokeAsync<TextModelOptions[]>("getModels");
    }

    public async Task<TextModelOptions> GetModel(IJSObjectReference id)
    {
        return await InvokeAsync<TextModelOptions>("getModel", id);
    }

    public async Task SetModelLanguage(IJSObjectReference id, string languageId)
    {
        await InvokeVoidAsync("setModelLanguage", id, languageId);
    }

    public async Task RemeasureFonts()
    {
        await InvokeVoidAsync("remeasureFonts");
    }

    public async Task AddKeybindingRules(KeybindingRule[] rules)
    {
        await InvokeVoidAsync("addKeybindingRules", rules);
    }

    public async Task AddKeybindingRule(KeybindingRule rule)
    {
        await InvokeVoidAsync("addKeybindingRule", rule);
    }
}