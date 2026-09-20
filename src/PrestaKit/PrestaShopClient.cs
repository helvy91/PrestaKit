using PrestaKit.Clients.Media.Attachments;
using PrestaKit.Clients.Media.Images;
using PrestaKit.Clients.Resources;
using PrestaKit.Entities;
using PrestaKit.Entities.Common;
using PrestaKit.Serialization;
using System.Collections.Concurrent;
using System.Net.Http.Headers;
using System.Text;

namespace PrestaKit
{
    /// <inheritdoc/>
    public class PrestaShopClient : IPrestaShopClient
    {
        private readonly HttpClient _http;
        private readonly IPrestaShopSerializer _serializer = new PrestaShopXmlSerializer();
        private readonly ConcurrentDictionary<Type,object> _cachedClients = new();

        /// <summary>
        /// Creates a client that manages its own <see cref="HttpClient"/>, configured from
        /// the given options.
        /// </summary>
        /// <param name="options">The shop base URL and API key.</param>
        /// <remarks>
        /// Prefer the dependency-injection registration (<c>AddPrestaShopClient</c>) or the
        /// <see cref="PrestaShopClient(HttpClient)"/> constructor in long-running
        /// applications, so the <see cref="HttpClient"/> lifetime is managed for you. This
        /// constructor is convenient for scripts and short-lived processes.
        /// </remarks>
        public PrestaShopClient(PrestaShopClientOptions options) : this (BuildHttpClient(options)) { }


        /// <summary>
        /// Creates a client that uses the supplied <see cref="HttpClient"/>. The caller owns
        /// its configuration and lifetime.
        /// </summary>
        /// <param name="http">
        /// An <see cref="HttpClient"/> whose base address points at the shop's <c>/api/</c>
        /// endpoint and whose authorization header carries the API key.
        /// </param>
        public PrestaShopClient(HttpClient http)
        {
            _http = http;
            Images = new ImageClient(_http, _serializer);
            Attachments = new AttachmentFileClient(_http, _serializer);
        }

        /// <inheritdoc/>
        public IResourceClient<T> Resource<T>()
            where T : PrestaShopEntity
            => (IResourceClient<T>)_cachedClients.GetOrAdd(
                typeof(T), new ResourceClient<T>(_http, _serializer));

        // Fast access

        /// <inheritdoc/>
        public IResourceClient<Product> Products => Resource<Product>();

        /// <inheritdoc/>
        public IResourceClient<Category> Categories => Resource<Category>();

        /// <inheritdoc/>
        public IResourceClient<Order> Orders => Resource<Order>();

        /// <inheritdoc/>
        public IResourceClient<Customer> Customers => Resource<Customer>();

        /// <inheritdoc/>
        public IResourceClient<StockAvailable> StockAvailables => Resource<StockAvailable>();

        /// <inheritdoc/>
        public IResourceClient<Combination> Combinations => Resource<Combination>();

        /// <inheritdoc/>
        public IResourceClient<Manufacturer> Manufacturers => Resource<Manufacturer>();

        /// <inheritdoc/>
        public IResourceClient<Supplier> Suppliers => Resource<Supplier>();

        /// <inheritdoc/>
        public IResourceClient<Address> Addresses => Resource<Address>();

        /// <inheritdoc/>
        public IResourceClient<OrderState> OrderStates => Resource<OrderState>();

        // File clients

        /// <inheritdoc/>
        public IImageClient Images { get; }

        /// <inheritdoc/>
        public IAttachmentFileClient Attachments { get; }

        private static HttpClient BuildHttpClient(PrestaShopClientOptions options)
        {
            ArgumentNullException.ThrowIfNull(options);
            if (options.BaseUrl == null)
            {
                throw new ArgumentException($"Parameter {nameof(options.BaseUrl)} is required.", nameof(options));
            }
            if (string.IsNullOrEmpty(options.ApiKey))
            {
                throw new ArgumentException($"Parameter {nameof(options.ApiKey)} is required.", nameof(options));
            }

            var httpClient = new HttpClient();
            httpClient.BaseAddress = NormalizeUrl(options.BaseUrl);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{options.ApiKey}:")));
            httpClient.Timeout = options.Timeout;

            return httpClient;
        }

        private static Uri NormalizeUrl(Uri baseUrl)
        {
            var url = baseUrl.AbsoluteUri;
            return url.EndsWith('/') ? baseUrl : new Uri(url + '/');
        }
    }
}
