using PrestaKit.Entities.Common;
using PrestaKit.Exceptions;
using PrestaKit.Querying;

namespace PrestaKit.Clients.Resources
{
    /// <summary>
    /// Typed client for a single PrestaShop resource (for example products or
    /// categories). Obtain one from <see cref="IPrestaShopClient.Resource{T}"/> or a
    /// convenience property such as <c>client.Products</c>.
    /// </summary>
    /// <typeparam name="T">The resource entity type.</typeparam>
    public interface IResourceClient<T> where T : PrestaShopEntity
    {
        /// <summary>Retrieves a single resource by its id.</summary>
        /// <param name="id">The resource id.</param>
        /// <param name="ct">A token to cancel the request.</param>
        /// <returns>The resource.</returns>
        /// <exception cref="PrestaShopNotFoundException">The resource does not exist.</exception>
        /// <exception cref="PrestaShopAuthException">The API key is missing or unauthorized.</exception>
        /// <exception cref="PrestaShopApiException">The request failed for another reason.</exception>
        /// <remarks>
        /// Use <see cref="FindAsync"/> instead if a missing resource is an expected,
        /// non-exceptional outcome.
        /// </remarks>
        Task<T> GetAsync(long id, CancellationToken ct = default);

        /// <summary>
        /// Retrieves a single resource by its id, or <see langword="null"/> if it does
        /// not exist.
        /// </summary>
        /// <param name="id">The resource id.</param>
        /// <param name="ct">A token to cancel the request.</param>
        /// <returns>The resource, or <see langword="null"/> if not found.</returns>
        /// <exception cref="PrestaShopAuthException">The API key is missing or unauthorized.</exception>
        /// <exception cref="PrestaShopApiException">The request failed for a reason other than not-found.</exception>
        /// <remarks>
        /// Unlike <see cref="GetAsync"/>, a missing resource returns <see langword="null"/>
        /// rather than throwing. Other failures (auth, server errors) still throw.
        /// </remarks>
        Task<T?> FindAsync(long id, CancellationToken ct = default);

        /// <summary>Determines whether a resource with the given id exists.</summary>
        /// <param name="id">The resource id.</param>
        /// <param name="ct">A token to cancel the request.</param>
        /// <returns><see langword="true"/> if the resource exists; otherwise <see langword="false"/>.</returns>
        /// <exception cref="PrestaShopAuthException">The API key is missing or unauthorized.</exception>
        /// <exception cref="PrestaShopApiException">The request failed for a reason other than not-found.</exception>
        /// <remarks>Uses a lightweight HEAD request and does not transfer the entity body.</remarks>
        Task<bool> ExistsAsync(long id, CancellationToken ct = default);

        /// <summary>Retrieves all resources.</summary>
        /// <param name="ct">A token to cancel the request.</param>
        /// <returns>All resources, or an empty list if there are none.</returns>
        /// <exception cref="PrestaShopAuthException">The API key is missing or unauthorized.</exception>
        /// <exception cref="PrestaShopApiException">The request failed for another reason.</exception>
        /// <remarks>
        /// Fetches every resource in a single request. For large resources such as
        /// products or orders, prefer <see cref="EnumerateAsync"/> to page results and
        /// avoid loading the entire set into memory.
        /// </remarks>
        Task<IReadOnlyList<T>> ListAsync(CancellationToken ct = default);

        /// <summary>Retrieves the resources matching the given query.</summary>
        /// <param name="query">Filters, ordering, and paging to apply.</param>
        /// <param name="ct">A token to cancel the request.</param>
        /// <returns>The matching resources, or an empty list if none match.</returns>
        /// <exception cref="PrestaShopApiException">The request failed.</exception>
        /// <exception cref="PrestaShopAuthException">The API key is missing or unauthorized.</exception>
        Task<IReadOnlyList<T>> ListAsync(Query<T> query, CancellationToken ct = default);

