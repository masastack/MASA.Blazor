using Masa.Blazor.Components.Input;

namespace Masa.Blazor;

public partial class MFileInput<TValue> : MTextField<TValue>
{
    [Inject] protected I18n I18n { get; set; } = null!;

    [Parameter] public bool HideInput { get; set; }

    [Parameter] public bool Chips { get; set; }

    [Parameter] public bool SmallChips { get; set; }

    [Parameter] public RenderFragment<(int index, string text)>? SelectionContent { get; set; }

    [Parameter] public bool Multiple { get; set; }

    [Parameter] public bool ShowSize { get; set; }

    [Parameter]
    [MasaApiParameter("$file")]
    public override string? PrependIcon { get; set; } = "$file";

    [Parameter] [MasaApiParameter(true)] public override bool Clearable { get; set; } = true;

    [Parameter] [MasaApiParameter(22)] public StringNumber TruncateLength { get; set; } = 22;

    [Parameter] public string? Accept { get; set; }

    /// <summary>
    /// Dot not use this parameter because it's inherited from MTextField and not supported in MFileInput.
    /// </summary>
    [MasaApiParameter(Ignored = true)]
    public override bool UpdateOnChange { get; set; }

    /// <summary>
    /// Dot not use this parameter because it's inherited from MTextField and not supported in MFileInput.
    /// </summary>
    [MasaApiParameter(Ignored = true)]
    public override bool UpdateOnBlur { get; set; }

    public override Action<TextFieldNumberProperty>? NumberProps { get; set; }

    protected override Dictionary<string, object> InputAttrs => new(Attributes)
    {
        { "type", "file" },
        { "accept", Accept }
    };

    public bool HasChips => Chips || SmallChips;

    public IList<string?> Text
    {
        get
        {
            if (!IsDirty && (IsFocused || !HasLabel))
            {
                return new List<string?> { Placeholder };
            }

            var text = new List<string?>();

            foreach (var file in Files)
            {
                var truncatedText = TruncateText(file.Name);
                text.Add(!ShowSize ? truncatedText : $"{truncatedText} ({HumanReadableFileSize(file.Size)})");
            }

            return text;
        }
    }

    public InputFile? InputFile { get; set; }

    public override ElementReference InputElement
    {
        get => InputFile?.Element ?? default;
        set => base.InputElement = value;
    }

    public IList<IBrowserFile> Files
    {
        get
        {
            if (InternalValue is IBrowserFile file)
            {
                return new List<IBrowserFile>
                {
                    file
                };
            }
            else if (InternalValue is IList<IBrowserFile> files)
            {
                return files;
            }

            return new List<IBrowserFile>();
        }
    }

    public override StringNumber ComputedCounterValue
    {
        get
        {
            if (!ShowSize)
            {
                return string.Format(I18n.T("$masaBlazor.fileInput.counter"), Files.Count);
            }

            var bytes = Files.Sum(r => r.Size);

            return string.Format(I18n.T("$masaBlazor.fileInput.counterSize"), Files.Count,
                HumanReadableFileSize((bytes)));
        }
    }

    protected override bool IsDirty => Files.Count > 0;

