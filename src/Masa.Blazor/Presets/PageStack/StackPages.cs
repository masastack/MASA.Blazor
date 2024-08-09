using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Masa.Blazor.Presets.PageStack;

internal class StackPages : IEnumerable
{
    private int _index;
    private readonly List<StackPageData> _pages = [];

    internal event EventHandler<StackPagesPushedEventArgs>? PagePushed;

    internal int Count => _pages.Count;

    internal void Push(string absolutePath)
    {
        var page = new StackPageData(absolutePath, _index++);
        Push(page);
    }

    internal void Push(StackPageData page)
    {
        PagePushed?.Invoke(this, new StackPagesPushedEventArgs(page));

        _pages.LastOrDefault()?.Deactivate();
        page.Activate();

        _pages.Add(page);
    }

    internal void Pop()
    {
        Pop(1);
    }

    internal void Pop(int count)
    {
        if (_pages.Count == 0)
        {
            return;
        }

        if (_pages.Count < count)
        {
            throw new ArgumentOutOfRangeException(nameof(count),
                "The count must be less than or equal to the number of pages.");
        }

        _pages.RemoveRange(_pages.Count - count, count);
        _pages.LastOrDefault()?.Activate();
    }

    internal void UpdateTop(string absolutePath, object? state)
    {
        if (_pages.Count == 0)
        {
            return;
        }

        var page = _pages.Last();
        page.UpdatePath(absolutePath);
        page.UpdateState(state);
    }

    internal bool TryPeek([NotNullWhen(true)] out StackPageData? page)
    {
        page = null;
        if (_pages.Count == 0)
        {
            return false;
        }

        page = _pages.Last();
        return true;
    }

    internal bool TryPeekSecondToLast([NotNullWhen(true)] out StackPageData? page)
    {
        return TryPeekByLastIndex(1, out page);
    }

    internal bool TryPeekByLastIndex(int index, [NotNullWhen(true)] out StackPageData? page)
    {
        page = null;
        if (_pages.Count - 1 < index)
        {
            return false;
        }

        page = _pages.ElementAt(_pages.Count - 1 - index);

        return true;
    }

    /// <summary>
    /// Get the number of pages between the current page and the specified page.
    /// </summary>
    /// <param name="absolutePath"></param>
    /// <returns></returns>
    internal int GetDelta(string absolutePath)
    {
        var lastIndex = _pages.FindLastIndex(x => x.AbsolutePath == absolutePath);
        return lastIndex == -1 ? -1 : _pages.Count - 1 - lastIndex;
    }

    internal void RemoveRange(int index, int count) => _pages.RemoveRange(index, count);

    internal StackPageData ElementAt(int index) => _pages.ElementAt(index);

    internal void Clear() => _pages.Clear();

    public IEnumerator GetEnumerator() => _pages.GetEnumerator();
}

internal class StackPagesPushedEventArgs(StackPageData page) : EventArgs
{
    public StackPageData Page { get; } = page;
}