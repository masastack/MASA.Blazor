using Microsoft.Extensions.Logging;

namespace Masa.Blazor;

public partial class MTemplateTable
{
    [Inject] private ILogger<MTemplateTable> Logger { get; set; } = null!;

    [Inject] private IGraphQLClient GraphQLClient { get; set; } = null!;

    [Parameter] [EditorRequired] public Sheet? Sheet { get; set; }

    [Parameter] public IList<View>? UserViews { get; set; }

    [Parameter] public EventCallback<View> OnUserViewAdd { get; set; }

    [Parameter] public EventCallback<View> OnUserViewUpdate { get; set; }

    [Parameter] public EventCallback<View> OnUserViewDelete { get; set; }

    [Parameter] public string? CheckboxColor { get; set; }

    [Parameter] public EventCallback<ICollection<Row>> OnRemove { get; set; }

    [Parameter] public EventCallback<Sheet> OnSave { get; set; }

    [Parameter] public StringNumber? Height { get; set; }

    [Parameter] public RenderFragment<IReadOnlyDictionary<string, JsonElement>>? RowActionsContent { get; set; }

    [Parameter] public RenderFragment<ICollection<Row>>? ViewActionsContent { get; set; }

    [Parameter] public Role Role { get; set; }

    [Parameter] public int DefaultPageSize { get; set; } = 10;

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

    /// <summary>
    /// All columns including the select and actions columns.
    /// </summary>
    private IList<ColumnInfo> _allColumns = [];

    private ICollection<Row> _rows = [];

    private long _totalCount;
    private bool _loading;

    private IList<View>? _prevUserViews;

    private Sheet? _prevSheet;

    private bool Editable => Role == _sheet?.ActiveView.AccessRole;

    protected override async Task OnParametersSetAsync()
    {
        // base.OnParametersSetAsync();

        if (_prevSheet != Sheet)
        {
            _prevSheet = Sheet;

            FormatSheet();
            if (_sheet is not null)
            {
                _sheet.ActiveView.PageIndex = 1;
                _sheet.ActiveView.PageSize = DefaultPageSize;
                await RefreshItemsAsync(GetItemsProviderRequest());

                if (UserViews is not null)
                {
                    var columns = _sheet.Columns;
                    _sheet.Views.AddRange(UserViews.Select(v => ViewInfo.From(v, columns, Role.User)));
                }
            }
        }
    }

    private void FormatSheet()
    {
        _sheet = SheetInfo.From(Sheet);
        _allColumns = [Preset.CreateSelectColumn(), .._sheet.Columns, Preset.CreateActionsColumn()];

        UpdateStateOfActiveView();
    }

    private async Task RefreshItemsAsync(QueryRequest request)
    {
        if (_sheet is null)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(_sheet.ItemKeyName))
        {
            throw new InvalidOperationException("The 'ItemKeyName' is required.");
        }

        _loading = true;
        StateHasChanged();

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
                            Func = StandardFilter.Contains,
                            Expected = request.FilterRequest.Search,
                            Type = ExpectedType.String
                        });
                    }

                    request.FilterRequest.Operator = FilterOperator.Or;
                }
            }

            var result = await GraphQLClient.QueryAsync(request);

            if (result.HasErrors)
            {
                var errorMessage = string.Join(Environment.NewLine, result.Errors.Select(e => e.Message));
                await PopupService.EnqueueSnackbarAsync(errorMessage, AlertTypes.Error);
                return;
            }

            _totalCount = result.Total;
            _rows = result.Items.Select(item =>
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
            _sheet.UpdateActiveViewItems(_rows);
        }
        catch (Exception e)
        {
            Console.Out.WriteLine(e);
            await PopupService.EnqueueSnackbarAsync(e);
            Logger.LogDebug(e, "Error while refreshing items");
        }
        finally
        {
            _loading = false;
        }
    }

    private void UpdateStateOfActiveView()
    {
        _rowHeight = _sheet!.ActiveViewRowHeight;
    }

    private QueryRequest GetItemsProviderRequest()
    {
        var filterRequest = _sheet!.ActiveView.Value.Filter is null
            ? null
            : new Filter()
            {
                Operator = _sheet.ActiveView.Value.Filter.Operator,
                Search = _sheet.ActiveView.Value.Filter.Search,
                Options = [.._sheet.ActiveView.Value.Filter.Options]
            };

        return new QueryRequest(_sheet.QueryBody, _sheet.CountField, filterRequest,
            _sheet.ActiveView.Value.Sort,
            _sheet.ActiveView.PageIndex, _sheet.ActiveView.PageSize);
    }

    private async Task HandleOnPaginationUpdateAsync((int PageIndex, int PageSize) data)
    {
        _sheet!.ActiveView.PageIndex = data.PageIndex;
        _sheet.ActiveView.PageSize = data.PageSize;
        await RefreshItemsAsync(GetItemsProviderRequest());
    }
}