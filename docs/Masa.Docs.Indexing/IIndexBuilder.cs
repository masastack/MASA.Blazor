using Masa.Docs.Indexing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masa.Docs.Indexing
{
    public interface IIndexBuilder<TData> where TData : class
    {
        Task<bool> CreateIndexAsync(IEnumerable<TData>? datas = null);

        IEnumerable<TData> GenerateRecords();
    }
}