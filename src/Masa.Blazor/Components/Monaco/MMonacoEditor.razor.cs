namespace Masa.Blazor;

public partial class MMonacoEditor : BDomComponentBase
{
    [Inject]
    protected MonacoEditorJSModule Module { get; set; }

    [Parameter]
    public object? EditorOptions { get; set; }

    [Parameter]
    public StringNumber Width { get; set; } = "100%";

    [Parameter]
    public StringNumber Height { get; set; }

    [Parameter]
    public StringNumber MinWidth { get; set; }

    [Parameter]
    public StringNumber MinHeight { get; set; } = "320px";

    [Parameter]
    public StringNumber MaxWidth { get; set; }

    [Parameter]
    public StringNumber MaxHeight { get; set; }

    [Parameter]
    public Func<Task<object>>? InitOptions { get; set; }

    [Parameter]
    public Action? InitCompleteHandle { get; set; }

    [Parameter]
    public string Value
    {
        get
        {
            return _value;
        }
        set
        {
            if (_value != value)
            {
                _value = value;
            }

            SetValue(value);
        }
    }

    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    /// <summary>
    /// Monaco
    /// </summary>
    public IJSObjectReference Monaco { get; private set; }

    private string _value;

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher
            .Watch<string>(nameof(Value), async val =>
            {
                await SetValueAsync(_value);
            });
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

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await InitMonaco();
            await SetValueAsync(Value);
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

        Monaco = await Module.Init(Id, EditorOptions, DotNetObjectReference.Create(this));

        InitCompleteHandle?.Invoke();
    }

    public async Task DefineThemeAsync(string themeName, StandaloneThemeData themeData)
    {
        await Module.DefineTheme(themeName, themeData);
    }

    public async Task AddCommandAsync<T>(int keybinding, DotNetObjectReference<T> dotNetObjectReference, string method) where T : class
    {
        await Module.AddCommand(Monaco, keybinding, dotNetObjectReference, method);
    }

    public async Task UpdateOptionsAsync(object options)
    {
        await Module.UpdateOptions(Monaco, options);
    }

    public async Task<string> GetValueAsync()
    {
        return await Module.GetValue(Monaco);
    }

    public async Task SetValueAsync(string value)
    {
        await Module.SetValue(Monaco, value);
    }

    public async Task SetThemeAsync(string theme)
    {
        await Module.SetTheme(theme);
    }

    public async Task<TextModelOptions[]> GetModelsAsync()
    {
        return await Module.GetModels();
    }

    public async Task<TextModelOptions> GetModelAsync()
    {
        return await Module.GetModel(Monaco);
    }

    public async Task SetModelLanguageAsync(string languageId)
    {
        await Module.SetModelLanguage(Monaco, languageId);
    }

    public async Task RemeasureFontsAsync()
    {
        await Module.RemeasureFonts();
    }

    public async Task AddKeybindingRulesAsync(KeybindingRule[] rules)
    {
        await Module.AddKeybindingRules(rules);
    }

    public async Task AddKeybindingRuleAsync(KeybindingRule rule)
    {
        await Module.AddKeybindingRule(rule);
    }

    [JSInvokable]
    public async Task OnChange(string value)
    {
        _value = value;

        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(value);
        }
    }
}
