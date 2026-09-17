using PrestaKit.Clients.Media.Images;
using PrestaKit.Clients.Resources;
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
    public class ImageClientTests : IDisposable
    {
        private readonly ImageClient _sut;
        private readonly WireMockServer _server;
        private readonly HttpClient _http;

        public ImageClientTests()
        {
            _server = WireMockServer.Start();
            _http = new HttpClient() { BaseAddress = new Uri(_server.Url + "/api/") };
            _sut = new ImageClient(_http, new PrestaShopXmlSerializer());
        }

        [Fact]
        public async Task DeleteAsync_ShouldPostToCollectionUrl()
        {
            // Arrange
            const int productId = 42;
            const int imageId = 2;

            _server
                .Given(Request.Create().WithPath($"/api/images/products/{productId}/{imageId}").UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(200));


            // Act & Assert
            await _sut.DeleteAsync<Product>(productId, imageId);
        }

        [Fact]
        public async Task DownloadAsync_ShouldPostToCollectionUrlAndReturnBytes()
        {
            // Arrange
            const int productId = 42;
            const int imageId = 2;
            var fileBytes = new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 };

            _server
                .Given(Request.Create().WithPath($"/api/images/products/{productId}/{imageId}").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(200).WithBody(fileBytes));

            // Act
            var response = await _sut.DownloadAsync<Product>(productId, imageId);

            // Assert
            response.ShouldBe(fileBytes);
        }

        [Fact]
        public async Task UploadAsync_ShouldPostToCollectionUrlAndReturnId()
        {
            // Arrange
            const int productId = 42;
            const int imageId = 12;

            using var stream = new MemoryStream([0xFF, 0xD8, 0xFF, 0xE0]);
            var imageUpload = new ImageUpload() { Content = stream, FileName = "test.jpg" };

            _server
                .Given(Request.Create().WithPath($"/api/images/products/{productId}").UsingPost())
                .RespondWith(Response.Create().WithStatusCode(200).WithBody(
                    $"""
                    <prestashop>
                      <image>
                        <id>{imageId}</id>
                      </image>
                    </prestashop>
                    """));

            // Act
            var response = await _sut.UploadAsync<Product>(productId, imageUpload);

            // Assert
            response.ShouldBe(imageId);
        }

        [Fact]
        public async Task UploadAsync_WhenResponseIsNotParseable_ShouldThrowSerializationException()
        {
            // Arrange
            const int productId = 42;

            using var stream = new MemoryStream([0xFF, 0xD8, 0xFF, 0xE0]);
            var imageUpload = new ImageUpload() { Content = stream, FileName = "test.jpg" };

            _server
                .Given(Request.Create().WithPath($"/api/images/products/{productId}").UsingPost())
                .RespondWith(Response.Create().WithStatusCode(200).WithBody(
                    $"""
                    <prestashop>
                      <image>
                        <id>twelve</id>
                      </image>
                    </prestashop>
                    """));

            // Act & Assert
            await Should.ThrowAsync(
                () => _sut.UploadAsync<Product>(productId, imageUpload), typeof(PrestaShopSerializationException));
        }

        [Fact]
        public async Task HasImageAsync_ShouldPostToCollectionUrlAndReturnTrue()
        {
            // Arrange
            const int productId = 42;

            _server
                .Given(Request.Create().WithPath($"/api/images/products/{productId}").UsingHead())
                .RespondWith(Response.Create().WithStatusCode(200));

            // Act
            var response = await _sut.HasImageAsync<Product>(productId);

            // Assert
            response.ShouldBeTrue();
        }

        [Fact]
        public async Task HasImageAsync_ShouldPostToCollectionUrlAndReturnFalse()
        {
            // Arrange
            const int productId = 42;

            _server
                .Given(Request.Create().WithPath($"/api/images/products/{productId}").UsingHead())
                .RespondWith(Response.Create().WithStatusCode(404));

            // Act
            var response = await _sut.HasImageAsync<Product>(productId);

            // Assert
            response.ShouldBeFalse();
        }

        [Fact]
        public async Task ListIdsAsync_ShouldPostToCollectionUrlAndReturnIds()
        {
            // Arrange
            const int productId = 42;

            _server
                .Given(Request.Create().WithPath($"/api/images/products/{productId}").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(200).WithBody(
                    """
                    <?xml version="1.0" encoding="UTF-8"?>
                    <prestashop xmlns:xlink="http://www.w3.org/1999/xlink">
                        <image>
                            <declination id="12" xlink:href="https://shop.example.com/api/images/products/5/12"/>
                            <declination id="13" xlink:href="https://shop.example.com/api/images/products/5/13"/>
                        </image>
                    </prestashop>
                    """));

            // Act
            var response = await _sut.ListIdsAsync<Product>(productId);

            // Assert
            response.Count.ShouldBe(2);
            response.ShouldAllBe(x =>  new long[] { 12, 13 }.Contains(x));
        }

        [Fact]
        public async Task DeleteAsync_WhenApiReturns404_ShouldThrowNotFoundException()
        {
            // Arrange
            const int productId = 42;
            const int imageId = 2;

            _server
                .Given(Request.Create().WithPath($"/api/images/products/{productId}/{imageId}").UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(404));


            // Act & Assert
            await Should.ThrowAsync(() => _sut.DeleteAsync<Product>(productId, imageId), typeof(PrestaShopNotFoundException));
        }

        [Fact]
        public async Task DeleteAsync_WhenApiReturns401_ShouldThrowNotFoundException()
        {
            // Arrange
            const int productId = 42;
            const int imageId = 2;

            _server
                .Given(Request.Create().WithPath($"/api/images/products/{productId}/{imageId}").UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(404));


            // Act & Assert
            await Should.ThrowAsync(() => _sut.DeleteAsync<Product>(productId, imageId), typeof(PrestaShopNotFoundException));
        }

        [Fact]
        public async Task DeleteAsync_WhenApiReturns500_ShouldThrowApiException()
        {
            // Arrange
            const int productId = 42;
            const int imageId = 2;

            _server
                .Given(Request.Create().WithPath($"/api/images/products/{productId}/{imageId}").UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(500));


            // Act & Assert
            await Should.ThrowAsync(() => _sut.DeleteAsync<Product>(productId, imageId), typeof(PrestaShopApiException));
        }

        [Fact]
        public async Task DeleteAsync_WhenApiReturns401_ShouldThrowAuthException()
        {
            // Arrange
            const int productId = 42;
            const int imageId = 2;

            _server
                .Given(Request.Create().WithPath($"/api/images/products/{productId}/{imageId}").UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(401));

            // Act & Assert
            await Should.ThrowAsync(() => _sut.DeleteAsync<Product>(productId, imageId), typeof(PrestaShopAuthException));
        }

        [Fact]
        public async Task DeleteAsync_WhenApiReturnsErrors_ShouldThrowExceptionWithErrors()
        {
            // Arrange
            const int productId = 42;
            const int imageId = 2;

            _server.Given(Request.Create().WithPath($"/api/images/products/{productId}/{imageId}").UsingDelete())
               .RespondWith(Response.Create().WithStatusCode(400)
                   .WithBody("<prestashop><errors><error><code>89</code><message>Invalid</message></error></errors></prestashop>"));

            // Act
            var ex = await Should.ThrowAsync<PrestaShopApiException>(() => _sut.DeleteAsync<Product>(productId, imageId));

            // Assert
            ex.HttpStatusCode.ShouldBe(HttpStatusCode.BadRequest);
            ex.Errors.ShouldHaveSingleItem();
            ex.Errors[0].Code.ShouldBe(89);
            ex.Errors[0].Message.ShouldBe("Invalid");
            ex.ResponseBody!.ShouldContain("<errors>");
            ex.RequestUri!.ToString().ShouldContain($"/api/images/products/{productId}/{imageId}");
        }

        public void Dispose()
        {
            _http.Dispose();
            _server.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
