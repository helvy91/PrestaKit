using PrestaKit.Entities;
using PrestaKit.Serialization;
using System.Net.Http.Headers;
using System.Text;

namespace PrestaKit.Clients.Media.Attachments
{
    internal class AttachmentFileClient : FileClientBase, IAttachmentFileClient
    {
        public AttachmentFileClient(HttpClient httpClient, IPrestaShopSerializer serializer) : base(httpClient, serializer) { }

        public async Task<byte[]> DownloadAsync(long id, CancellationToken ct = default)
        {
            using var response = await SendAsync(HttpMethod.Get, $"attachments/file/{id}", ct: ct);
            return await response.Content.ReadAsByteArrayAsync(ct);
        }

        public async Task<Attachment> ReplaceFileAsync(long id, AttachmentUpload file, CancellationToken ct = default)
        {
            var responseBody = await SendFileAsync(HttpMethod.Put, file, $"attachments/file/{id}", ct);
            return _serializer.DeserializeSingle<Attachment>(responseBody);
        }

        public async Task<Attachment> UploadAsync(AttachmentUpload file, CancellationToken ct = default)
        {
            var responseBody = await SendFileAsync(HttpMethod.Post, file, $"attachments/file", ct);
            return _serializer.DeserializeSingle<Attachment>(responseBody);
        }

        private async Task<string> SendFileAsync(
            HttpMethod method, 
            AttachmentUpload file, 
            string url, 
            CancellationToken ct = default)
        {
            using var multiPartContent = new MultipartFormDataContent();
            var streamContent = new StreamContent(file.Content);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            multiPartContent.Add(streamContent, "file", file.FileName);

            var response = await SendAsync(method, url, multiPartContent, ct);
            return await response.Content.ReadAsStringAsync(ct);
        }
    }
}
