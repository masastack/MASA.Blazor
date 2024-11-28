using Masa.Blazor.Attributes;
using Masa.Blazor.Components.TemplateTable.FilterDialogs;

namespace Masa.Blazor;

public partial class MTemplateTable
{
    [Parameter] public SheetProvider? SheetProvider { get; set; }

    [Parameter] public ItemsProvider? ItemsProvider { get; set; }

    [Parameter] public EventCallback<Sheet> OnSave { get; set; }

    [Parameter] public StringNumber? Height { get; set; }

    [Parameter] [MasaApiParameter(true)] public bool Detail { get; set; } = true;

    [Parameter] public RenderFragment? ActionsContent { get; set; }

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
                  type
                }
                operator
                search
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
              searchable
            }
            activeViewId
            defaultViewId
            pagination {
              pageSizeOptions
            }
          }
        }
        """;

    private SheetInfo _sheet = new();
    private bool _init;
    private ICollection<IReadOnlyDictionary<string, JsonElement>> _items = [];

    private long _totalCount;
    private bool _hasPreviousPage;
    private bool _hasNextPage;
    private bool _loading;

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
            _sheet.Columns.Add(Preset.CreateActionsColumn());
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
                if (!string.IsNullOrWhiteSpace(request.FilterRequest?.Search))
                {
                    var searchableColumns = _sheet.Columns.Where(u => u.Searchable).ToList();
                    if (searchableColumns.Count > 0)
                    {
                        foreach (var searchableColumn in searchableColumns)
                        {
                            request.FilterRequest.Options.Add(new FilterOption()
                            {
                                ColumnId = searchableColumn.Id,
                                Func = FilterTypes.Contains,
                                Expected = request.FilterRequest.Search
                            });
                        }

                        request.FilterRequest.Operator = FilterOperator.Or;
                    }
                }

                var result = await ItemsProvider(request);
                await Task.Delay(500); // TODO: just for testing, remove it if implemented
                _totalCount = result.Result.TotalCount;
                _hasPreviousPage = result.Result.PageInfo.HasPreviousPage;
                _hasNextPage = result.Result.PageInfo.HasNextPage;
                _items = result.Result.Items ?? [];

                _sheet.UpdateActiveViewItems(_items, _hasPreviousPage, _hasNextPage);
            }
            catch (Exception e)
            {
                await PopupService.EnqueueSnackbarAsync(e);
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

        _columnOrder.Clear();

        foreach (var viewColumn in _sheet.ActiveViewColumns)
        {
            _columnOrder.Add(viewColumn.ColumnId);

            var column = _sheet.Columns.FirstOrDefault(u => u.Id == viewColumn.ColumnId);
            if (column is not null)
            {
                viewColumn.Column = column;
            }
        }

        if (_sheet.ActiveViewHasActions)
        {
            _columnOrder.Add(Preset.ActionsColumnId);

            if (_sheet.ActiveViewColumns.All(u => u.ColumnId != Preset.ActionsColumnId))
            {
                _sheet.ActiveViewColumns.Add(Preset.CreateActionsViewColumn());
            }
        }
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
            FilterRequest = new Filter()
            {
                Operator = _sheet.ActiveView.Value.Filter.Operator,
                Search = _sheet.ActiveView.Value.Filter.Search,
                Options = [.._sheet.ActiveView.Value.Filter.Options]
            },
            SortRequest = _sheet.ActiveView.Value.Sort
        };
    }

    private async Task HandleOnPaginationUpdateAsync((int PageIndex, int PageSize) data)
    {
        _sheet.ActiveView.PageIndex = data.PageIndex;
        _sheet.ActiveView.PageSize = data.PageSize;
        await RefreshItemsAsync(GetItemsProviderRequest());
    }
}