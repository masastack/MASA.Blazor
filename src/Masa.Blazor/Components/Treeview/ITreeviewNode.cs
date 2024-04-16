namespace Masa.Blazor;

public interface ITreeviewNode<TItem, TKey> : ITreeviewBase<TItem, TKey>
{
    TKey Key { get; }
}