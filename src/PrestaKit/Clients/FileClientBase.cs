using PrestaKit.Serialization;

namespace PrestaKit.Clients
{
    internal abstract class FileClientBase : PrestaShopClientBase
    {
        protected FileClientBase(HttpClient http, IPrestaShopSerializer serializer) : base(http, serializer) { }

        protected async Task<HttpResponseMessage> SendAsync(
            HttpMethod method, string url, HttpContent? content = null, CancellationToken ct = default)
        {
            var request = new HttpRequestMessage(method, url);
            if (content != null)
            {
                request.Content = content;
            }

            var response = await _http.SendAsync(request, ct);
            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync(ct);
                response.Dispose();

                throw CreateApiException(response, responseContent);
            }

            return response;
        }
    }
}
