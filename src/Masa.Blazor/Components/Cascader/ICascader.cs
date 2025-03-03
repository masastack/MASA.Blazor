namespace Masa.Blazor.Components.Cascader;

public interface ICascader<TItem, TItemValue>
{
    bool ChangeOnSelect { get; }
    
    void Register(MCascaderColumn<TItem, TItemValue> cascaderColumn);
}