namespace Masa.Blazor.Docs.Examples.components.selects;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    protected override ParameterList<bool> GenToggleParameters() => new()
    {
        { nameof(MSelect<Item,string,string>.Solo), false },
        { nameof(MSelect<Item,string,string>.Filled), false },
        { nameof(MSelect<Item,string,string>.Outlined), false },
    };

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MSelect<Item,string,string>.Clearable), new CheckboxParameter("false",true) },
        { nameof(MSelect<Item,string,string>.Chips), new CheckboxParameter("false",true) },
    };

    public Usage() : base(typeof(MSelect<Item, string, string>))
    {
    }

    protected override Dictionary<string, object>? GenAdditionalParameters()
    {
        return new Dictionary<string, object>()
        {
            { nameof(MSelect<Item,string,string>.Items), items },
            { nameof(MSelect<Item,string,string>.ItemText), (Func<Item,string>)(x=>x.Label) },
            { nameof(MSelect<Item,string,string>.ItemValue),  (Func<Item,string>)(x=>x.Value) },
            { nameof(MSelect<Item,string,string>.Label), "Select" }
        };
    }

    public class Item
    {
        public string Label { get; set; }
        public string Value { get; set; }

        public Item(string label, string value)
        {
            Label = label;
            Value = value;
        }
    }

    List<Item> items = new()
    {
        new Item("Foo", "1"),
        new Item("Bar", "2"),
        new Item("Fizz", "3"),
        new Item("Buzz", "4"),
    };
}