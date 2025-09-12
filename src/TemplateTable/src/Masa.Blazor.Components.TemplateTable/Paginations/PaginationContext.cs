namespace Masa.Blazor.Components.TemplateTable.Paginations
{
    public record PaginationContext(int PageIndex, int PageSize, long Total, IEnumerable<int> PageSizeOptions, Func<(int, int),Task> OnOptionsChanged);
}
