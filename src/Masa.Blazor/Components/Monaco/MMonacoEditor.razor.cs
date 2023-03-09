namespace Masa.Blazor;

public partial class MMonacoEditor : BDomComponentBase, IAsyncDisposable
{
    [Inject] 
    protected MonacoEditorJSModule Module { get; set; }

    [Parameter] 
    public object? EditorOptions { get; set; }

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
    public Func<Task<object>>? InitOptions { get; set; }

    [Parameter] 
    public Action? InitCompleteHandle { get; set; }

    private ElementReference _ref;

    private ElementReference? _prevRef;

    private bool _elementReferenceChanged;

    /// <summary>
    /// Monaco
    /// </summary>
    public IJSObjectReference Monaco { get; private set; }

    public virtual ElementReference Ref
    {
        get => _ref;
        set
        {
            if (_prevRef.HasValue)
            {
                if (_prevRef.Value.Id != value.Id)
                {
                    _prevRef = value;
                    _elementReferenceChanged = true;
                }
            }
            else
            {
                _prevRef = value;
            }

            _ref = value;
        }
    }


    protected override void SetComponentClass()
    {
        CssProvider
            .Apply(styleAction: styleBuilder =>
            {
                styleBuilder
                    .AddWidth(Width)
                    .AddMinWidth(MinWidth)
                    .AddMinHeight(MinHeight)
                    .AddMaxHeight(MaxHeight)
                    .AddMaxWidth(MaxWidth);
            });
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await InitMonaco();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task InitMonaco()
    {
        if (InitOptions != null)
        {
            EditorOptions = await InitOptions.Invoke();
        }

        EditorOptions ??= new
        {
            value = string.Empty,
            language = "csharp"
        };

        Monaco = await Module.Init(Id, EditorOptions);

        InitCompleteHandle?.Invoke();
    }

    public async Task DefineTheme(string themeName, StandaloneThemeData themeData)
    {
        await Module.DefineTheme(themeName, themeData);
    }

    public async Task UpdateOptions(object options)
    {
        await Module.UpdateOptions(Monaco,options);
    }

    public async Task<string> GetValue()
    {
        return await Module.GetValue(Monaco);
    }

    public async Task SetValue(string value)
    {
        await Module.SetValue(Monaco, value);
    }

    public async Task SetTheme(string theme)
    {
        await Module.SetTheme(theme);
    }

    public async Task<TextModelOptions[]> GetModels()
    {
        return await Module.GetModels();
    }

    public async Task<TextModelOptions> GetModel(IJSObjectReference id)
    {
        return await Module.GetModel(Monaco);
    }

    public async Task SetModelLanguage(IJSObjectReference id, string languageId)
    {
        await Module.SetModelLanguage(Monaco, languageId);
    }

    public async Task RemeasureFonts()
    {
        await Module.RemeasureFonts();
    }

    public async Task AddKeybindingRules(KeybindingRule[] rules)
    {
        await Module.AddKeybindingRules(rules);
    }

    public async Task AddKeybindingRule(KeybindingRule rule)
    {
        await Module.AddKeybindingRule(rule);
    }

    public async ValueTask DisposeAsync()
    {
        await Module.DisposeAsync();
    }
}