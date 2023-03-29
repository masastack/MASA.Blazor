namespace Masa.Docs.Indexing
{
    public interface IIndexBuilder<TData> where TData : class
    {
        Task<bool> CreateIndexAsync(IEnumerable<TData> tData);

        IEnumerable<TData> GenerateRecords();
    }
}