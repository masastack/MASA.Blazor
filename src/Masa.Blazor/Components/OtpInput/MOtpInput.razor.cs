using Masa.Blazor.Components.OptInput;

namespace Masa.Blazor;

public partial class MOtpInput : ThemeComponentBase, IThemeable
{
    [CascadingParameter] public MForm? Form { get; set; }

    [Parameter] public string? Color { get; set; }

    [Parameter]
    [MasaApiParameter(ReleasedOn = "v1.5.0")]
    public bool AutoFocus { get; set; }

    [Parameter] public int Length { get; set; } = 6;

    [Parameter] public OtpInputType Type { get; set; } = OtpInputType.Text;

    [Parameter] public bool Readonly { get; set; }

    [Parameter] public string? BackgroundColor { get; set; }

    [Parameter] public bool Disabled { get; set; }

    [Parameter] public StringNumber? Height { get; set; }

    [Parameter] public string? Value { get; set; }

    [Parameter] public EventCallback<string?> ValueChanged { get; set; }

    [Parameter] public bool Plain { get; set; }

    [Parameter] public EventCallback<string> OnFinish { get; set; }

    [Parameter] public EventCallback<string> OnInput { get; set; }

    private DotNetObjectReference<Invoker<OtpJsResult>>? _handle;

    private int _prevLength;
    private int _prevFocusIndex;
    private string? _prevValue;

    private List<ElementReference> _inputRefs = [];

    /// <summary>
    /// Always keep the length of _otp equal to Length
    /// </summary>
    private List<string> _otp = [];

    /// <summary>
    /// Get the latest value of otp
    /// </summary>
    private string ComputedValue => string.Join("", _otp);

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (_prevLength != Length)
        {
            _prevLength = Length;

            if (_otp.Count > Length)
            {
                _otp = _otp.Take(Length).ToList();

                if (ValueChanged.HasDelegate)
                {
                    _prevValue = ComputedValue;
                    await ValueChanged.InvokeAsync(_prevValue);
                }
            }
            else
            {
                for (var i = 0; i < Length; i++)
                {
                    if (_otp.Count < (i + 1))
                        _otp.Add(string.Empty);
                    if (_inputRefs.Count < (i + 1))
                        _inputRefs.Add(new ElementReference());
                }
            }
        }

