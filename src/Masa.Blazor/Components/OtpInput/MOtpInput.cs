using Masa.Blazor.Components.OptInput;

namespace Masa.Blazor;

public partial class MOtpInput : MasaComponentBase, IThemeable
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [CascadingParameter] public MForm? Form { get; set; }

    [CascadingParameter(Name = "IsDark")] public bool CascadingIsDark { get; set; }

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

    [Parameter] public bool Dark { get; set; }

    [Parameter] public bool Light { get; set; }

    public bool IsDark
    {
        get
        {
            if (Dark)
            {
                return true;
            }

            if (Light)
            {
                return false;
            }

            return CascadingIsDark;
        }
    }

    private bool IndependentTheme =>
        (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

    private DotNetObjectReference<Invoker<OtpJsResult>>? _handle;

    private int _prevLength;
    private int _prevFocusIndex;
    private string? _prevValue;

    private List<ElementReference> InputRefs { get; } = new();

    private List<string> Values { get; set; } = new();

    private string ComputedValue => string.Join("", Values);

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

#if NET8_0_OR_GREATER
        if (MasaBlazor.IsSsr && !IndependentTheme)
        {
            CascadingIsDark = MasaBlazor.Theme.Dark;
        }

#endif
        if (_prevValue != Value)
        {
            _prevValue = Value;

            if (Value != null)
            {
                for (int i = 0; i < Value.Length; i++)
                {
                    if (Values.Count < i + 1)
                    {
                        Values.Add(Value[i].ToString());
                    }
                    else
                    {
                        Values[i] = Value[i].ToString();
                    }
                }
            }
        }

        if (_prevLength != Length)
        {
            _prevLength = Length;

            if (Values.Count > Length)
            {
                for (int i = Length; i < Values.Count; i++)
                {
                    Values.RemoveAt(i);
                }

                if (ValueChanged.HasDelegate)
                {
                    await ValueChanged.InvokeAsync(ComputedValue);
                }
            }
            else
            {
                for (int i = 0; i < Length; i++)
                {
                    if (Values.Count() < (i + 1))
                        Values.Add(string.Empty);
                    if (InputRefs.Count() < (i + 1))
                        InputRefs.Add(new ElementReference());
                }
            }
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _handle = DotNetObjectReference.Create(new Invoker<OtpJsResult>(GetResultFromJs));
            await Js.InvokeVoidAsync(JsInteropConstants.RegisterOTPInputOnInputEvent, InputRefs, _handle);

            NextTick(() =>
            {
                if (AutoFocus && InputRefs.Count > 0)
                {
                    _ = InputRefs[0].FocusAsync();
                }
            });
        }
    }

    private Block _block = new("m-otp-input");
    private Block _textFieldBlock = new("m-text-field");
    private Block _inputBlock = new("m-input");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return _block.AddTextColor(Color).AddTheme(IsDark, IndependentTheme).GenerateCssClasses();
    }

    protected override IEnumerable<string> BuildComponentStyle()
    {
        yield return CssClassUtils.GetColor(Color, true) ?? string.Empty;
    }

    private string GetContentClass()
    {
        var inputClasses = _inputBlock.Modifier(IsFocused).And(IsDisabled).AddTheme(IsDark, IndependentTheme);

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

                if (result.Index >= Length - 1 && !Values.Any(p => string.IsNullOrEmpty(p)) && OnFinish.HasDelegate)
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
                    var item = InputRefs[index];
                    await item.FocusAsync();
                }
            }
        }
    }

    private int GetFirstEmptyIndex()
    {
        for (int i = 0; i < Values.Count; i++)
        {
            if (string.IsNullOrEmpty(Values[i]))
            {
                return i;
            }
        }

        return Values.Count - 1;
    }

    private async Task ApplyValues(int index, string value)
    {
        var temp = new List<string>(Values.ToArray());
        temp[index] = value;
        temp.RemoveAll(p => string.IsNullOrEmpty(p));

        Values[index] = value;

        await Task.Yield();
        await InvokeAsync(StateHasChanged);

        Values[index] = String.Empty;

        Values = temp;

        var count = temp.Count;

        for (int i = 0; i < this.Length - count; i++)
        {
            Values.Add(String.Empty);
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

                Values[changeIndex] = clipboardData[i].ToString();
            }

            var newFocusIndex = Math.Min(events.Index + clipboardData.Length - 1, this.Length - 1);
            await FocusAsync(newFocusIndex);

            var hasEmptyValue = Values.Any(string.IsNullOrWhiteSpace);

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
        if (IsFocused || IsDisabled || InputRefs.ElementAtOrDefault(optIndex).Context is null)
        {
            return;
        }

        await FocusAsync(optIndex);
    }

    protected override async ValueTask DisposeAsyncCore()
    {
        _handle?.Dispose();
        await Js.InvokeVoidAsync(JsInteropConstants.UnregisterOTPInputOnInputEvent, InputRefs);
    }
}