using Masa.Blazor.Components.MonacoEditor.Options;

namespace Masa.Blazor.Components.MonacoEditor;

public partial class MMonacoEditor : BDomComponentBase, IAsyncDisposable
{
    [Inject]
    protected I18n I18n { get; set; }

    [Inject]
    protected MonacoEditorJSModule Module { get; set; }

    [Parameter]
    public StringNumber Width { get; set; } = "100%";

    [Parameter]
    public StringNumber Height { get; set; } = "100%";

    [Parameter]
    public StringNumber MinWidth { get; set; }

    [Parameter]
    public StringNumber MinHeight { get; set; }

    [Parameter]
    public StringNumber MaxWidth { get; set; }

    [Parameter]
    public StringNumber MaxHeight { get; set; }
    
    [Parameter]
    public object Option { get; set; } = new { };

    [Parameter]
    public bool Light { get; set; }

    [Parameter]
    public bool Dark { get; set; }

    [Parameter]
    public string Theme { get; set; }

    [CascadingParameter(Name = "IsDark")]
    public bool CascadingIsDark { get; set; }

    [Parameter] public EditorOptions EditorOptions { get; set; } = new ();


    private bool _isMonacoEditorDisposed = false;

    private object _prevOption;

    public string ComputedTheme
    {
        get
        {
            if (Theme is not null)
            {
                return Theme;
            }

            if (Dark)
            {
                return "dark";
            }

            return null;
        }
    }

    protected override void SetComponentClass()
    {
        CssProvider
            .Apply(styleAction: styleBuilder =>
            {
                styleBuilder
                    .AddWidth(Width)
                    .AddHeight(Height)
                    .AddMinWidth(MinWidth)
                    .AddMinHeight(MinHeight)
                    .AddMaxHeight(MaxHeight)
                    .AddMaxWidth(MaxWidth);
            });
    }

    protected override async Task OnInitializedAsync()
    {

        await InitMonaco();
        await base.OnInitializedAsync();
    }
    

    public async Task InitMonaco()
    {
        await Module.Init(Id, EditorOptions);
    }

    public async Task<string> GetValue()
    {
        return await Module.GetValue(Id);
    }

    public async Task SetModelLanguage(string language)
    {
        await Module.SetModelLanguage(Id, language);
    }

    public async Task<bool> SetValue(string newValue)
    {
        return await Module.SetValue(Id, newValue);
    }

    public async Task<bool> SetTheme(string newTheme)
    {
        return await Module.SetTheme(Id, newTheme);
    }

    public async Task<TextModelOptions[]> GetModels()
    {
        return await Module.GetModels(Id);
    }

    public async Task<TextModelOptions> GetModel(Uri uri)
    {
        return await Module.GetModel(Id, uri);
    }

    public async Task RemeasureFonts()
    {
        await Module.RemeasureFonts(Id);
    }

    public async Task AddKeybindingRules(KeybindingRule[] rules)
    {
        await Module.AddKeybindingRules(Id, rules);
    }

    public async Task AddKeybindingRules(KeybindingRule rule)
    {
        await Module.AddKeybindingRule(Id, rule);
    }


    public async Task DisposeECharts()
    {

        _isMonacoEditorDisposed = true;
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            await DisposeECharts();

        }
        catch
        {
            // ignored
        }
    }
}