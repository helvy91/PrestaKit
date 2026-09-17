using PrestaKit.Exceptions;
using PrestaKit.Serialization;
using System.Net;

namespace PrestaKit.Clients
{
    internal abstract class PrestaShopClientBase
    {
        protected readonly HttpClient _http;
        protected readonly IPrestaShopSerializer _serializer;

        protected PrestaShopClientBase(HttpClient http, IPrestaShopSerializer serializer)
        {
            _http = http;
            _serializer = serializer;
        }

        protected PrestaShopApiException CreateApiException(HttpResponseMessage response, string? content = null)
        {
            var errors = !string.IsNullOrWhiteSpace(content) ?
                _serializer.DeserializeErrors(content) : [];

            var code = response.StatusCode;
            var body = content;
            var uri = response.RequestMessage?.RequestUri;

            return response.StatusCode switch
            {
                HttpStatusCode.NotFound => new PrestaShopNotFoundException(code, errors, body, uri),
                HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden => new PrestaShopAuthException(code, errors, body, uri),
                _ => new PrestaShopApiException(code, errors, body, uri)
            };
        }
    }
}
