namespace Masa.Maui.Data.App.Invoice.Dto;

public class BillDto
{
    public string? Type { get; set; }

    public int Cost { get; set; }

    public int Qty { get; set; }

    public decimal Price { get; set; }

    public string? Remark { get; set; }

    public bool ShowMenu { get; set; }

    public string? Discount { get; set; }

    public int Tax1 { get; set; }

    public int Tax2 { get; set; }

    public BillDto() { }

    public BillDto(string? type, int cost, int qty, decimal price, string? remark)
    {
        Type = type;
        Cost = cost;
        Qty = qty;
        Price = price;
        Remark = remark;
    }

    public void Set(BillDto bill)
    {
        Type = bill.Type;
        Cost = bill.Cost;
        Qty = bill.Qty;
        Price = bill.Price;
        Remark = bill.Remark;
    }
}