        if (_prevValue != Value)
        {
            Value ??= string.Empty;
            var prevValueLength = _prevValue?.Length ?? 0;
            var valueLength = Value.Length;
            _prevValue = Value;

            for (var i = 0; i < Math.Max(prevValueLength, valueLength); i++)
            {
                if (i >= _otp.Count)
                {
                    continue;
                }

                _otp[i] = i < valueLength ? Value[i].ToString() : string.Empty;
            }

            if (Value != ComputedValue)
            {
                _prevValue = ComputedValue;
                await ValueChanged.InvokeAsync(_prevValue);
            }
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _handle = DotNetObjectReference.Create(new Invoker<OtpJsResult>(GetResultFromJs));
            await Js.InvokeVoidAsync(JsInteropConstants.RegisterOTPInputOnInputEvent, _inputRefs, _handle);

            NextTick(() =>
            {
                if (AutoFocus && _inputRefs.Count > 0)
                {
                    _ = _inputRefs[0].FocusAsync();
                }
            });
        }
    }

    private static Block _block = new("m-otp-input");
    private static Block _textFieldBlock = new("m-text-field");
    private static Block _inputBlock = new("m-input");
    private ModifierBuilder _inputModifierBuilder = _inputBlock.CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _block.Name;
        yield return CssClassUtils.GetTextColor(Color);
        yield return CssClassUtils.GetTheme(ComputedTheme);
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        yield return CssClassUtils.GetColor(Color, true) ?? string.Empty;
    }

    private string GetContentClass()
    {
        var inputClasses = _inputModifierBuilder.Add(IsFocused).Add(IsDisabled).AddTheme(ComputedTheme);

        StringBuilder stringBuilder = new();
        stringBuilder.Append(inputClasses);
        stringBuilder.Append(" ");
        stringBuilder.Append("m-text-field m-text-field--is-booted ");
        stringBuilder.Append(Plain ? "m-otp-input--plain " : "m-text-field--outlined ");
        return stringBuilder.ToString().Trim();
    }

    private async Task GetResultFromJs(OtpJsResult result)
    {
        switch (result.Type)
        {
            case "Backspace":
            case "Input":
                await ApplyValues(result.Index, result.Value);
                if (ValueChanged.HasDelegate)
                {
                    await ValueChanged.InvokeAsync(ComputedValue);
                }

                if (result.Type == "input" && OnInput.HasDelegate)
                {
                    await OnInput.InvokeAsync(result.Value);
                }

                if (result.Index >= Length - 1 && !_otp.Any(p => string.IsNullOrEmpty(p)) && OnFinish.HasDelegate)
                {
                    await OnFinish.InvokeAsync(ComputedValue);
                }

                break;
            default:
                break;
        }
    }

    private async Task FocusAsync(int index)
    {
        if (_prevFocusIndex != index)
        {
            _prevFocusIndex = index;

            if (index < 0)
            {
                await FocusAsync(0);
            }
            else if (index >= Length)
            {
                await FocusAsync(Length - 1);
            }
            else
            {
                var containsActiveElement =
                    await Js.InvokeAsync<bool>(JsInteropConstants.ContainsActiveElement, Ref);

                if (containsActiveElement)
                {
                    var item = _inputRefs[index];
                    await item.FocusAsync();
                }
            }
        }
    }

    private int GetFirstEmptyIndex()
    {
        for (int i = 0; i < _otp.Count; i++)
        {
            if (string.IsNullOrEmpty(_otp[i]))
            {
                return i;
            }
        }

        return _otp.Count - 1;
    }

    private async Task ApplyValues(int index, string value)
    {
        var temp = new List<string>(_otp.ToArray());
        temp[index] = value;
        temp.RemoveAll(p => string.IsNullOrEmpty(p));

        _otp[index] = value;

        await Task.Yield();
        await InvokeAsync(StateHasChanged);

        _otp[index] = String.Empty;

        _otp = temp;

        var count = temp.Count;

        for (int i = 0; i < this.Length - count; i++)
        {
            _otp.Add(String.Empty);
        }

        await InvokeAsync(StateHasChanged);
    }


    private async Task HandleOnPasteAsync(OtpInputEventArgs<PasteWithDataEventArgs> events)
    {
        var clipboardData = events.Args.PastedData;

        if (!string.IsNullOrWhiteSpace(clipboardData))
        {
            var firstEmptyIndex = GetFirstEmptyIndex();

            var startIndex = Math.Min(firstEmptyIndex, events.Index);

            for (int i = 0; i < clipboardData.Length; i++)
            {
                var changeIndex = startIndex + i;

                if (changeIndex >= this.Length)
                    break;

                _otp[changeIndex] = clipboardData[i].ToString();
            }

            var newFocusIndex = Math.Min(events.Index + clipboardData.Length - 1, this.Length - 1);
            await FocusAsync(newFocusIndex);

            var hasEmptyValue = _otp.Any(string.IsNullOrWhiteSpace);

            if (!hasEmptyValue)
            {
                if (OnFinish.HasDelegate)
                {
                    await OnFinish.InvokeAsync(ComputedValue);
                }
            }
        }
    }

    private async Task OnPasteWithDataAsync(PasteWithDataEventArgs args)
    {
        await HandleOnPasteAsync(new OtpInputEventArgs<PasteWithDataEventArgs>(args));
    }

    private bool IsFocused { get; set; }

    public virtual bool IsDisabled => Disabled || Form is { Disabled: true };

    private async Task HandleOnInputSlotClick(int optIndex)
    {
        if (IsFocused || IsDisabled || _inputRefs.ElementAtOrDefault(optIndex).Context is null)
        {
            return;
        }

        await FocusAsync(optIndex);
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        _handle?.Dispose();
        await Js.InvokeVoidAsync(JsInteropConstants.UnregisterOTPInputOnInputEvent, _inputRefs);
    }
}