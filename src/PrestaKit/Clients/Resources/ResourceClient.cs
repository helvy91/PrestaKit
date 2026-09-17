using PrestaKit.Entities.Common;
using PrestaKit.Exceptions;
using PrestaKit.Querying;
using PrestaKit.Serialization;
using System.Net;
using System.Runtime.CompilerServices;

namespace PrestaKit.Clients.Resources
{
    internal sealed class ResourceClient<T> : PrestaShopClientBase, IResourceClient<T>
        where T : PrestaShopEntity
    {
        public ResourceClient(HttpClient http, IPrestaShopSerializer serializer) : base(http, serializer)  { }

        public async Task<T> AddAsync(T entity, CancellationToken ct = default)
        {
            var serialized = _serializer.SerializeEntity(entity);
            var responseContent = await SendAsync(HttpMethod.Post, ResourceMeta<T>.Name, serialized, ct);

            return _serializer.DeserializeSingle<T>(responseContent);
        }

        public async Task<T> GetAsync(long id, CancellationToken ct = default)
        {
            var responseContent = await SendAsync(HttpMethod.Get, $"{ResourceMeta<T>.Name}/{id}", ct: ct);

            return _serializer.DeserializeSingle<T>(responseContent);
        }

        public async Task UpdateAsync(T entity, CancellationToken ct = default)
        {
            ArgumentNullException.ThrowIfNull(entity);

            if (!entity.Id.HasValue)
            {
                throw new InvalidOperationException($"Cannot update {typeof(T).Name} without an Id.");
            }

            var serialized = _serializer.SerializeEntity<T>(entity);
            _ = await SendAsync(HttpMethod.Put, $"{ResourceMeta<T>.Name}/{entity.Id.Value}", serialized, ct);
        }

        public async Task DeleteAsync(long id, CancellationToken ct = default)
        {
            _ = await SendAsync(HttpMethod.Delete, $"{ResourceMeta<T>.Name}/{id}", ct: ct);
        }

        public async Task DeleteAsync(T entity, CancellationToken ct = default)
        {
            ArgumentNullException.ThrowIfNull(entity);
            
            if (!entity.Id.HasValue)
            {
                throw new InvalidOperationException($"Cannot delete {typeof(T).Name} without an Id.");
            }

            await DeleteAsync(entity.Id.Value, ct);
        }

        public async Task<bool> ExistsAsync(long id, CancellationToken ct = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Head, $"{ResourceMeta<T>.Name}/{id}");
            var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }

            throw CreateApiException(response);
        }

        public async Task<T?> FindAsync(long id, CancellationToken ct = default)
        {
            try
            {
                return await GetAsync(id, ct);
            }
            catch (PrestaShopNotFoundException)
            {
                return null;
            }
        }

        public async IAsyncEnumerable<T> EnumerateAsync(
            Query<T> query, int pageSize = 50, [EnumeratorCancellation] CancellationToken ct = default)
        {
            var offset = 0;

            while (true)
            {
                query = query.Page(offset, pageSize);

                var pagingItems = await ListAsync(query, ct);
                foreach (var item in pagingItems)
                {
                    yield return item;
                }

                if (pagingItems.Count < pageSize)
                {
                    break;
                }

                offset += pageSize;
            }
        }

        public async Task<IReadOnlyList<T>> ListAsync(CancellationToken ct = default)
        {
            return await ListAsync(new Query<T>(), ct);
        }

        public async Task<IReadOnlyList<T>> ListAsync(Query<T> query, CancellationToken ct = default)
        {
            var responseContent = await SendAsync(
                HttpMethod.Get, 
                $"{ResourceMeta<T>.Name}?{query.ToQueryString(DisplayMode.Full)}",
                ct: ct);

            return _serializer.DeserializeList<T>(responseContent);
        }

        public async Task<IReadOnlyList<long>> ListIdsAsync(Query<T> query, CancellationToken ct = default)
        {
            var responseContent = await SendAsync(
                HttpMethod.Get,
                $"{ResourceMeta<T>.Name}?{query.ToQueryString(DisplayMode.IdsOnly)}",
                ct: ct);

            return _serializer.DeserializeIds(responseContent);
        }

        private async Task<string> SendAsync(HttpMethod method,string url, string? content = null, CancellationToken ct = default)
        {
            var request = new HttpRequestMessage(method, url);
            if (!string.IsNullOrWhiteSpace(content))
            {
                var requestContent = new StringContent(content, System.Text.Encoding.UTF8, _serializer.ContentType);
                request.Content = requestContent;
            }
            
            using var response = await _http.SendAsync(request, ct);
            var responseContent = await response.Content.ReadAsStringAsync(ct);
            if (!response.IsSuccessStatusCode)
            {
                throw CreateApiException(response, responseContent);
            }

            return responseContent;
        }
    }
}
