namespace Masa.Blazor.Presets;

public partial class PMobileCascader<TItem, TValue>
{
    [Parameter] public bool Visible { get; set; }

    [Parameter] public EventCallback<bool> VisibleChanged { get; set; }

    [Parameter] public RenderFragment<ActivatorProps> ActivatorContent { get; set; }

    [Parameter] public List<TItem> Items { get; set; }

    [Parameter] public Func<TItem, string> ItemText { get; set; }

    [Parameter] public Func<TItem, TValue> ItemValue { get; set; }

    [Parameter] public Func<TItem, List<TItem>> ItemChildren { get; set; }

    private int _tabValue;

    private List<TItem> Tabs { get; set; } = new();

    private List<string> ComputedTabs
    {
        get
        {
            if (Tabs.Count == 0)
            {
                return new List<string>() { "请选择" };
            }

            var tabs = Tabs.Select(t => ItemText(t)).ToList();

            if (Children is not null && Children.Count > 0)
            {
                tabs.Add("请选择");
            }

            return tabs;
        }
    }

    private List<TItem> CurrentItems { get; set; }
    private List<TItem> Children { get; set; }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (Tabs.Count == 0)
        {
            CurrentItems = Items;
        }
    }

    private void TabValueChanged(StringNumber val)
    {
        _tabValue = val.AsT1;
        var tab = Tabs[_tabValue];

        if (_tabValue == 0)
        {
            CurrentItems = Items;
        }
        else if (_tabValue == 1)
        {
            var index = Items.FindIndex(item => EqualityComparer<TItem>.Default.Equals(item, tab));
            if (index > -1)
            {
                var item = Items[index];
                CurrentItems = ItemChildren(item);
            }
        }
    }

    private void HandleOnItemClick(TItem item)
    {
        // var children = ItemChildren(item);
        // CurrentItems = children;

        if (Tabs.Count > _tabValue)
        {
            var activeTab = Tabs[_tabValue];
            if (EqualityComparer<TItem>.Default.Equals(activeTab, item))
            {
                // 取消选择

                Tabs = Tabs.Take(_tabValue).ToList();

                if (Tabs.Count > 0)
                {
                    CurrentItems = ItemChildren(Tabs.Last());
                }
                else
                {
                    CurrentItems = Items;
                }
            }
            else
            {
                // 选择其他

                Tabs = Tabs.Take(_tabValue).ToList();
                Tabs.Add(item);

                CurrentItems = ItemChildren(item);
            }

            _tabValue = Tabs.Count - 1;
        }
        else
        {
            Tabs.Add(item);
            var children = ItemChildren(item);
            Children = children;
            if (children is not null && children.Count > 0)
            {
                _tabValue++;
                CurrentItems = children;
            }
        }
    }
}
