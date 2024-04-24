using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Masa.Blazor.Presets.PageStack;

internal class StackPages : IEnumerable
{
    private List<StackPageData> _pages = new();

    internal event EventHandler<StackPagesPushedEventArgs>? PagePushed;

    internal int Count => _pages.Count;

    internal void Push(string absolutePath)
    {
        var page = new StackPageData(absolutePath);
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
        if (_pages.Count == 0)
        {
            return;
        }

        _pages.RemoveAt(_pages.Count - 1);

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
        page = null;
        if (_pages.Count < 2)
        {
            return false;
        }

        page = _pages.ElementAt(_pages.Count - 2);
        return true;
    }

    internal StackPageData ElementAt(int index) => _pages.ElementAt(index);

    internal void Clear() => _pages.Clear();

    public IEnumerator GetEnumerator() => _pages.GetEnumerator();
}

internal class StackPagesPushedEventArgs : EventArgs
{
    public StackPagesPushedEventArgs(StackPageData page)
    {
        Page = page;
    }

    public StackPageData Page { get; }
}