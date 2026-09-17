using PrestaKit.Clients.Media.Attachments;
using PrestaKit.Exceptions;
using PrestaKit.IntegrationTests.Fixtures;
using Shouldly;
using System.Text;

namespace PrestaKit.IntegrationTests
{
    public class AttachmentFileIntegrationTests : IntegrationTestBase
    {
        public AttachmentFileIntegrationTests(PrestaShopContainerFixture fixture) : base(fixture) { }

        [Fact]
        public async Task CanUploadAttachment()
        {
            using var stream = new MemoryStream(SampleFileBytes);
            var upload = new AttachmentUpload
            {
                Content = stream,
                FileName = $"doc.pdf"
            };

            var attachment = await Client.Attachments.UploadAsync(upload);

            attachment.Id.ShouldNotBeNull();
            attachment.Id!.Value.ShouldBeGreaterThan(0);
            attachment.Name!.Values[0].Value.ShouldBe(upload.FileName);
        }

        [Fact]
        public async Task CanDownloadAttachment()
        {
            using var stream = new MemoryStream(SampleFileBytes);
            var upload = new AttachmentUpload
            {
                Content = stream,
                FileName = $"doc.pdf"
            };

            var attachment = await Client.Attachments.UploadAsync(upload);
            var bytes = await Client.Attachments.DownloadAsync(attachment.Id!.Value);

            bytes.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task CanReplaceAttachmentFile()
        {
            using var uploadStream = new MemoryStream(SampleFileBytes);
            var upload = new AttachmentUpload
            {
                Content = uploadStream,
                FileName = $"original.pdf"
            };

            var attachment = await Client.Attachments.UploadAsync(upload);

            using var replaceStream = new MemoryStream(SampleFileBytes);
            var replacement = new AttachmentUpload
            {
                Content = replaceStream,
                FileName = $"replaced.pdf"
            };

            var replaced = await Client.Attachments.ReplaceFileAsync(attachment.Id!.Value, replacement);

            replaced.Id.ShouldBe(attachment.Id);
            replaced.FileName.ShouldNotBe(attachment.FileName);
        }

        [Fact]
        public async Task DownloadAttachmentThrowsWhenAttachmentDoesNotExist()
        {
            await Should.ThrowAsync<PrestaShopApiException>(
                () => Client.Attachments.DownloadAsync(999999));
        }

        private static byte[] SampleFileBytes => Encoding.ASCII.GetBytes(
            "%PDF-1.4\n" +
            "1 0 obj<</Type/Catalog/Pages 2 0 R>>endobj\n" +
            "2 0 obj<</Type/Pages/Kids[3 0 R]/Count 1>>endobj\n" +
            "3 0 obj<</Type/Page/Parent 2 0 R/MediaBox[0 0 612 792]>>endobj\n" +
            "xref\n0 4\n0000000000 65535 f \n" +
            "trailer<</Size 4/Root 1 0 R>>\n" +
            "startxref\n0\n%%EOF");
    }
}