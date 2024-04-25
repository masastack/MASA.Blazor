namespace Masa.Blazor;

public interface ITreeview<TItem, TKey> : ITreeviewBase<TItem, TKey>
{
    bool OpenAll { get; }

    void AddNode(ITreeviewNode<TItem, TKey> node);

    void UpdateActive(TKey key, bool isActive);

    void UpdateSelected(TKey key, bool isSelected);

    void UpdateOpen(TKey key);

    bool IsSelected(TKey key);

    bool IsIndeterminate(TKey key);

    bool IsActive(TKey key);

    bool IsOpen(TKey key);

    bool IsExcluded(TKey key);

    Task EmitActiveAsync();

    Task EmitOpenAsync();

    Task EmitSelectedAsync();
}