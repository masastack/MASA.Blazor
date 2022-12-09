namespace Masa.Blazor;

public partial class MMonacoEditor : BDomComponentBase, IAsyncDisposable
{
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

    [Parameter] public EditorOptions EditorOptions { get; set; } = new();

    private IJSObjectReference _monaco;

    private bool _isMonacoEditorDisposed = false;
    
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
        _monaco = await Module.Init(Id, EditorOptions);
    }

    public async Task<string> GetValue()
    {
        return await Module.GetValue(_monaco);
    }

    public async Task SetModelLanguage(string language)
    {
        await Module.SetModelLanguage(_monaco, language);
    }

    public async Task SetValue(string newValue)
    {
        await Module.SetValue(_monaco, newValue);
    }

    public async Task SetTheme(string newTheme)
    {
        await Module.SetTheme(newTheme);
    }

    public async Task<TextModelOptions[]> GetModels()
    {
        return await Module.GetModels();
    }

    public async Task<TextModelOptions> GetModel()
    {
        return await Module.GetModel(_monaco);
    }

    public async Task RemeasureFonts()
    {
        await Module.RemeasureFonts();
    }

    public async Task AddKeybindingRules(KeybindingRule[] rules)
    {
        await Module.AddKeybindingRules( rules);
    }

    public async Task AddKeybindingRules(KeybindingRule rule)
    {
        await Module.AddKeybindingRule(rule);
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