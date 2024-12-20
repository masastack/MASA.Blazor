using Masa.Blazor.Attributes;
using Masa.Blazor.Components.TemplateTable.FilterDialogs;
using Microsoft.Extensions.Logging;

namespace Masa.Blazor;

public partial class MTemplateTable
{
    [Inject] private ILogger<MTemplateTable> Logger { get; set; } = null!;

    [Parameter] public SheetProvider? SheetProvider { get; set; }

    [Parameter] public IList<View>? UserViews { get; set; }

    [Parameter] public EventCallback<View> OnUserViewAdd { get; set; }

    [Parameter] public EventCallback<View> OnUserViewUpdate { get; set; }

    [Parameter] public EventCallback<View> OnUserViewDelete { get; set; }

    [Parameter] public ItemsProvider? ItemsProvider { get; set; }

    [Parameter] public string? CheckboxColor { get; set; }

    [Parameter] public EventCallback<ICollection<Row>> OnRemove { get; set; }

    [Parameter] public EventCallback<Sheet> OnSave { get; set; }

    [Parameter] public StringNumber? Height { get; set; }

    [Parameter] public RenderFragment? RowActionsContent { get; set; }

    [Parameter] public RenderFragment<ICollection<Row>>? ViewActionsContent { get; set; }

    [Parameter] public Role Role { get; set; }

    // TODO: Sheet请求服务不一定是GraphQL的，需要支持普通的HTTP请求
    private const string SheetQuery =
        """
        query {
          sheet {
            views {
              showActions
              showSelect
              showDetail
              showBulkDelete
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
            itemKeyName
            pagination {
              pageSizeOptions
            }
          }
        }
        """;

    private SheetInfo? _sheet = null;
    private bool _init;
    private ICollection<Row> _rows = [];

    private long _totalCount;
    private bool _hasPreviousPage;
    private bool _hasNextPage;
    private bool _loading;

    private IList<View>? _prevUserViews;

    protected override async Task OnParametersSetAsync()
    {
        // base.OnParametersSetAsync();

        if (_init == false)
        {
            _init = true;
            await RefreshSheetAsync(GetSheetProviderRequest());
            if (_sheet is not null)
            {
                _sheet.ActiveView.PageIndex = 1;
                _sheet.ActiveView.PageSize = 5;
                await RefreshItemsAsync(GetItemsProviderRequest());
            }
        }

        if (_sheet is not null)
        {
            if (!Equals(_prevUserViews, UserViews))
            {
                _sheet.Views.RemoveAll(v => v.AccessRole == Role.User);

                if (UserViews is not null)
                {
                    var columns = _sheet.Columns;
                    _sheet.Views.AddRange(UserViews.Select(v => ViewInfo.From(v, columns, Role.User)));
                }
            }
        }
    }

    private async Task RefreshSheetAsync(SheetProviderRequest request)
    {
        if (SheetProvider is not null)
        {
            try
            {
                var result = await SheetProvider(request);
                if (result.Errors?.Length > 0)
                {
                    await PopupService.EnqueueSnackbarAsync("Error: " + result.Errors[0].Message, AlertTypes.Error);
                    return;
                }

                _sheet = SheetInfo.From(result.Data.Sheet);

                // add select and actions columns, they are not in the column data
                // because they are controlled by the ShowSelect and HasActions properties
                _sheet.Columns.Add(Preset.CreateActionsColumn());
                _sheet.Columns.Add(Preset.CreateSelectColumn());

                UpdateStateOfActiveView();
            }
            catch (Exception e)
            {
                await PopupService.EnqueueSnackbarAsync(e);
                Logger.LogDebug(e, "Error while refreshing sheet");
            }
        }
    }

