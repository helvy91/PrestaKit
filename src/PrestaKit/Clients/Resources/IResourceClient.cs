using PrestaKit.Entities.Common;
using PrestaKit.Querying;

namespace PrestaKit.Clients.Resources
{
    public interface IResourceClient<T> where T : PrestaShopEntity
    {
        Task<T> GetAsync(long id, CancellationToken ct = default);
        Task<T?> FindAsync(long id, CancellationToken ct = default);     
        Task<bool> ExistsAsync(long id, CancellationToken ct = default);  

        Task<IReadOnlyList<T>> ListAsync(CancellationToken ct = default);
        Task<IReadOnlyList<T>> ListAsync(Query<T> query, CancellationToken ct = default);
        Task<IReadOnlyList<long>> ListIdsAsync(Query<T> query, CancellationToken ct = default);

        IAsyncEnumerable<T> EnumerateAsync(Query<T> query, int pageSize = 50, CancellationToken ct = default);

        Task<T> AddAsync(T entity, CancellationToken ct = default);
        Task UpdateAsync(T entity, CancellationToken ct = default);
        Task DeleteAsync(long id, CancellationToken ct = default);
        Task DeleteAsync(T entity, CancellationToken ct = default);
    }
}
