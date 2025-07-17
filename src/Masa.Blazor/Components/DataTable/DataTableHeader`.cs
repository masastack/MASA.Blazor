namespace Masa.Blazor;

public class DataTableHeader<TItem> : DataTableHeader
{
    public DataTableHeader()
    {
    }

    public DataTableHeader(string text, Func<TItem, object?> valueExpression)
    {
        Text = text ?? throw new ArgumentNullException(nameof(text));
        ValueExpression = valueExpression ?? throw new ArgumentNullException(nameof(valueExpression));
    }
    
    private Func<TItem, object?>? _valueExpression;

    public Func<object?, string?, TItem, bool>? Filter { get; set; }

    public bool Filterable { get; set; } = true;

    public Func<TItem, OneOf<string, RenderFragment>>? CellRender { get; set; }

    public Func<TItem, object?>? ValueExpression
    {
        get => _valueExpression;
        set
        {
            _valueExpression = value;
            UpdateItemValue();
        }
    }

    public override string? Value
    {
        get => base.Value;
        set
        {
            base.Value = value;
            UpdateItemValue();
        }
    }

    public ItemValue<TItem>? ItemValue { get; private set; }

    private void UpdateItemValue()
    {
        if (_valueExpression is not null)
        {
            ItemValue = new ItemValue<TItem>(_valueExpression);
        }
        else if (Value is not null)
        {
            ItemValue = new ItemValue<TItem>(Value);
        }
        else
        {
            ItemValue = null;
        }
    }
}