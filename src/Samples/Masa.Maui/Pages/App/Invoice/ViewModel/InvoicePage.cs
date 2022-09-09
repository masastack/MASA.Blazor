namespace Masa.Maui.Pages.App.Invoice;

public class InvoicePage
{
    public List<InvoiceRecordDto> Datas { get; set; }

    private IEnumerable<InvoiceRecordDto> GetFilterDatas()
    {
        IEnumerable<InvoiceRecordDto> datas = Datas;

        if (State is not null)
        {
            datas = datas.Where(d => d.State == State);
        }
        if (Search is not null)
        {
            datas = datas.Where(d => d.Client.FullName.ToUpper().Contains(Search.ToUpper()));
        }
        datas = datas.OrderByDescending(d => d.Date);
        if (datas.Count() < (PageIndex - 1) * PageSize) PageIndex = 1;

        return datas;
    }

    public List<InvoiceRecordDto> GetPageDatas()
    {
        return GetFilterDatas().Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
    }

    public int CurrentCount => GetFilterDatas().Count();

    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public int PageCount => (int)Math.Ceiling(CurrentCount / (double)PageSize);

    public int? State { get; set; }

    public string? Search { get; set; }

    public InvoicePage(List<InvoiceRecordDto> datas)
    {
        Datas = datas;
    }
}
