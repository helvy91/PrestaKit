using PrestaKit.Clients.Media.Attachments;
using PrestaKit.Entities;
using PrestaKit.Exceptions;
using PrestaKit.Serialization;
using Shouldly;
using System.Net;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace PrestaKit.Tests.Clients
{
    [Trait("Category", "Unit")]
    public class AttachmentFileClientTests : IDisposable
    {
        private readonly AttachmentFileClient _sut;
        private readonly WireMockServer _server;
        private readonly HttpClient _http;

        public AttachmentFileClientTests()
        {
            _server = WireMockServer.Start();
            _http = new HttpClient() { BaseAddress = new Uri(_server.Url + "/api/") };
            _sut = new AttachmentFileClient(_http, new PrestaShopXmlSerializer());
        }

        [Fact]
        public async Task DownloadAsync_ShouldGetFromFileUrlAndReturnBytes()
        {
            // Arrange
            const int attachmentId = 7;
            var fileBytes = new byte[] { 0x25, 0x50, 0x44, 0x46 }; // "%PDF"

            _server
                .Given(Request.Create().WithPath($"/api/attachments/file/{attachmentId}").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(200).WithBody(fileBytes));

            // Act
            var response = await _sut.DownloadAsync(attachmentId);

            // Assert
            response.ShouldBe(fileBytes);
        }

        [Fact]
        public async Task UploadAsync_ShouldPostToFileUrlAndReturnAttachment()
        {
            // Arrange
            const int attachmentId = 12;

            using var stream = new MemoryStream([0x25, 0x50, 0x44, 0x46]);
            var upload = new AttachmentUpload() { Content = stream, FileName = "manual.pdf" };

            _server
                .Given(Request.Create().WithPath("/api/attachments/file").UsingPost())
                .RespondWith(Response.Create().WithStatusCode(200).WithBody(
                    $"""
                    <prestashop>
                      <attachment>
                        <id>{attachmentId}</id>
                        <file_name>manual.pdf</file_name>
                        <mime>application/pdf</mime>
                      </attachment>
                    </prestashop>
                    """));

            // Act
            var response = await _sut.UploadAsync(upload);

            // Assert
            response.Id.ShouldBe(attachmentId);
            response.FileName.ShouldBe("manual.pdf");
            response.Mime.ShouldBe("application/pdf");
        }

        [Fact]
        public async Task UploadAsync_WhenResponseIsNotParseable_ShouldThrowSerializationException()
        {
            // Arrange
            using var stream = new MemoryStream([0x25, 0x50, 0x44, 0x46]);
            var upload = new AttachmentUpload() { Content = stream, FileName = "manual.pdf" };

            _server
                .Given(Request.Create().WithPath("/api/attachments/file").UsingPost())
                .RespondWith(Response.Create().WithStatusCode(200).WithBody("<prestashop><attachment</prestashop>"));

            // Act & Assert
            await Should.ThrowAsync<PrestaShopSerializationException>(
                () => _sut.UploadAsync(upload));
        }

        [Fact]
        public async Task ReplaceFileAsync_ShouldPutToFileUrlAndReturnAttachment()
        {
            // Arrange
            const int attachmentId = 7;

            using var stream = new MemoryStream([0x25, 0x50, 0x44, 0x46]);
            var upload = new AttachmentUpload() { Content = stream, FileName = "updated.pdf" };

            _server
                .Given(Request.Create().WithPath($"/api/attachments/file/{attachmentId}").UsingPut())
                .RespondWith(Response.Create().WithStatusCode(200).WithBody(
                    $"""
                    <prestashop>
                      <attachment>
                        <id>{attachmentId}</id>
                        <file_name>updated.pdf</file_name>
                      </attachment>
                    </prestashop>
                    """));

            // Act
            var response = await _sut.ReplaceFileAsync(attachmentId, upload);

            // Assert
            response.Id.ShouldBe(attachmentId);
            response.FileName.ShouldBe("updated.pdf");
        }

        [Fact]
        public async Task DownloadAsync_WhenApiReturns404_ShouldThrowNotFoundException()
        {
            // Arrange
            const int attachmentId = 999;

            _server
                .Given(Request.Create().WithPath($"/api/attachments/file/{attachmentId}").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(404));

            // Act & Assert
            await Should.ThrowAsync<PrestaShopNotFoundException>(
                () => _sut.DownloadAsync(attachmentId));
        }

        [Fact]
        public async Task DownloadAsync_WhenApiReturns401_ShouldThrowAuthException()
        {
            // Arrange
            const int attachmentId = 7;

            _server
                .Given(Request.Create().WithPath($"/api/attachments/file/{attachmentId}").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(401));

            // Act & Assert
            await Should.ThrowAsync<PrestaShopAuthException>(
                () => _sut.DownloadAsync(attachmentId));
        }

        [Fact]
        public async Task DownloadAsync_WhenApiReturns500_ShouldThrowApiException()
        {
            // Arrange
            const int attachmentId = 7;

            _server
                .Given(Request.Create().WithPath($"/api/attachments/file/{attachmentId}").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(500));

            // Act & Assert
            await Should.ThrowAsync<PrestaShopApiException>(
                () => _sut.DownloadAsync(attachmentId));
        }

        [Fact]
        public async Task UploadAsync_WhenApiReturnsErrors_ShouldThrowExceptionWithErrors()
        {
            // Arrange
            using var stream = new MemoryStream([0x25, 0x50, 0x44, 0x46]);
            var upload = new AttachmentUpload() { Content = stream, FileName = "manual.pdf" };

            _server.Given(Request.Create().WithPath("/api/attachments/file").UsingPost())
               .RespondWith(Response.Create().WithStatusCode(400)
                   .WithBody("<prestashop><errors><error><code>89</code><message>Invalid</message></error></errors></prestashop>"));

            // Act
            var ex = await Should.ThrowAsync<PrestaShopApiException>(() => _sut.UploadAsync(upload));

            // Assert
            ex.HttpStatusCode.ShouldBe(HttpStatusCode.BadRequest);
            ex.Errors.ShouldHaveSingleItem();
            ex.Errors[0].Code.ShouldBe(89);
            ex.Errors[0].Message.ShouldBe("Invalid");
            ex.ResponseBody!.ShouldContain("<errors>");
            ex.RequestUri!.ToString().ShouldContain("/api/attachments/file");
        }

        public void Dispose()
        {
            _http.Dispose();
            _server.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}