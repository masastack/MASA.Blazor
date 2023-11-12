namespace Masa.Blazor;

public partial class MDescriptions : BDomComponentBase, IThemeable
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [CascadingParameter(Name = "IsDark")] private bool CascadingIsDark { get; set; }

    [Parameter] [MassApiParameter(ReleasedOn = "v1.1.1")]
    public bool AlignCenter { get; set; }

    [Parameter, MassApiParameter(true)] public bool Colon { get; set; } = true;

    [Parameter] public int Xs { get; set; }

    [Parameter] public int Sm { get; set; }

    [Parameter] public int Md { get; set; }

    [Parameter, MassApiParameter(3)] public int Column { get; set; } = 3;

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

    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider
            .UseBem("m-descriptions", css => { css.Modifiers(u => u.Modifier(Dense, Bordered, AlignCenter)).AddTheme(IsDark); })
            .Extend("header")
            .Extend("header__title")
            .Extend("header__actions")
            .Extend("view")
            .Extend("row")
            .Extend("item")
            .Extend("item__label", css => { css.Add(LabelClass); }, styleBuilder => { styleBuilder.Add(LabelStyle); })
            .Extend("item__content", css => { css.Add(ContentClass); }, styleBuilder => { styleBuilder.Add(ContentStyle); })
            .Extend("item-container")
            .Extend("item-container__label", css => { css.Modifiers(u => u.Modifier("no-colon", !Colon)).Add(LabelClass); },
                styleBuilder => { styleBuilder.Add(LabelStyle); })
            .Extend("item-container__content", cs => { cs.Add(ContentClass); }, styleBuilder => { styleBuilder.Add(ContentStyle); });
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

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        MasaBlazor.Breakpoint.OnUpdate -= BreakpointOnOnUpdate;
    }
}
