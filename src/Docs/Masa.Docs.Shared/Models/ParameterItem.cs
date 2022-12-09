namespace Masa.Docs.Shared;

public class ParameterItem<TValue>
{
    public string Key { get; set; }

    public TValue Value { get; set; }

    public ParameterItem(string key, TValue value)
    {
        Key = key;
        Value = value;
    }
}
