using PrestaKit.Entities;
using PrestaKit.Entities.Associations;
using PrestaKit.Exceptions;
using PrestaKit.IntegrationTests.Fixtures;
using PrestaKit.Querying;
using Shouldly;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace PrestaKit.IntegrationTests
{
    [Collection("PrestaShop")]
    public class ProductIntegrationTests : IntegrationTestBase
    {
        private readonly string _testRunId = Guid.NewGuid().ToString("N")[..8];

        private readonly PrestaShopContainerFixture _fixture;

        public ProductIntegrationTests(PrestaShopContainerFixture fixture) : base(fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task CanAddProduct()
        {
            var product = new Product
            {
                Price = 9.99m,
                Name = "Chair",
                Description = "Wooden chair for kitchen or dining room",
                Associations = new ProductAssociations() { Categories = [1, 10] },
            };

            var created = await Client.Products.AddAsync(product);

            var fetched = await Client.Products.GetAsync(created.Id!.Value);

            fetched.Price!.Value.ShouldBe(product.Price.Value);
            fetched.Name!.Values[0].Value.ShouldBe(product.Name.Values[0].Value);
            fetched.Description!.Values[0].Value.ShouldBe(product.Description);
            fetched.Associations!.Categories.ShouldAllBe(x => product.Associations.Categories.Select(x => x.Id).Contains(x.Id));
        }

        [Fact]
        public async Task CanUpdateProduct()
        {
            var created = await Client.Products.AddAsync(new Product
            {
                Price = 19.99m,
                Name = "Bicycle tire"
            });

            var fetched = await Client.Products.GetAsync(created.Id!.Value);
            fetched.Price = 29.99m;
            fetched.Name = "Premium bicycle tire";

            await Client.Products.UpdateAsync(fetched);
            
            fetched = await Client.Products.GetAsync(created.Id!.Value);

            fetched.Name!.Values[0].Value.ShouldBe("Premium bicycle tire");
            fetched.Price.ShouldBe(29.99m);
        }

        [Fact]
        public async Task CanDeleteProductById()
        {
            var created = await Client.Products.AddAsync(new Product
            {
                Price = 19.99m,
                Name = "Bicycle tire"
            });

            await Client.Products.DeleteAsync(created.Id!.Value);
            await Client.Products.ExistsAsync(created.Id.Value);
        }

        [Fact]
        public async Task CanDeleteProductByObject()
        {
            var created = await Client.Products.AddAsync(new Product
            {
                Price = 19.99m,
                Name = "Bicycle tire"
            });

            await Client.Products.DeleteAsync(created);
            await Client.Products.ExistsAsync(created.Id!.Value);
        }

        [Fact]
        public async Task CanListProductsWithQuery()
        {
            for (int i = 0; i < 10; i++)
            {
                await Client.Products.AddAsync(new Product
                {
                    Price = 19.99m,
                    Name = $"{_testRunId}_{i}",
                });
            }

            var query = new Query<Product>();
            query.WhereBeginsWith(x => x.Name, _testRunId);

            var result = await Client.Products.ListAsync(query);

            result.Count.ShouldBe(10);

            for (int i = 0; i < result.Count; i++)
            {
                result[i].Name!.Values[0].Value.ShouldBe($"{_testRunId}_{i}");
                result[i].Price.ShouldBe(19.99m);
            }
        }

        [Fact]
        public async Task CanListIdsWithQuery()
        {
            var ids = new List<long>();
            for (int i = 0; i < 10; i++)
            {
                var created = await Client.Products.AddAsync(new Product
                {
                    Price = 19.99m,
                    Name = $"{_testRunId}_{i}",
                });

                ids.Add(created.Id!.Value);
            }

            var query = new Query<Product>();
            query.WhereBeginsWith(x => x.Name, _testRunId);

            var result = await Client.Products.ListIdsAsync(query);

            result.Count.ShouldBe(10);
            ids.ShouldAllBe(x => ids.Contains(x));
        }

        [Fact]
        public async Task CanEnumerateProductsWithQuery()
        {
            for (int i = 0; i < 10; i++)
            {
                await Client.Products.AddAsync(new Product
                {
                    Price = 19.99m,
                    Name = $"{_testRunId}_{i}",
                });
            }

            var query = new Query<Product>();
            query.WhereBeginsWith(x => x.Name, _testRunId);

            var counter = 0;
            var products = new List<Product>();
            await foreach (var product in Client.Products.EnumerateAsync(query))
            {
                if (counter++ == 5)
                {
                    break;
                }

                products.Add(product);
            }
            
            products.Count.ShouldBe(5);

            for (int i = 0; i < products.Count; i++)
            {
                products[i].Name!.Values[0].Value.ShouldBe($"{_testRunId}_{i}");
                products[i].Price.ShouldBe(19.99m);
            }
        }

        [Fact]
        public async Task CanFindProductById()
        {
            var product = new Product
            {
                Price = 19.99m,
                Name = "Bicycle tire"
            };
            var created = await Client.Products.AddAsync(product);

            var found = await Client.Products.FindAsync(created.Id!.Value);

            found!.Price.ShouldBe(product.Price);
            found.Name!.Values[0].Value.ShouldBe(product.Name.Values[0].Value);
        }

        [Fact]
        public async Task CanGetProductById()
        {
            var product = new Product
            {
                Price = 19.99m,
                Name = "Bicycle tire"
            };
            var created = await Client.Products.AddAsync(product);

            var fetched = await Client.Products.GetAsync(created.Id!.Value);

            fetched!.Price.ShouldBe(product.Price);
            fetched.Name!.Values[0].Value.ShouldBe(product.Name.Values[0].Value);
        }

        [Fact]
        public async Task ThrowsWhenProductDoesntHaveRequiredFields()
        {
            var product = new Product();
       
            var ex = await Should.ThrowAsync<PrestaShopApiException>(
                () => Client.Products.AddAsync(product));

            ex.HttpStatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ThrowsWhenFetchingProductThatDoesntExist()
        {
            var ex = await Should.ThrowAsync<PrestaShopNotFoundException>(
                () => Client.Products.GetAsync(99999999));

            ex.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ThrowsWhenNotAuthorized()
        {
            var badHttp = new HttpClient { BaseAddress = new Uri(_fixture.BaseUrl) };
            badHttp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("WRONGKEY:")));
            var badClient = new PrestaShopClient(badHttp);

            await Should.ThrowAsync<PrestaShopAuthException>(
                () => badClient.Products.GetAsync(1));

            badHttp.Dispose();
        }
    }
}