        /// <summary>Retrieves only the ids of the resources matching the given query.</summary>
        /// <param name="query">Filters, ordering, and paging to apply.</param>
        /// <param name="ct">A token to cancel the request.</param>
        /// <returns>The ids of the matching resources, or an empty list if none match.</returns>
        /// <exception cref="PrestaShopApiException">The request failed.</exception>
        /// <exception cref="PrestaShopAuthException">The API key is missing or unauthorized.</exception>
        /// <remarks>Cheaper than <see cref="ListAsync(Query{T}, CancellationToken)"/> when only ids are needed.</remarks>
        Task<IReadOnlyList<long>> ListIdsAsync(Query<T> query, CancellationToken ct = default);

        /// <summary>
        /// Streams the resources matching the given query, fetching one page at a time.
        /// </summary>
        /// <param name="query">Filters and ordering to apply. Paging on the query is ignored; use <paramref name="pageSize"/>.</param>
        /// <param name="pageSize">The number of resources fetched per request.</param>
        /// <param name="ct">A token to cancel the enumeration.</param>
        /// <returns>An asynchronous sequence of the matching resources.</returns>
        /// <exception cref="PrestaShopApiException">A page request failed.</exception>
        /// <exception cref="PrestaShopAuthException">The API key is missing or unauthorized.</exception>
        /// <remarks>
        /// Only one page is held in memory at a time, making this suitable for large
        /// resources. Because paging is position-based, a stable sort order (for
        /// example by id) is recommended to avoid skipped or duplicated results if the
        /// data changes during enumeration.
        /// </remarks>
        IAsyncEnumerable<T> EnumerateAsync(Query<T> query, int pageSize = 50, CancellationToken ct = default);

        /// <summary>Creates a new resource.</summary>
        /// <param name="entity">The resource to create. Its id is assigned by the server.</param>
        /// <param name="ct">A token to cancel the request.</param>
        /// <returns>The created resource, including its server-assigned id.</returns>
        /// <exception cref="PrestaShopApiException">The request failed, for example due to a validation error.</exception>
        /// <exception cref="PrestaShopAuthException">The API key is missing or unauthorized.</exception>
        /// <remarks>Read-only and server-managed fields are omitted from the request automatically.</remarks>
        Task<T> AddAsync(T entity, CancellationToken ct = default);

        /// <summary>Updates an existing resource.</summary>
        /// <param name="entity">The resource to update. Its <see cref="PrestaShopEntity.Id"/> must be set.</param>
        /// <param name="ct">A token to cancel the request.</param>
        /// <exception cref="ArgumentNullException">The entity has no id.</exception>
        /// <exception cref="PrestaShopApiException">The request failed, for example due to a validation error.</exception>
        /// <exception cref="PrestaShopAuthException">The API key is missing or unauthorized.</exception>
        /// <remarks>Read-only and server-managed fields are omitted from the request automatically.</remarks>
        Task UpdateAsync(T entity, CancellationToken ct = default);

        /// <summary>Deletes a resource by its id.</summary>
        /// <param name="id">The id of the resource to delete.</param>
        /// <param name="ct">A token to cancel the request.</param>
        /// <exception cref="PrestaShopNotFoundException">The resource does not exist.</exception>
        /// <exception cref="PrestaShopApiException">The request failed for another reason.</exception>
        Task DeleteAsync(long id, CancellationToken ct = default);

        /// <summary>Deletes the given resource.</summary>
        /// <param name="entity">The resource to delete. Its <see cref="PrestaShopEntity.Id"/> must be set.</param>
        /// <param name="ct">A token to cancel the request.</param>
        /// <exception cref="ArgumentNullException">The entity has no id.</exception>
        /// <exception cref="PrestaShopNotFoundException">The resource does not exist.</exception>
        /// <exception cref="PrestaShopApiException">The request failed for another reason.</exception>
        Task DeleteAsync(T entity, CancellationToken ct = default);
    }
}
