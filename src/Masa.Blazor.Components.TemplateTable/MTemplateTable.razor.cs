namespace Masa.Blazor;

public partial class MTemplateTable
{
    [Parameter] public SheetProvider? SheetProvider { get; set; }

    [Parameter] public ItemsProvider? ItemsProvider { get; set; }

    [Parameter] public EventCallback<Sheet> OnSave { get; set; }

    [Parameter] public StringNumber? Height { get; set; }

    [Parameter] public EventCallback<IReadOnlyDictionary<string, JsonElement>> OnUpdate { get; set; }

    [Parameter] public EventCallback<IReadOnlyDictionary<string, JsonElement>> OnDelete { get; set; }

    [Parameter] public EventCallback<IReadOnlyDictionary<string, JsonElement>> OnAction1 { get; set; }

    [Parameter] public EventCallback<IReadOnlyDictionary<string, JsonElement>> OnAction2 { get; set; }

    private const string SheetQuery =
        """
        query {
          sheet {
            views {
              hasActions
              id
              name
              rowHeight
              type
              columns {
                columnId
                hidden
                width
              }
              filter {
                options {
                  columnId
                  func
                  expected
                }
                operator
              }
              sort {
                options {
                  columnId
                  orderBy
                  index
                }
              }
            }
            columns {
              config
              id
              name
              type
            }
            activeViewId
            defaultViewId
          }
        }
        """;

    private SheetInfo _sheet = new();
    private bool _init;
    private ICollection<IReadOnlyDictionary<string, JsonElement>> _items = [];
    private ICollection<ViewColumnInfo> _viewColumns = [];


    private long _totalCount;
    private bool _hasPreviousPage;
    private bool _hasNextPage;
    private bool _loading;

    private bool HasActions => OnUpdate.HasDelegate || OnDelete.HasDelegate ||
                               OnAction1.HasDelegate || OnAction2.HasDelegate;

    protected override async Task OnParametersSetAsync()
    {
        // base.OnParametersSetAsync();

        if (_init == false)
        {
            _init = true;
            await RefreshSheetAsync(GetSheetProviderRequest());
            _sheet.ActiveView.PageIndex = 1;
            _sheet.ActiveView.PageSize = 5;
            await RefreshItemsAsync(GetItemsProviderRequest());
        }
    }

    private async Task RefreshSheetAsync(SheetProviderRequest request)
    {
        if (SheetProvider is not null)
        {
            var result = await SheetProvider(request);
            _sheet = SheetInfo.From(result.Sheet);
        }

        if (HasActions && _sheet.Columns.All(u => u.Id != Preset.ActionsColumnId))
        {
            _sheet.Columns.Add(CreateActionsColumn());
        }

        UpdateStateOfActiveView();
    }

    private async Task RefreshItemsAsync(ItemsProviderRequest request)
    {
        if (ItemsProvider is not null)
        {
            _loading = true;

            try
            {
                var result = await ItemsProvider(request);
                await Task.Delay(500); // TODO: just for testing, remove it if implemented
                _totalCount = result.Result.TotalCount;
                _hasPreviousPage = result.Result.PageInfo.HasPreviousPage;
                _hasNextPage = result.Result.PageInfo.HasNextPage;
                _items = result.Result.Items ?? [];

                _sheet.UpdateActiveViewItems(_items, _hasPreviousPage, _hasNextPage);
            }
            finally
            {
                _loading = false;
            }
        }
    }

    private void UpdateStateOfActiveView()
    {
        _rowHeight = _sheet.ActiveViewRowHeight;
        _hasNextPage = _sheet.ActiveView?.HasNextPage ?? false;
        _hasPreviousPage = _sheet.ActiveView?.HasPreviousPage ?? false;

        _viewColumns = []; // needs different instance
        _hiddenColumnIds.Clear();
        _columnOrder.Clear();

        foreach (var viewColumn in _sheet.ActiveViewColumns)
        {
            if (viewColumn.Hidden)
            {
                _hiddenColumnIds.Add(viewColumn.ColumnId);
            }

            _columnOrder.Add(viewColumn.ColumnId);

            var column = _sheet.Columns.FirstOrDefault(u => u.Id == viewColumn.ColumnId);
            if (column is not null)
            {
                _viewColumns.Add(ViewColumnInfo.From(viewColumn, column));
            }
        }

        if (_sheet.ActiveViewHasActions)
        {
            _columnOrder.Add(Preset.ActionsColumnId);
            _viewColumns.Add(CreateActionsViewColumn());
        }
    }

    private static ColumnInfo CreateActionsColumn()
    {
        return new ColumnInfo()
        {
            Id = Preset.ActionsColumnId,
            Name = "Actions",
            Type = ColumnType.Actions
        };
    }

    private static ViewColumnInfo CreateActionsViewColumn()
    {
        var column = CreateActionsColumn();
        return ViewColumnInfo.From(Preset.ActionsColumnId, false, column);
    }

    private SheetProviderRequest GetSheetProviderRequest()
    {
        return new SheetProviderRequest(SheetQuery);
    }

    private ItemsProviderRequest GetItemsProviderRequest()
    {
        return new ItemsProviderRequest()
        {
            PageIndex = _sheet.ActiveView.PageIndex,
            PageSize = _sheet.ActiveView.PageSize,
            FilterRequest = _sheet.ActiveView.Filter,
            SortRequest = _sheet.ActiveView.Sort
        };
    }

    private async Task HandleOnNextPage()
    {
        _sheet.ActiveView.PageIndex++;

        await RefreshItemsAsync(GetItemsProviderRequest());
    }

    private async Task HandleOnPreviousPage()
    {
        _sheet.ActiveView.PageIndex--;
        await RefreshItemsAsync(GetItemsProviderRequest());
    }
}