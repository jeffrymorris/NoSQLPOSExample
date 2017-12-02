using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Configuration.Client;
using Couchbase.Core;
using Couchbase.IO;
using Couchbase.N1QL;
using NoSQLPOSExample.Models;

namespace NoSQLPOSExample.Infrastructure
{
    public class Repository : IRepository 
    {
        private readonly IBucket _bucket;

        public Repository(IPosBucketProvider provider)
        {
            _bucket = provider.GetBucket();
        }

        public async Task<ResponseStatus> InsertAsync<T>(Document<T> document) where T : Document<T>
        {
            var result = await _bucket.InsertAsync(document);
            if (!result.Success)
            {
                //perhaps log reason for failure?
                if (result.Exception != null)
                {
                    throw result.Exception;
                }
            }
            return result.Status;
        }

        public async Task<ResponseStatus> UpsertAsync<T>(Document<T> document) where T : Document<T>
        {
            var result = await _bucket.UpsertAsync(document);
            if (result.Success) return result.Status;

            //perhaps log reason for failure?
            if (result.Exception != null)
            {
                throw result.Exception;
            }
            return result.Status;
        }

        public async Task<T> GetAsync<T>(string key) where T : Document<T>
        {
            var result = await _bucket.GetDocumentAsync<T>(key);
            if (result.Success) return result.Document.Content;

            //perhaps log reason for failure?
            if (result.Exception != null)
            {
                throw result.Exception;
            }
            return result.Document.Content;
        }

        public async Task<ResponseStatus> RemoveAsync<T>(string key) where T : Document<T>
        {
            var result = await _bucket.RemoveAsync(key);

            if (result.Success) return result.Status;

            //perhaps log reason for failure?
            if (result.Exception != null)
            {
                throw result.Exception;
            }
            return result.Status;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string statement)
        {
            var request = new QueryRequest(statement).UseStreaming(false);
            var response = await _bucket.QueryAsync<T>(request);

            if (response.Success) return response.Rows;

            if (response.Exception != null)
            {
                throw response.Exception;
            }
            throw new InvalidOperationException(response.Status.ToString());
        }
    }
}
