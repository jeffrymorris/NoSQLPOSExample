using System.Collections.Generic;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.IO;

namespace NoSQLPOSExample.Infrastructure
{
    public interface IRepository
    {
        Task<ResponseStatus> InsertAsync<T>(Document<T> document) where T : Document<T>;
        Task<ResponseStatus> UpsertAsync<T>(Document<T> document) where T : Document<T>;
        Task<T> GetAsync<T>(string key) where T : Document<T>;
        Task<ResponseStatus> RemoveAsync<T>(string key) where T : Document<T>;
        Task<IEnumerable<T>> QueryAsync<T>(string query);
    }
}