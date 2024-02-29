using Masa.Blazor.Utils;

using System.Collections;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace Masa.Blazor.Presets.PageContainer;

public class LRUCache<TKey, TValue> : IEnumerable<TValue> where TKey : notnull
{
    private const int DEFAULT_CAPACITY = 10;

    private readonly ConcurrentLinkedList<TKey> _list;
    private readonly ConcurrentDictionary<TKey, TValue> _dictionary;
    private readonly int _capacity;

    public LRUCache() : this(DEFAULT_CAPACITY)
    {
    }

    public LRUCache(int capacity)
    {
        _capacity = capacity > 0 ? capacity : DEFAULT_CAPACITY;
        _list = new ConcurrentLinkedList<TKey>();
        _dictionary = new ConcurrentDictionary<TKey, TValue>();
    }

    public ICollection<TKey> Keys => _dictionary.Keys;

    public TValue? Get(TKey key)
    {
        if (_dictionary.TryGetValue(key, out var value))
        {
            _list.Remove(key);
            _list.AddLast(key);
            return value;
        }

        return default;
    }

    public void Put(TKey key, TValue value)
    {
        if (_dictionary.TryGetValue(key, out _))
        {
            _dictionary[key] = value;
            _list.Remove(key);
            _list.AddLast(key);
        }
        else
        {
            if (_list.Count == _capacity)
            {
                _dictionary.TryRemove(_list.First!.Value, out var _);
                _list.RemoveFirst();
            }

            _list.AddLast(key);
            _dictionary.TryAdd(key, value);
        }
    }

    public bool TryPut(TKey key, TValue value)
    {
        var contains = _dictionary.ContainsKey(key);

        if (!contains)
        {
            Put(key, value);
            return true;
        }

        return false;
    }

    public TValue? PutOrGet(TKey key, TValue value)
    {
        if (!TryPut(key, value))
        {
            return Get(key);
        }

        return default;
    }

    public void Remove(TKey key)
    {
        if (_dictionary.TryGetValue(key, out _))
        {
            _dictionary.TryRemove(key, out var _);
            _list.Remove(key);
        }
    }

    public void RemoveAll(IEnumerable<TKey> keys)
    {
        Parallel.ForEach(keys, Remove);
    }

    public void Clear()
    {
        _dictionary.Clear();
        _list.Clear();
    }

    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
    {
        return _dictionary.TryGetValue(key, out value);
    }

    public bool Any(Func<TValue, bool> predicate)
    {
        return _dictionary.Values.Any(predicate);
    }

    public TValue? FirstOrDefault(Func<TValue, bool> predicate)
    {
        return _dictionary.Values.FirstOrDefault(predicate);
    }

    public IEnumerator<TValue> GetEnumerator()
    {
        return _list.Select(key => _dictionary[key]).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}