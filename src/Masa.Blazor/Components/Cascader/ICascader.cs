namespace Masa.Blazor.Components.Cascader;

public interface ICascader<TItem, TItemValue>
{
    void Register(MCascaderColumn<TItem, TItemValue> cascaderColumn);
}