using Element = BemIt.Element;

namespace Masa.Blazor;

public partial class MDescriptions : MasaComponentBase, IThemeable
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [CascadingParameter(Name = "IsDark")] private bool CascadingIsDark { get; set; }

    [Parameter] [MasaApiParameter(ReleasedOn = "v1.1.1")]
    public bool AlignCenter { get; set; }

    [Parameter, MasaApiParameter(true)] public bool Colon { get; set; } = true;

    [Parameter] public int Xs { get; set; }

    [Parameter] public int Sm { get; set; }

    [Parameter] public int Md { get; set; }

    [Parameter, MasaApiParameter(3)] public int Column { get; set; } = 3;

    [Parameter] public int Lg { get; set; }

    [Parameter] public int Xl { get; set; }

    [Parameter] public bool Bordered { get; set; }

    [Parameter] public bool Dense { get; set; }

    [Parameter] public bool Vertical { get; set; }

    [Parameter] public string? LabelStyle { get; set; }

    [Parameter] public string? LabelClass { get; set; }

    [Parameter] public string? ContentStyle { get; set; }

    [Parameter] public string? ContentClass { get; set; }

    [Parameter] public string? Title { get; set; }

    [Parameter] public RenderFragment? TitleContent { get; set; }

    [Parameter] public RenderFragment? ActionsContent { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public bool Dark { get; set; }

    [Parameter] public bool Light { get; set; }

    private readonly List<IDescriptionsItem> _descriptionItems = new();

    private CancellationTokenSource _ctsForRegister = new();
    private CancellationTokenSource _ctsForUnregister = new();
    private bool _renderTwice = true;

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

    private bool HasTitle => !string.IsNullOrWhiteSpace(Title) || TitleContent != null;

    private bool HasHeader => HasTitle || ActionsContent != null;

    private int ComputeColumn
    {
        get
        {
            var val = 0;

            if (MasaBlazor.Breakpoint.XsOnly)
            {
                val = Xs <= 0 ? 1 : Xs;
            }
            else if (MasaBlazor.Breakpoint.SmAndDown)
            {
                val = Sm <= 0 ? 2 : Sm;
            }
            else if (MasaBlazor.Breakpoint.MdAndDown && Md > 0)
            {
                val = Md;
            }
            else if (MasaBlazor.Breakpoint.LgAndDown && Lg > 0)
            {
                val = Lg;
            }
            else if (MasaBlazor.Breakpoint.XlOnly && Xl > 0)
            {
                val = Xl;
            }

            if (val == 0)
            {
                return Column;
            }

            return val;
        }
    }

    private List<List<DescriptionsGroupItem>> Rows
    {
        get
        {
            var rows = new List<List<DescriptionsGroupItem>>();
            var row = new List<DescriptionsGroupItem>();
            var col = 0;
            for (var index = 0; index < _descriptionItems.Count; index++)
            {
                var item = _descriptionItems[index];

                if (col + item.Span > ComputeColumn)
                {
                    rows.Add(row);
                    row = new List<DescriptionsGroupItem>();
                    col = 0;
                }

                var isLast = _descriptionItems.Count - 1 == index;
                int span;
                if (isLast)
                {
                    span = ComputeColumn - col;
                }
                else
                {
                    var next = _descriptionItems[index + 1];
                    if (next.Span + col + item.Span > ComputeColumn)
                    {
                        span = ComputeColumn - col;
                    }
                    else
                    {
                        span = item.Span;
                    }
                }

                row.Add(new DescriptionsGroupItem
                {
                    Span = span,
                    Label = item.Label,
                    ChildContent = item.ChildContent,
                    LabelStyle = item.LabelStyle,
                    LabelClass = item.LabelClass,
                    Class = item.Class,
                    Style = item.Style
                });
                col += item.Span;
            }

            if (row.Count > 0)
            {
                rows.Add(row);
            }

            return rows;
        }
    }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);

        if (Column < 1)
        {
            throw new InvalidOperationException(
                $"The {ComponentName} component requires a value greater than 0 for the '{nameof(Column)}' parameter.");
        }
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        MasaBlazor.Breakpoint.OnUpdate += BreakpointOnOnUpdate;
    }

    
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        // DescriptionItem may be update after render, so we need to render twice
        if (_renderTwice)
        {
            _renderTwice = false;
            StateHasChanged();
        }
        else
        {
            _renderTwice = true;
        }
    }

    private async void BreakpointOnOnUpdate(object? sender, BreakpointChangedEventArgs e)
    {
        await InvokeStateHasChangedAsync();
    }

    private bool IndependentTheme => (IsDirtyParameter(nameof(Dark)) && Dark) || (IsDirtyParameter(nameof(Light)) && Light);

#if NET8_0_OR_GREATER

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (MasaBlazor.IsSsr && !IndependentTheme)
            {
                CascadingIsDark = MasaBlazor.Theme.Dark;
            }
        }
#endif
    
    private static Block _block = new("m-descriptions");
    private ModifierBuilder _modifierBuilder = _block.CreateModifierBuilder();
    private static Block _header = _block.Extend("header");
    private static Element _headerTitle = _header.Element("title");
    private static Element _headerActions = _header.Element("actions");
    private static Block _item = _block.Extend("item");
    private static Element _itemLabel = _item.Element("label");
    private static Element _itemContent = _item.Element("content");
    private static Block _row = _block.Extend("row");
    private static Block _itemContainer = _block.Extend("item-container");
    private static Element _itemContainerLabel = _itemContainer.Element("label");
    private static Element _itemContainerContent = _itemContainer.Element("content");
    private ModifierBuilder _itemContainerLabelModifierBuilder = _itemContainerLabel.CreateModifierBuilder();

    protected override IEnumerable<string> BuildComponentClass()
    {
        yield return _modifierBuilder
            .Add(Dense, Bordered, AlignCenter)
            .AddTheme(IsDark, IndependentTheme)
            .Build();
    }

    internal async Task Register(IDescriptionsItem descriptionsItem)
    {
        _descriptionItems.Add(descriptionsItem);

        _ctsForRegister.Cancel();
        _ctsForRegister = new CancellationTokenSource();
        await RunTaskInMicrosecondsAsync(StateHasChanged, 300, _ctsForRegister.Token);
    }


    internal async Task Unregister(IDescriptionsItem descriptionsItem)
    {
        _descriptionItems.Remove(descriptionsItem);

        if (IsDisposed)
        {
            return;
        }

        _ctsForUnregister.Cancel();
        _ctsForUnregister = new CancellationTokenSource();
        await RunTaskInMicrosecondsAsync(StateHasChanged, 300, _ctsForUnregister.Token);
    }

    protected override ValueTask DisposeAsyncCore()
    {
        MasaBlazor.Breakpoint.OnUpdate -= BreakpointOnOnUpdate;

        return base.DisposeAsyncCore();
    }
}
