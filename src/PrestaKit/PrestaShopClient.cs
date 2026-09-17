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
    public class PrestaShopClient : IPrestaShopClient
    {
        private readonly HttpClient _http;
        private readonly IPrestaShopSerializer _serializer = new PrestaShopXmlSerializer();
        private readonly ConcurrentDictionary<Type,object> _cachedClients = new();

        public PrestaShopClient(PrestaShopClientOptions options) : this (BuildHttpClient(options)) { }

        public PrestaShopClient(HttpClient http)
        {
            _http = http;
            Images = new ImageClient(_http, _serializer);
            Attachments = new AttachmentFileClient(_http, _serializer);
        }

        public IResourceClient<T> Resource<T>()
            where T : PrestaShopEntity
            => (IResourceClient<T>)_cachedClients.GetOrAdd(
                typeof(T), new ResourceClient<T>(_http, _serializer));

        // Fast access
        public IResourceClient<Product> Products => Resource<Product>();
        public IResourceClient<Category> Categories => Resource<Category>();
        public IResourceClient<Order> Orders => Resource<Order>();
        public IResourceClient<Customer> Customers => Resource<Customer>();
        public IResourceClient<StockAvailable> StockAvailables => Resource<StockAvailable>();
        public IResourceClient<Combination> Combinations => Resource<Combination>();
        public IResourceClient<Manufacturer> Manufacturers => Resource<Manufacturer>();
        public IResourceClient<Supplier> Suppliers => Resource<Supplier>();
        public IResourceClient<Address> Addresses => Resource<Address>();
        public IResourceClient<OrderState> OrderStates => Resource<OrderState>();

        // File clients
        public IImageClient Images { get; }
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
