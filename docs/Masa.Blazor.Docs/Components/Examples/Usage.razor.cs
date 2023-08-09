namespace Masa.Blazor.Docs.Components;

[JSCustomElement(IncludeNamespace = true)]
public partial class Usage : NextTickComponentBase
{
    [Parameter]
    public bool Dark { get; set; }
    
    private const string DefaultKey = "default";

    private readonly Type _type;

    private ParameterList<bool> _toggleParameters = new();
    private ParameterList<CheckboxParameter> _checkboxParameters;
    private ParameterList<SliderParameter> _sliderParameters;
    private ParameterList<SelectParameter> _selectParameters;
    private RenderFragment? _childContent;

    private bool _rendered;
    private StringNumber? _toggleValue;

    private bool HasRightOptions => _checkboxParameters.Any() || _sliderParameters.Any() || _selectParameters.Any();

    public Usage(Type type)
    {
        _type = type;
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _toggleParameters = GenToggleParameters();
        _toggleParameters.Insert(0, new ParameterItem<bool>(DefaultKey, false));

        _checkboxParameters = GenCheckboxParameters();
        _sliderParameters = GenSliderParameters();
        _selectParameters = GenSelectParameters();
        _childContent = GenChildContent();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            NextTick(async () =>
            {
                if (!_rendered)
                {
                    await Task.Delay(1);
                    _rendered = true;
                    StateHasChanged();
                }
            });

            StateHasChanged();
        }
    }

    private Dictionary<string, object?> Parameters
    {
        get
        {
            var parameters = new List<ParameterItem<object?>>();
            parameters.AddRange(_toggleParameters.Where(item => item.Key != DefaultKey)
                                                 .Select(item => new ParameterItem<object?>(item.Key, item.Value)));
            parameters.AddRange(_checkboxParameters.Select(item => new ParameterItem<object?>(item.Key,
                item.Value.Value ? (item.Value.IsBoolean ? item.Value.Value : item.Value.ParameterValue) : default)));
            parameters.AddRange(_sliderParameters.Select(item => new ParameterItem<object?>(item.Key, item.Value.Value)));
            parameters.AddRange(_selectParameters.Select(item => new ParameterItem<object?>(item.Key, item.Value.Value)));

            var dict = parameters.ToDictionary(item => item.Key, CastValue);

            if (_childContent is not null)
            {
                dict.Add("ChildContent", _childContent);
            }

            var additionalParameters = GenAdditionalParameters();
            if (additionalParameters is not null)
            {
                foreach (var (key, value) in additionalParameters)
                {
                    dict.Add(key, value);
                }
            }

            return dict;
        }
    }

    protected virtual string ComponentName => GetComponentName(_type);
    protected virtual ParameterList<bool> GenToggleParameters() => new();
    protected virtual ParameterList<CheckboxParameter> GenCheckboxParameters() => new();
    protected virtual ParameterList<SliderParameter> GenSliderParameters() => new();
    protected virtual ParameterList<SelectParameter> GenSelectParameters() => new();
    protected virtual RenderFragment? GenChildContent() => default;
    protected virtual Dictionary<string, object>? GenAdditionalParameters() => default;

    protected virtual object? CastValue(ParameterItem<object?> parameter)
    {
        return parameter.Value;
    }

    /// <summary>
    /// Format value to string for display.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    protected virtual string FormatValue(string key, object value)
    {
        return value.ToString() ?? string.Empty;
    }

    protected virtual IEnumerable<ParameterItem<bool>> ActiveToggleParameters =>
        _toggleParameters.Where(item => item.Key != DefaultKey && item.Value);

    protected virtual IEnumerable<ParameterItem<CheckboxParameter>> ActiveCheckboxParameters =>
        _checkboxParameters.Where(item => item.Value.Value);

    protected virtual IEnumerable<ParameterItem<SliderParameter>> ActiveSliderParameters =>
        _sliderParameters.Where(item => item.Value.Condition);

    protected virtual IEnumerable<ParameterItem<SelectParameter>> ActiveSelectParameters =>
        _selectParameters.Where(item => item.Value.Value != null);

    protected virtual IEnumerable<string> AdditionalParameters => Enumerable.Empty<string>();

    protected virtual string? ChildContentSourceCode => null;

    private string SourceCode
    {
        get
        {
            var parameterList = new List<string>();
            parameterList.AddRange(AdditionalParameters);

            parameterList.AddRange(ActiveToggleParameters.Select(item => item.Key));
            parameterList.AddRange(ActiveCheckboxParameters.Select(item =>
                item.Value.IsBoolean ? item.Key : $"{item.Key}=\"{FormatValue(item.Key, item.Value.ParameterValue)}\""));
            parameterList.AddRange(ActiveSliderParameters.Select(item => $"{item.Key}=\"{FormatValue(item.Key, item.Value.Value)}\""));
            parameterList.AddRange(ActiveSelectParameters.Select(item => $"{item.Key}=\"{FormatValue(item.Key, item.Value.Value!)}\""));

            var parameters = string.Join($"{Environment.NewLine}\t", parameterList);

            return parameterList.Count == 0
                ? $"<{ComponentName}>{ChildContentSourceCode}</{ComponentName}>"
                : ChildContentSourceCode is null
                    ? $"<{ComponentName}{Environment.NewLine}\t{parameters}>{Environment.NewLine}</{ComponentName}>"
                    : $"<{ComponentName}{Environment.NewLine}\t{parameters}>{Environment.NewLine}\t{ChildContentSourceCode}{Environment.NewLine}</{ComponentName}>";
        }
    }

    private void ToggleValueChanged(StringNumber val)
    {
        _toggleValue = val;

        foreach (var parameter in _toggleParameters)
        {
            parameter.Value = val == parameter.Key;
        }
    }

    private static string GetComponentName(Type type)
    {
        return type.IsGenericType ? type.Name.Remove(type.Name.IndexOf('`')) : type.Name;
    }
}
