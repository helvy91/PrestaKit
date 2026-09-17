using PrestaKit.Clients.Resources;
using PrestaKit.Entities.Common;
using PrestaKit.Exceptions;
using PrestaKit.Serialization;
using System.Net;
using System.Net.Http.Headers;
using System.Xml.Linq;

namespace PrestaKit.Clients.Media.Images
{
    internal class ImageClient : FileClientBase, IImageClient
    {
        public ImageClient(HttpClient http, IPrestaShopSerializer serializer) : base(http, serializer) { }

        public async Task DeleteAsync<T>(long resourceId, long imageId, CancellationToken ct = default) 
            where T : PrestaShopEntity, IHasImages
        {
            var _  = await SendAsync(
                HttpMethod.Delete, $"images/{ResourceMeta<T>.Name}/{resourceId}/{imageId}", null, ct);
        }

        public async Task<byte[]> DownloadAsync<T>(long resourceId, long imageId, CancellationToken ct = default) 
            where T : PrestaShopEntity, IHasImages
        {
            using var response = await SendAsync(
                HttpMethod.Get, $"images/{ResourceMeta<T>.Name}/{resourceId}/{imageId}", null, ct);
            return await response.Content.ReadAsByteArrayAsync(ct);
        }

        public async Task<long> UploadAsync<T>(long resourceId, ImageUpload image, CancellationToken ct = default)
            where T : PrestaShopEntity, IHasImages
        {
            using var multiPartContent = new MultipartFormDataContent();
            var streamContent = new StreamContent(image.Content);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(image.ContentType);
            multiPartContent.Add(streamContent, "image", image.FileName);

            using var response = await SendAsync(HttpMethod.Post, $"images/{ResourceMeta<T>.Name}/{resourceId}", multiPartContent, ct);
            var body = await response.Content.ReadAsStringAsync(ct);

            return ParseImageId(body);
        }

        public async Task<bool> HasImageAsync<T>(long resourceId, CancellationToken ct = default) 
            where T : PrestaShopEntity, IHasImages
        {
            var request = new HttpRequestMessage(HttpMethod.Head, $"images/{ResourceMeta<T>.Name}/{resourceId}");
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
            
            var content = await response.Content.ReadAsStringAsync(ct);
            throw CreateApiException(response, content);
        }

        public async Task<IReadOnlyList<long>> ListIdsAsync<T>(long resourceId, CancellationToken ct = default) 
            where T : PrestaShopEntity, IHasImages
        {
            using var response = await SendAsync(HttpMethod.Get, $"images/{ResourceMeta<T>.Name}/{resourceId}", ct: ct);
            var body = await response.Content.ReadAsStringAsync(ct); 

            return _serializer.DeserializeIds(body);
        }

        private static long ParseImageId(string body)
        {
            var image = XDocument.Parse(body).Root?.Elements().FirstOrDefault(); 
            var idElement = image?.Elements().FirstOrDefault(e => e.Name.LocalName == "id");

            if (idElement is null || !long.TryParse(idElement.Value, out var id))
            {
                throw new PrestaShopSerializationException(
                    "Image upload response had no parseable <id>.", body);
            }

            return id;
        }
    }
}
