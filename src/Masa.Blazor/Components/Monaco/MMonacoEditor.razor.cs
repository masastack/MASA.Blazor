namespace Masa.Blazor;

public partial class MMonacoEditor : BDomComponentBase
{
    [Inject]
    protected MonacoEditorJSModule Module { get; set; } = null!;

    [Parameter]
    public object? EditorOptions { get; set; }

    [Parameter]
    [MassApiParameter("100%")]
    public StringNumber? Width { get; set; } = "100%";

    [Parameter]
    public StringNumber? Height { get; set; }

    [Parameter]
    public StringNumber? MinWidth { get; set; }

    [Parameter]
    [MassApiParameter("320px")]
    public StringNumber? MinHeight { get; set; } = "320px";

    [Parameter]
    public StringNumber? MaxWidth { get; set; }

    [Parameter]
    public StringNumber? MaxHeight { get; set; }

    [Parameter]
    public Func<Task<object>>? InitOptions { get; set; }

    [Parameter]
    public Action? InitCompleteHandle { get; set; }

    [Parameter]
    public string? Value
    {
        get => _value;
        set
        {
            if (_value != value)
            {
                _value = value;
                _valueChangedByUser = true;
            }

            SetValue(value);
        }
    }

    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    private string? _value;
    private bool _valueChangedByUser;

    public IJSObjectReference? Editor { get; private set; }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);

        Id.ThrowIfNull(ComponentName);
    }

    protected override void RegisterWatchers(PropertyWatcher watcher)
    {
        base.RegisterWatchers(watcher);

        watcher
            .Watch<string>(nameof(Value), ValueChangeCallback);
    }

    private async void ValueChangeCallback(string? val)
    {
        if (_valueChangedByUser)
        {
            _valueChangedByUser = false;
            await SetValueAsync(_value);
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

        // TODO: need dispose the object reference in .NET or JS
        Editor = await Module.Init(Id!, EditorOptions, DotNetObjectReference.Create(this));

        InitCompleteHandle?.Invoke();
    }

    public async Task DefineThemeAsync(string themeName, StandaloneThemeData themeData)
    {
        await Module.DefineTheme(themeName, themeData);
    }

    public async Task AddCommandAsync<T>(int keybinding, DotNetObjectReference<T> dotNetObjectReference, string method) where T : class
    {
        await Module.AddCommand(Editor!, keybinding, dotNetObjectReference, method);
    }

    public async Task UpdateOptionsAsync(object options)
    {
        await Module.UpdateOptions(Editor!, options);
    }

    public async Task<string> GetValueAsync()
    {
        return await Module.GetValue(Editor!);
    }

    public async Task SetValueAsync(string? value)
    {
        await Module.SetValue(Editor!, value);
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
        return await Module.GetModel(Editor!);
    }

    public async Task SetModelLanguageAsync(string languageId)
    {
        await Module.SetModelLanguage(Editor!, languageId);
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
        _valueChangedByUser = false;

        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(value);
        }
    }
}
