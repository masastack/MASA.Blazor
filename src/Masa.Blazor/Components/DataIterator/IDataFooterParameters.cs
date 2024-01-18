namespace Masa.Blazor;

public interface IDataFooterParameters
{
    string? ItemsPerPageText { get; set; }
    DataOptions Options { get; set; }
    DataPagination Pagination { get; set; }
    EventCallback<Action<DataOptions>> OnOptionsUpdate { get; set; }
    IEnumerable<OneOf<int, DataItemsPerPageOption>>? ItemsPerPageOptions { get; set; }
    string? PrevIcon { get; set; }
    string? NextIcon { get; set; }
    string? LastIcon { get; set; }
    string? FirstIcon { get; set; }
    string? ItemsPerPageAllText { get; set; }
    bool ShowFirstLastPage { get; set; }
    bool ShowCurrentPage { get; set; }
    bool DisablePagination { get; set; }
    bool DisableItemsPerPage { get; set; }
    string? PageText { get; set; }
}
