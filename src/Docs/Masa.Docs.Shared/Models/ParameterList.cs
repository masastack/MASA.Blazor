using System.Collections;

namespace Masa.Docs.Shared;

public class ParameterList<TValue> : IEnumerable<ParameterItem<TValue>>
{
    private readonly List<ParameterItem<TValue>> _list;

    public ParameterList(params ParameterItem<TValue>[] list)
    {
        _list = list.ToList();
    }

    public void Add(string key, TValue value)
    {
        if (_list.Any(item => item.Key.Equals(key, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException($"Key {key} exists");
        }

        _list.Add(new ParameterItem<TValue>(key, value));
    }

    public void Remove(string key)
    {
        var value = _list.FirstOrDefault(item => item.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
        if (value is null) return;

        _list.Remove(value);
    }

    public void Clear() => _list.Clear();

    public bool Any() => _list.Any();

    public IEnumerator<ParameterItem<TValue>> GetEnumerator()
    {
        return _list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
