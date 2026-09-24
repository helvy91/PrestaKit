using PrestaKit.Clients.Resources;
using PrestaKit.Entities;
using PrestaKit.Exceptions;
using PrestaKit.Querying;
using PrestaKit.Serialization;
using Shouldly;
using System.Globalization;
using System.Net;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace PrestaKit.Tests.Clients
{
    [Trait("Category", "Unit")]
    public class ResourceClientTests : IDisposable
    {
        private readonly ResourceClient<Product> _sut;
        private readonly WireMockServer _server;
        private readonly HttpClient _http;

        public ResourceClientTests()
        {
            _server = WireMockServer.Start();
            _http = new HttpClient() { BaseAddress = new Uri(_server.Url + "/api/") };
            _sut = new ResourceClient<Product>(_http, new PrestaShopXmlSerializer());
        }

        [Fact]
        public async Task AddAsync_ShouldReturnCreatedProductWithId()
        {
            // Arrange
            var product = new Product() { Price = 19.5M, Active = true };

            _server.Given(Request.Create().WithPath("/api/products").UsingPost())
                .RespondWith(Response.Create().WithStatusCode(201)
                    .WithBody("<prestashop><product><id>42</id></product></prestashop>"));

            // Act
            var result = await _sut.AddAsync(product);

            // Assert
            result.Id.ShouldBe(42);
        }

        [Fact]
        public async Task AddAsync_ShouldPostToCollectionUrlWithBody()
        {
            var product = new Product() { Price = 19.5M, Active = true };
            _server.Given(Request.Create().WithPath("/api/products").UsingPost())
                .RespondWith(Response.Create().WithStatusCode(201).WithBody(
                    "<prestashop><product><id>42</id></product></prestashop>"));

            await _sut.AddAsync(product);

            var request = _server.LogEntries.Single().RequestMessage!;
            request.Method.ShouldBe("POST");           
            request.Path.ShouldBe("/api/products");   
            request.Body.ShouldNotBeNullOrEmpty();     
        }

        [Fact]
        public async Task GetAsync_ShouldSendRequestToCollectionUrlWithId()
        {
            // Arrange
            const int productId = 42;

            _server.Given(Request.Create().WithPath($"/api/products/{productId}").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(200).WithBody(
                    "<prestashop><product><id>42</id></product></prestashop>"));

            // Act
            var product = await _sut.GetAsync(productId);

            // Assert
            product.Id!.Value.ShouldBe(productId);
        }
        
        [Fact]
        public async Task DeleteAsync_ShouldSendRequestToCollectionUrlWithId()
        {
            // Arrange
            const int productId = 42;

            _server.Given(Request.Create().WithPath($"/api/products/{productId}").UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(200));

            // Act & Assert
            await _sut.DeleteAsync(productId);
        }

        [Fact]
        public async Task DeleteAsync_ShouldSendRequestToCollectionUrlWithIdFromEntity()
        {
            // Arrange
            const int productId = 42;
            var product = new Product() { Id = productId };

            _server.Given(Request.Create().WithPath($"/api/products/{productId}").UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(200));

            // Act & Assert
            await _sut.DeleteAsync(product);
        }

        [Fact]
        public async Task ExistsAsync_WhenProductExists_ShouldSendRequestToCollectionUrlWithIdAdReturnTrue()
        {
            // Arrange
            const int productId = 42;

            _server.Given(Request.Create().WithPath($"/api/products/{productId}").UsingHead())
                .RespondWith(Response.Create().WithStatusCode(200));

            // Act
            var exists = await _sut.ExistsAsync(productId);

            // Assert
            exists.ShouldBeTrue();
        }

        [Fact]
        public async Task ExistsAsync_WhenProductDoesNotExist_ShouldSendRequestToCollectionUrlWithIdAndReturnFalse()
        {
            // Arrange
            const int productId = 42;

            _server.Given(Request.Create().WithPath($"/api/products/{productId}").UsingHead())
                .RespondWith(Response.Create().WithStatusCode(404));

            // Act
            var exists = await _sut.ExistsAsync(productId);

            // Assert
            exists.ShouldBeFalse();
        }

        [Fact]
        public async Task ExistsAsync_WhenApiReturnsUnhandledCode_ShouldThrowApiException()
        {
            // Arrange
            const int productId = 42;

            _server.Given(Request.Create().WithPath($"/api/products/{productId}").UsingHead())
                .RespondWith(Response.Create().WithStatusCode(500));

            // Act & Assert
            await Should.ThrowAsync(
                () => _sut.ExistsAsync(productId), typeof(PrestaShopApiException));
        }

        [Fact]
        public async Task FindAsync_WhenProductExists_ShouldReturnProduct()
        {
            // Arrange
            const int productId = 42;

            _server.Given(Request.Create().WithPath($"/api/products/{productId}").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(200).WithBody(
                    "<prestashop><product><id>42</id></product></prestashop>"));

            // Act
            var product = await _sut.FindAsync(productId);

            // Assert
            product!.Id!.Value.ShouldBe(productId);
        }

        [Fact]
        public async Task FindAsync_WhenProductDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            const int productId = 42;

            _server.Given(Request.Create().WithPath($"/api/products/{productId}").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(404).WithBody(
                    "<prestashop><product><id>42</id></product></prestashop>"));

            // Act
            var product = await _sut.FindAsync(productId);

            // Assert
            product.ShouldBeNull();
        }

        [Fact]
        public async Task ListAsync_ShouldSendRequestToCollectionUrlAndReturnProducts()
        {
            // Arrange
            _server.Given(Request.Create().WithPath($"/api/products").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(200).WithBody(
                    """
                    <prestashop>
                      <products>
                        <product>
                          <id>1</id>
                          <price>10.000000</price>
                        </product>
                        <product>
                          <id>2</id>
                          <price>25.500000</price>
                        </product>
                      </products>
                    </prestashop>
                    """));

            // Act
            var products = await _sut.ListAsync();

            // Assert
            products.Count.ShouldBe(2);
            products[0].Id.ShouldBe(1);
            products[0].Price.ShouldBe(10M);
            products[1].Id.ShouldBe(2);
            products[1].Price.ShouldBe(25.5M);
        }

        [Fact]
        public async Task ListAsync_ShouldSendRequestToCollectionUrlWithQueryAndReturnOneProduct()
        {
            // Arrange
            var request = Request.Create()
                .WithPath($"/api/products")
                .WithParam("filter[id]", "1")
                .WithParam("sort", "[id_ASC]")
                .WithParam("display", "full")
                .UsingGet();

            _server.Given(request)
                .RespondWith(Response.Create().WithStatusCode(200).WithBody(
                    """
                    <prestashop>
                      <products>
                        <product>
                          <id>1</id>
                          <price>10.000000</price>
                        </product>
                      </products>
                    </prestashop>
                    """));

            var query = new Query<Product>();
            query.OrderBy(x => x.Id);
            query.WhereEquals(x => x.Id, "1");

            // Act
            var products = await _sut.ListAsync(query);

            // Assert
            products.Count.ShouldBe(1);
            products[0].Id.ShouldBe(1);
            products[0].Price.ShouldBe(10M);
        }

        [Fact]
        public async Task EnumerateAsync_ShouldSendRequestToCollectionUrlWithQueryAndIterateThroughPages()
        {
            // Arrange
            StubPage(0, 1, 1, 10.5M);
            StubPage(1, 1, 2, 15.5M);
            StubPage(2, 1, 3, 99.9M);
            StubEmptyPage(3, 1);

            // Act & Assert

            var products = new List<Product>();
            await foreach (var product in _sut.EnumerateAsync(new Query<Product>(), 1))
            {
                products.Add(product);
            }

            // Assert
            products.Count.ShouldBe(3);

            products[0].Id.ShouldBe(1);
            products[0].Price.ShouldBe(10.5M);
            products[1].Id.ShouldBe(2);
            products[1].Price.ShouldBe(15.5M);
            products[2].Id.ShouldBe(3);
            products[2].Price.ShouldBe(99.9M);
        }

        [Fact]
        public async Task GetAsync_WhenApiReturns404_ShouldThrowNotFoundException()
        {
            // Arrange
            const int productId = 42;

            _server.Given(Request.Create().WithPath($"/api/products/{productId}").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(404));

            // Act & Assert
            await Should.ThrowAsync(() => _sut.GetAsync(productId), typeof(PrestaShopNotFoundException));
        }

        [Fact]
        public async Task GetAsync_WhenApiReturns500_ShouldThrowApiException()
        {
            // Arrange
            const int productId = 42;

            _server.Given(Request.Create().WithPath($"/api/products/{productId}").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(500));

            // Act & Assert
            await Should.ThrowAsync(() => _sut.GetAsync(productId), typeof(PrestaShopApiException));
        }

        [Fact]
        public async Task GetAsync_WhenApiReturns401_ShouldThrowAuthException()
        {
            // Arrange
            const int productId = 42;

            _server.Given(Request.Create().WithPath($"/api/products/{productId}").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(401));

            // Act & Assert
            await Should.ThrowAsync(() => _sut.GetAsync(productId), typeof(PrestaShopAuthException));
        }

        [Fact]
        public async Task GetAsync_WhenResponseBodyIsMalformed_ShouldThrowSerializationException()
        {
            // Arrange
            const int productId = 42;

            _server.Given(Request.Create().WithPath($"/api/products/{productId}").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(200).WithBody("</]prestashop</>"));

            // Act & Assert
            await Should.ThrowAsync(() => _sut.GetAsync(productId), typeof(PrestaShopSerializationException));
        }

        [Fact]
        public async Task GetAsync_WhenApiReturnsErrorResponse_ShouldThrowApiExceptionWithStatusErrorsAndBody()
        {
            // Arrange
            const int productId = 5;

            _server.Given(Request.Create().WithPath($"/api/products/{productId}").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(400)
                    .WithBody("<prestashop><errors><error><code>89</code><message>Invalid</message></error></errors></prestashop>"));

            // Act
            var ex = await Should.ThrowAsync<PrestaShopApiException>(() => _sut.GetAsync(productId));

            // Assert
            ex.HttpStatusCode.ShouldBe(HttpStatusCode.BadRequest);
            ex.Errors.ShouldHaveSingleItem();
            ex.Errors[0].Code.ShouldBe(89);
            ex.Errors[0].Message.ShouldBe("Invalid");
            ex.ResponseBody!.ShouldContain("<errors>");
            ex.RequestUri!.ToString().ShouldContain($"/api/products/{productId}");
        }

        private void StubEmptyPage(int offset, int pageSize)
        {
            var request = Request.Create()
                .WithUrl($"*/api/products?*limit={offset},{pageSize}*")
                .UsingGet();

            _server.Given(request)
                .RespondWith(Response.Create().WithStatusCode(200).WithBody(
                    $"""
                    <prestashop>
                      <products>
                      </products>
                    </prestashop>
                    """));
        }

        private void StubPage(int offset, int pageSize, int id, decimal price)
        {
            var request = Request.Create()
                .WithUrl($"*/api/products?*limit={offset},{pageSize}*")
                .UsingGet();

            _server.Given(request)
                .RespondWith(Response.Create().WithStatusCode(200).WithBody(
                    $"""
                    <prestashop>
                      <products>
                        <product>
                          <id>{id.ToString(CultureInfo.InvariantCulture)}</id>
                          <price>{price.ToString(CultureInfo.InvariantCulture)}</price>
                        </product>
                      </products>
                    </prestashop>
                    """));
        }

        public void Dispose()
        {
            _server.Dispose();
            _http.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
