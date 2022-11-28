using Masa.Blazor.Components.MonacoEditor.Options;

namespace Masa.Blazor.Components.MonacoEditor;

public class MonacoEditorJSModule : JSModule
{
    public MonacoEditorJSModule(IJSRuntime js) : base(js, "./_content/Masa.Blazor/js/monaco-editor/0.34.1/min/vs/monaco-helper.js")
    {
    }

    public async ValueTask<bool> Init(string id, EditorOptions options)
        => await InvokeAsync<bool>("Masa.Editor.Init", id, options);

    public async ValueTask<string> GetValue(string id)
        => await InvokeAsync<string>("Masa.Editor.GetValue", id);

    public async ValueTask<bool> SetValue(string id, string value)
        => await InvokeAsync<bool>("Masa.Editor.SetValue", id, value);

    public async ValueTask<bool> SetTheme(string id, string theme)
        => await InvokeAsync<bool>("Masa.Editor.SetTheme", id, theme);

    public async ValueTask<TextModelOptions[]> GetModels(string id)
        => await InvokeAsync<TextModelOptions[]>("Masa.Editor.GetModels", id);

    public async ValueTask<TextModelOptions> GetModel(string id, Uri uri)
        => await InvokeAsync<TextModelOptions>("Masa.Editor.getModel", id, uri);

    public async ValueTask SetModelLanguage(string id, TextModelOptions model, string languageId) 
        => await InvokeVoidAsync("Masa.Editor.setModelLanguage", id, model, languageId);

    public async ValueTask RemeasureFonts(string id)
        => await InvokeVoidAsync("Masa.Editor.remeasureFonts", id);

    public async ValueTask AddKeybindingRules(string id, KeybindingRule[] rules)
        => await InvokeVoidAsync("Masa.Editor.addKeybindingRules", id, rules);

    public async ValueTask AddKeybindingRule(string id, KeybindingRule rule)
        => await InvokeVoidAsync("Masa.Editor.addKeybindingRules", id, rule);
}