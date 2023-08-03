namespace Masa.Docs.Indexing
{
    public interface IIndexBuilder<TData> where TData : class
    {
        Task CreateIndexAsync(IEnumerable<TData> tData, CancellationToken ct = default);

        IEnumerable<TData> GenerateRecords();
    }
}