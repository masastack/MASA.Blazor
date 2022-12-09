namespace Masa.Blazor;

public class MonacoEditorJSModule : JSModule
{
    public MonacoEditorJSModule(IJSRuntime js) : base(js, "./_content/Masa.Blazor/js/proxies/monaco-editor-proxy.js")
    {
    }

    public async ValueTask<IJSObjectReference> Init(string id, EditorOptions options)
        => await InvokeAsync<IJSObjectReference>("init", id, options);

    public async ValueTask<string> GetValue(IJSObjectReference id)
        => await InvokeAsync<string>("getValue", id);

    public async ValueTask SetValue(IJSObjectReference id, string value)
        => await InvokeVoidAsync("setValue", id, value);

    public async ValueTask SetTheme(string theme)
        => await InvokeVoidAsync("setTheme", theme);

    public async ValueTask<TextModelOptions[]> GetModels()
        => await InvokeAsync<TextModelOptions[]>("getModels");

    public async ValueTask<TextModelOptions> GetModel(IJSObjectReference id)
        => await InvokeAsync<TextModelOptions>("getModel", id);

    public async ValueTask SetModelLanguage(IJSObjectReference id, string languageId)
        => await InvokeVoidAsync("setModelLanguage", id, languageId);

    public async ValueTask RemeasureFonts()
        => await InvokeVoidAsync("remeasureFonts");

    public async ValueTask AddKeybindingRules(KeybindingRule[] rules)
        => await InvokeVoidAsync("addKeybindingRules", rules);

    public async ValueTask AddKeybindingRule(KeybindingRule rule)
        => await InvokeVoidAsync("addKeybindingRules", rule);
}