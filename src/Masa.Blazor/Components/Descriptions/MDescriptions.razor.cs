namespace Masa.Blazor;

public class RowCol : IDescriptionItem
{
    public int Span { get; set; }

    public string? Label { get; set; }

    public RenderFragment? ChildContent { get; set; }
}

public partial class MDescriptions : BDomComponentBase
{
    [Inject] private MasaBlazor MasaBlazor { get; set; } = null!;

    [Parameter, ApiDefaultValue(true)] public bool Colon { get; set; } = true;

    [Parameter, ApiDefaultValue(4)] public int Cols { get; set; } = 4;

    [Parameter] public int? Sm { get; set; }

    [Parameter] public int? Md { get; set; }

    [Parameter] public int? Lg { get; set; }

    [Parameter] public int? Xl { get; set; }

    [Parameter] public bool Bordered { get; set; }

    [Parameter] public bool Dense { get; set; }

    [Parameter] public string? Title { get; set; }

    [Parameter] public RenderFragment? TitleContent { get; set; }

    [Parameter] public RenderFragment? ActionsContent { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    private readonly List<IDescriptionItem> _descriptionItems = new();

    private bool HasTitle => !string.IsNullOrWhiteSpace(Title) || TitleContent != null;

    private bool HasHeader => HasTitle || ActionsContent != null;

    private int ComputeColumn
    {
        get
        {
            var val = Cols;

            if (MasaBlazor.Breakpoint.Xl && Xl.HasValue)
            {
                val = Xl.Value;
            }
            else if (MasaBlazor.Breakpoint.LgAndUp && Lg.HasValue)
            {
                val = Lg.Value;
            }
            else if (MasaBlazor.Breakpoint.MdAndUp && Md.HasValue)
            {
                val = Md.Value;
            }
            else if (MasaBlazor.Breakpoint.SmAndUp && Sm.HasValue)
            {
                val = Sm.Value;
            }

            return 12 / (val <= 0 ? 4 : val);
        }
    }

    private List<List<RowCol>> Rows
    {
        get
        {
            var rows = new List<List<RowCol>>();
            var row = new List<RowCol>();
            var col = 0;
            for (var index = 0; index < _descriptionItems.Count; index++)
            {
                var item = _descriptionItems[index];
                if (col + item.Span > ComputeColumn)
                {
                    rows.Add(row);
                    row = new List<RowCol>();
                    col = 0;
                }

                var span = _descriptionItems.Count - 1 == index ? ComputeColumn - col : item.Span;

                row.Add(new RowCol
                {
                    Span = span,
                    Label = item.Label,
                    ChildContent = item.ChildContent
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

    protected override void OnInitialized()
    {
        base.OnInitialized();

        MasaBlazor.Breakpoint.OnUpdate += BreakpointOnOnUpdate;
    }

    private Task BreakpointOnOnUpdate()
    {
        return InvokeStateHasChangedAsync();
    }

    protected override void SetComponentClass()
    {
        base.SetComponentClass();

        CssProvider
            .UseBaseCssName("m-descriptions")
            .Apply(cssBuilder =>
            {
                cssBuilder.AddIf("--dense", () => Dense)
                          .AddIf("--bordered", () => Bordered);
            })
            .Apply("header")
            .Apply("header__title")
            .Apply("header__actions")
            .Apply("view")
            .Apply("row")
            .Apply("item")
            .Apply("item__label")
            .Apply("item__content")
            .Apply("item-container")
            .Apply("item-container__label", cssBuilder =>
            {
                cssBuilder
                    .AddIf("--no-colon", () => !Colon);
            })
            .Apply("item-container__content");
    }

    internal void Register(IDescriptionItem descriptionItem)
    {
        _descriptionItems.Add(descriptionItem);
        StateHasChanged();
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        MasaBlazor.Breakpoint.OnUpdate -= BreakpointOnOnUpdate;
    }
}
