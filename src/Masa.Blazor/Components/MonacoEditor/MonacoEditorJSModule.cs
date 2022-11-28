using Masa.Blazor.Components.MonacoEditor.Options;

namespace Masa.Blazor.Components.MonacoEditor;

public class MonacoEditorJSModule : JSModule
{
    public MonacoEditorJSModule(IJSRuntime js) : base(js, "./_content/Masa.Blazor/js/monaco-editor/0.34.1/min/vs/monaco-helper.js")
    {
    }

    public async ValueTask<bool> Init(string id, EditorOptions options)
        => await InvokeAsync<bool>("Init", id, options);

    public async ValueTask<string> GetValue(string id)
        => await InvokeAsync<string>("GetValue", id);

    public async ValueTask<bool> SetValue(string id, string value)
        => await InvokeAsync<bool>("SetValue", id, value);

    public async ValueTask<bool> SetTheme(string id, string theme)
        => await InvokeAsync<bool>("SetTheme", id, theme);

    public async ValueTask<TextModelOptions[]> GetModels(string id)
        => await InvokeAsync<TextModelOptions[]>("GetModels", id);

    public async ValueTask<TextModelOptions> GetModel(string id, Uri uri)
        => await InvokeAsync<TextModelOptions>("getModel", id, uri);

    public async ValueTask SetModelLanguage(string id, TextModelOptions model, string languageId) 
        => await InvokeVoidAsync("setModelLanguage", id, model, languageId);

    public async ValueTask RemeasureFonts(string id)
        => await InvokeVoidAsync("remeasureFonts", id);

    public async ValueTask AddKeybindingRules(string id, KeybindingRule[] rules)
        => await InvokeVoidAsync("addKeybindingRules", id, rules);

    public async ValueTask AddKeybindingRule(string id, KeybindingRule rule)
        => await InvokeVoidAsync("addKeybindingRules", id, rule);
}