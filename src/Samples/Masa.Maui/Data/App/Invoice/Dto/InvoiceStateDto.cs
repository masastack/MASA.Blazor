namespace Masa.Maui.Data.App.Invoice.Dto;

public class InvoiceStateDto
{
    public string Label { get; set; }

    public int Value { get; set; }

    public InvoiceStateDto(string label, int value)
    {
        Label = label;
        Value = value;
    }
}

