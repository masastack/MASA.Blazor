using Masa.Blazor.Components.MonacoEditor.Options;

namespace Masa.Blazor.Components.MonacoEditor;

public class MonacoEditorJSModule : JSModule
{
    public MonacoEditorJSModule(IJSRuntime js) : base(js, "./_content/Masa.Blazor/js/proxies/monaco-proxy.js")
    {
    }

    public async ValueTask<IJSObjectReference> Init(string id, EditorOptions options)
        => await InvokeAsync<IJSObjectReference>("init", id, options);

    public async ValueTask<string> GetValue(IJSObjectReference id)
        => await InvokeAsync<string>("getValue", id);

    public async ValueTask<bool> SetValue(IJSObjectReference id, string value)
        => await InvokeAsync<bool>("setValue", id, value);

    public async ValueTask<bool> SetTheme(IJSObjectReference id, string theme)
        => await InvokeAsync<bool>("setTheme", id, theme);

    public async ValueTask<TextModelOptions[]> GetModels(IJSObjectReference id)
        => await InvokeAsync<TextModelOptions[]>("getModels", id);

    public async ValueTask<TextModelOptions> GetModel(IJSObjectReference id, Uri uri)
        => await InvokeAsync<TextModelOptions>("getModel", id, uri);

    public async ValueTask SetModelLanguage(IJSObjectReference id, string languageId)
        => await InvokeVoidAsync("setModelLanguage", id, languageId);

    public async ValueTask RemeasureFonts(IJSObjectReference id)
        => await InvokeVoidAsync("remeasureFonts", id);

    public async ValueTask AddKeybindingRules(IJSObjectReference id, KeybindingRule[] rules)
        => await InvokeVoidAsync("addKeybindingRules", id, rules);

    public async ValueTask AddKeybindingRule(IJSObjectReference id, KeybindingRule rule)
        => await InvokeVoidAsync("addKeybindingRules", id, rule);
}