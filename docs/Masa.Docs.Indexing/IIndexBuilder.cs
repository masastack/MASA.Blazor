namespace Masa.Docs.Indexing
{
    public interface IIndexBuilder<TData> where TData : class
    {
        Task<bool> CreateIndexAsync(IEnumerable<TData>? datas = null);

        IEnumerable<TData> GenerateRecords();
    }
}