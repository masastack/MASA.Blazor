namespace Masa.Blazor.Docs.Examples.components.cascaders;

public class Usage : Masa.Blazor.Docs.Components.Usage
{
    private record Item(string Value, string Label, List<Item> Children);

    public Usage() : base(typeof(MCascader<Item, string, string>))
    {
    }

    protected override ParameterList<CheckboxParameter> GenCheckboxParameters() => new()
    {
        { nameof(MCascader<Item, string, string>.Dense), new CheckboxParameter("false", true) },
        { nameof(MCascader<Item, string, string>.ShowAllLevels), new CheckboxParameter("false", true) },
        { nameof(MCascader<Item, string, string>.ChangeOnSelect), new CheckboxParameter("false", true) },
    };

    protected override Dictionary<string, object>? GenAdditionalParameters()
    {
        return new Dictionary<string, object>()
        {
            { nameof(MCascader<Item, string, string>.Value), _value },
            {
                nameof(MCascader<Item, string, string>.ValueChanged),
                EventCallback.Factory.Create<string>(this, x => _value = x)
            },
            { nameof(MCascader<Item, string, string>.Items), _items },
            { nameof(MCascader<Item, string, string>.ItemText), (Func<Item, string>)(x => x.Label) },
            {
                nameof(MCascader<Item, string, string>.ItemChildren),
                (Func<Item, List<Item>>)(x => x.Children)
            },
            { nameof(MCascader<Item, string, string>.ItemValue), (Func<Item, string>)(x => x.Value) },
        };
    }

    private string _value;

    private readonly List<Item> _items =
    [
        new Item("1", "湖北", [
            new Item("11", "武汉", [
                new Item("111", "武昌区", [
                    new Item("1111", "黄鹤楼街道", []),
                    new Item("1112", "白沙洲街道", [])
                ]),
                new Item("112", "洪山区", [])
            ]),
            new Item("12", "黄石", []),
            new Item("13", "宜昌", [])
        ]),

        new Item("2", "浙江", [
            new Item("21", "杭州", []),
            new Item("22", "温州", []),
            new Item("23", "义乌", []),
            new Item("24", "宁波", [])
        ]),

        new Item("3", "上海", [
            new Item("31", "徐汇区", []),
            new Item("32", "黄浦区", []),
            new Item("33", "浦东新区", []),
            new Item("34", "崇明区", [])
        ]),

        new Item("4", "北京", [
            new Item("41", "朝阳", []),
            new Item("42", "东城", []),
            new Item("43", "西城", [])
        ]),

        new Item("5", "江苏", [
            new Item("51", "南京", [
                new Item("511", "鼓楼区", []),
                new Item("512", "玄武区", [])
            ]),
            new Item("52", "苏州", []),
            new Item("53", "无锡", []),
            new Item("54", "扬州", [])
        ])
    ];
}