    private async Task RefreshItemsAsync(ItemsProviderRequest request)
    {
        if (_sheet is null)
        {
            return;
        }

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
                _totalCount = result.Result.TotalCount;
                _hasPreviousPage = result.Result.PageInfo.HasPreviousPage;
                _hasNextPage = result.Result.PageInfo.HasNextPage;

                if (string.IsNullOrWhiteSpace(_sheet.ItemKeyName))
                {
                    throw new InvalidOperationException("The 'ItemKeyName' is required.");
                }

                _rows = (result.Result.Items ?? []).Select(item =>
                {
                    if (!item.TryGetValueWithCaseInsensitiveKey(_sheet.ItemKeyName, out var value))
                    {
                        throw new InvalidOperationException("Cannot find the item key that named " +
                                                            _sheet.ItemKeyName);
                    }

                    if (string.IsNullOrWhiteSpace(value))
                    {
                        throw new InvalidOperationException("The item key should not be null or empty");
                    }

                    return new Row(value, item);
                }).ToList();

                _sheet.UpdateActiveViewItems(_rows, _hasPreviousPage, _hasNextPage);
            }
            catch (Exception e)
            {
                await PopupService.EnqueueSnackbarAsync(e);
                Logger.LogDebug(e, "Error while refreshing items");
            }
            finally
            {
                _loading = false;
            }
        }
    }

    private void UpdateStateOfActiveView()
    {
        /*
         * 1. 还原视图的行高
         * 2. 还原视图的分页信息
         * 3. 还原列顺序
         * 4. 还原列的配置信息
         */

        _rowHeight = _sheet!.ActiveViewRowHeight;
        _hasNextPage = _sheet.ActiveView.HasNextPage;
        _hasPreviousPage = _sheet.ActiveView.HasPreviousPage;

        _columnOrder.Clear();

        var selectColumnExists = false;
        var actionsColumnExists = false;

        foreach (var viewColumn in _sheet.ActiveView.Columns)
        {
            var column = _sheet.Columns.FirstOrDefault(u => u.Id == viewColumn.ColumnId);
            if (column is not null)
            {
                viewColumn.Column = column;
            }

            if (viewColumn.ColumnId == Preset.RowSelectColumnId)
            {
                selectColumnExists = true;
            }
            else if (viewColumn.ColumnId == Preset.ActionsColumnId)
            {
                actionsColumnExists = true;
            }
            else
            {
                _columnOrder.Add(viewColumn.ColumnId);
            }
        }

        if (_sheet.ActiveView.Value.ShowSelect)
        {
            if (!selectColumnExists)
            {
                _sheet.ActiveViewColumns.Insert(0, Preset.CreateSelectViewColumn());
            }

            _columnOrder.Insert(0, Preset.RowSelectColumnId);
        }

        if (_sheet.ActiveView.Value.ShowActions)
        {
            if (!actionsColumnExists)
            {
                _sheet.ActiveViewColumns.Add(Preset.CreateActionsViewColumn());
            }

            _columnOrder.Add(Preset.ActionsColumnId);
        }

        Console.Out.WriteLine($"[MTemplateTable] UpdateStateOfActiveView {string.Join(',', _columnOrder)}");
    }

    private SheetProviderRequest GetSheetProviderRequest()
    {
        return new SheetProviderRequest(SheetQuery);
    }

    private ItemsProviderRequest GetItemsProviderRequest()
    {
        var filterRequest = _sheet!.ActiveView.Value.Filter is null
            ? null
            : new Filter()
            {
                Operator = _sheet.ActiveView.Value.Filter.Operator,
                Search = _sheet.ActiveView.Value.Filter.Search,
                Options = [.._sheet.ActiveView.Value.Filter.Options]
            };

        return new ItemsProviderRequest()
        {
            PageIndex = _sheet.ActiveView.PageIndex,
            PageSize = _sheet.ActiveView.PageSize,
            FilterRequest = filterRequest,
            SortRequest = _sheet.ActiveView.Value.Sort
        };
    }

    private async Task HandleOnPaginationUpdateAsync((int PageIndex, int PageSize) data)
    {
        _sheet!.ActiveView.PageIndex = data.PageIndex;
        _sheet.ActiveView.PageSize = data.PageSize;
        await RefreshItemsAsync(GetItemsProviderRequest());
    }
}