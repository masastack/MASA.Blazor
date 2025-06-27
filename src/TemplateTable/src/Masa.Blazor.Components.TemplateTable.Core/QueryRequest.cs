namespace Masa.Blazor.Components.TemplateTable.Core;

public struct QueryRequest
{
    internal QueryRequest(string queryBody,
        string countField,
        Filter? filter,
        Sort? sort,
        int pageIndex,
        int pageSize)
    {
        QueryBody = queryBody;
        CountField = countField;
        FilterRequest = filter;
        SortRequest = sort;
        PageIndex = pageIndex;
        PageSize = pageSize;
    }

    public int PageIndex { get; set; }

    public int PageSize { get; set; }

    public string QueryBody { get; init; }

    public string CountField { get; init; }

    public Filter? FilterRequest { get; set; }

    public Sort? SortRequest { get; set; }
}