using Masa.Blazor.Components.TemplateTable.FilterDialogs;

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
                  direction
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

    private SheetManager _sheet = new();
    private bool _init;
    private ICollection<IReadOnlyDictionary<string, JsonElement>> _items = [];
    private IList<ViewColumn> _viewColumns = [];

    private Filter? _filter;
    private Sort _sort = new();

    private long _totalCount;
    private int _pageIndex = 1;
    private int _pageSize = 5;
    private bool _hasPreviousPage;
    private bool _hasNextPage;

    private bool HasActions => OnUpdate.HasDelegate || OnDelete.HasDelegate ||
                               OnAction1.HasDelegate || OnAction2.HasDelegate;

    protected override async Task OnParametersSetAsync()
    {
        // base.OnParametersSetAsync();

        if (_init == false)
        {
            _init = true;
            await RefreshSheetAsync(GetSheetProviderRequest());
            await RefreshItemsAsync(GetItemsProviderRequest(1, 5, _sheet.ActiveView?.Filter));
        }
    }

    private async Task RefreshSheetAsync(SheetProviderRequest request)
    {
        if (SheetProvider is not null)
        {
            var result = await SheetProvider(request);
            _sheet = result.Sheet;
        }

        if (HasActions)
        {
            _sheet.Columns.Add(CreateActionsColumn());
        }

        UpdateStateOfActiveView();
    }

    private async Task RefreshItemsAsync(ItemsProviderRequest request)
    {
        if (ItemsProvider is not null)
        {
            var result = await ItemsProvider(request);
            _totalCount = result.Result.TotalCount;
            _hasPreviousPage = result.Result.PageInfo.HasPreviousPage;
            _hasNextPage = result.Result.PageInfo.HasNextPage;
            _items = result.Result.Items ?? [];
        }
    }

    private void UpdateStateOfActiveView()
    {
        _rowHeight = _sheet.ActiveViewRowHeight;
        _viewColumns = _sheet.ActiveViewColumns.ToList(); // needs different instance
        _filter = _sheet.ActiveView?.Filter;
        _sort = _sheet.ActiveView?.Sort ?? new Sort();

        _hiddenColumnIds.Clear();
        _columnOrder.Clear();

        foreach (var viewColumn in _viewColumns)
        {
            if (viewColumn.Hidden)
            {
                _hiddenColumnIds.Add(viewColumn.ColumnId);
            }

            _columnOrder.Add(viewColumn.ColumnId);

            var column = _sheet.Columns.FirstOrDefault(u => u.Id == viewColumn.ColumnId);
            if (column is not null)
            {
                viewColumn.AttachColumn(column);
            }
        }

        if (_sheet.ActiveViewHasActions)
        {
            _columnOrder.Add(Preset.ActionsColumnId);
            _viewColumns.Add(CreateActionsViewColumn());
        }
    }

    private static Column CreateActionsColumn()
    {
        return new()
        {
            Id = Preset.ActionsColumnId,
            Name = "Actions",
            Type = ColumnType.Actions
        };
    }

    private static ViewColumn CreateActionsViewColumn()
    {
        var column = CreateActionsColumn();
        return new()
        {
            ColumnId = Preset.ActionsColumnId,
            Hidden = false,
            Column = column
        };
    }

    private SheetProviderRequest GetSheetProviderRequest()
    {
        return new SheetProviderRequest(SheetQuery);
    }

    private ItemsProviderRequest GetItemsProviderRequest(int pageIndex, int pageSize,
        Filter? filterRequest = null, Sort? sortRequest = null)
    {
        _pageIndex = pageIndex;
        _pageSize = pageSize;

        return new ItemsProviderRequest()
        {
            PageIndex = _pageIndex,
            PageSize = _pageSize,
            FilterRequest = filterRequest,
            SortRequest = sortRequest
        };
    }

    private async Task HandleOnNextPage()
    {
        await RefreshItemsAsync(GetItemsProviderRequest(_pageIndex + 1, _pageSize));
    }

    private async Task HandleOnPreviousPage()
    {
        await RefreshItemsAsync(GetItemsProviderRequest(_pageIndex - 1, _pageSize));
    }
}