    public override bool HasPrependClick => true;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (typeof(TValue) != typeof(IBrowserFile) && typeof(TValue) != typeof(List<IBrowserFile>))
        {
            throw new ArgumentException("TValue should be IBrowserFile or List<IBrowserFile>");
        }
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (Multiple && typeof(TValue) != typeof(List<IBrowserFile>))
        {
            throw new ArgumentException("Multiple TValue should be List<IBrowserFile>");
        }
    }

    public string HumanReadableFileSize(long bytes, bool binary = false)
    {
        var @base = binary ? 1024M : 1000M;
        if (bytes < @base)
        {
            return $"{bytes} B";
        }

        var prefix = binary ? new[] { "Ki", "Mi", "Gi" } : new[] { "k", "M", "G" };
        var unit = -1;
        var size = Convert.ToDecimal(bytes);

        while (Math.Abs(size) > @base && unit < prefix.Length - 1)
        {
            size /= @base;
            ++unit;
        }

        return $"{Math.Round(size, 1)} {prefix[unit]}B";
    }

    public string TruncateText(string name)
    {
        if (name.Length < TruncateLength.ToInt32())
        {
            return name;
        }

        var charsKeepOneSide = Convert.ToInt32(Math.Floor(Math.Max(0, TruncateLength.ToInt32() - 1) / 2.0));
        return string.Concat(name.AsSpan(0, charsKeepOneSide), "…", name.AsSpan(name.Length - charsKeepOneSide));
    }

    private Block _block = new Block("m-file-input");

    protected override IEnumerable<string> BuildComponentClass()
    {
        return base.BuildComponentClass().Concat(
            _block.GenerateCssClasses()
        );
    }

    protected override IEnumerable<string> BuildControlStyle()
    {
        if (HideInput)
        {
            return base.BuildControlStyle().Concat(
                new[] { "display:none" }
            );
        }

        return base.BuildControlStyle();
    }

    public override async Task HandleOnPrependClickAsync(MouseEventArgs args)
    {
        if (InputFile?.Element is null) return;

        await base.HandleOnPrependClickAsync(args);
        var input = Document.GetElementByReference(InputFile.Element.Value);
        if (input is not null)
        {
            var @event = new MouseEvent("click");
            await input.DispatchEventAsync(@event, stopPropagation: true);
        }
    }

    public override Task HandleOnInputAsync(ChangeEventArgs args) => Task.CompletedTask;

    public override Task HandleOnChangeAsync(ChangeEventArgs args) => Task.CompletedTask;

    public override async Task HandleOnBlurAsync(FocusEventArgs args)
    {
        IsFocused = false;
        if (OnBlur.HasDelegate)
        {
            await OnBlur.InvokeAsync(args);
        }
    }

    public override async Task HandleOnKeyDownAsync(KeyboardEventArgs args)
    {
        if (OnKeyDown.HasDelegate)
        {
            await OnKeyDown.InvokeAsync(args);
        }
    }

    public async Task HandleOnFileChange(InputFileChangeEventArgs args)
    {
        if (Multiple)
        {
            IList<IBrowserFile> files = new List<IBrowserFile>();

            foreach (var file in args.GetMultipleFiles())
            {
                files.Add(file);
            }

            UpdateInternalValue((TValue)files, InternalValueChangeType.Input);
        }
        else
        {
            UpdateInternalValue(args.FileCount > 0 ? (TValue)args.File : default, InternalValueChangeType.Input);
        }

        await OnInput.InvokeAsync(InternalValue);
        await OnChange.InvokeAsync(InternalValue);
    }

    public override async Task HandleOnClickAsync(ExMouseEventArgs args)
    {
        await base.HandleOnClickAsync(args);

        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync();
        }

        if (InputFile?.Element is null) return;

        var input = Document.GetElementByReference(InputFile.Element.Value);
        if (input is not null)
        {
            var @event = new MouseEvent("click");
            await input.DispatchEventAsync(@event, stopPropagation: true);
        }
    }

    public override async Task HandleOnClearClickAsync(MouseEventArgs args)
    {
        if (InputFile?.Element is null) return;

        var input = Document.GetElementByReference(InputFile.Element.Value);
        if (input is not null)
        {
            await input.SetPropertyAsync("value", "");
        }

        if (Multiple)
        {
            IList<IBrowserFile> values = new List<IBrowserFile>();
            UpdateInternalValue((TValue)values, InternalValueChangeType.InternalOperation);
        }
        else
        {
            UpdateInternalValue(default, InternalValueChangeType.InternalOperation);
        }

        await OnClearClick.InvokeAsync(args);
        await OnChange.InvokeAsync(InternalValue);
    }
}