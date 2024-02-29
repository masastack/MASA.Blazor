
using System.Collections;

namespace Masa.Blazor.Utils;

/// <summary>
/// 线程安全的LinkedList
/// </summary>
/// <typeparam name="T"></typeparam>
public class ConcurrentLinkedList<T> : ICollection<T>
{
    private readonly LinkedList<T> _list = new();

    /// <inheritdoc cref="LinkedList{T}.First"/>
    public LinkedListNode<T>? First
    {
        get
        {
            lock (((ICollection)_list).SyncRoot)
            {
                return _list.First;
            }
        }
    }


    /// <inheritdoc cref="LinkedList{T}.Count"/>
    public int Count
    {
        get
        {
            lock (((ICollection)_list).SyncRoot)
            {
                return _list.Count;
            }
        }
    }

    /// <inheritdoc/>
    public bool IsReadOnly => false;

    /// <inheritdoc/>
    public void Add(T item)
    {
        AddFirst(item);
    }

    /// <inheritdoc cref="LinkedList{T}.AddLast(T)"/>
    public void AddLast(T value)
    {
        lock (((ICollection)_list).SyncRoot)
        {
            _list.AddLast(value);
        }
    }
    public void RemoveFirst()
    {
        lock (((ICollection)_list).SyncRoot)
        {
            _list.RemoveFirst();
        }
    }
    /// <summary>
    /// <inheritdoc cref="LinkedList{T}.Clear"/>
    /// </summary>
    public void Clear()
    {
        lock (((ICollection)_list).SyncRoot)
        {
            _list.Clear();
        }
    }

    /// <inheritdoc/>
    public bool Contains(T item)
    {
        lock (((ICollection)_list).SyncRoot)
        {
            return _list.Contains(item);
        }
    }

    /// <inheritdoc/>
    public void CopyTo(T[] array, int arrayIndex)
    {
        lock (((ICollection)_list).SyncRoot)
        {
            _list.CopyTo(array, arrayIndex);
        }
    }

    /// <inheritdoc/>
    public IEnumerator<T> GetEnumerator()
    {
        lock (((ICollection)_list).SyncRoot)
        {
            return _list.ToList().GetEnumerator();
        }
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        lock (((ICollection)_list).SyncRoot)
        {
            return GetEnumerator();
        }
    }

    /// <inheritdoc/>
    public bool Remove(T item)
    {
        lock (((ICollection)_list).SyncRoot)
        {
            return _list.Remove(item);
        }
    }

    /// <inheritdoc cref="LinkedList{T}.AddFirst(T)"/>
    private void AddFirst(T value)
    {
        lock (((ICollection)_list).SyncRoot)
        {
            _list.AddFirst(value);
        }
    }
}