using PrestaKit.IntegrationTests.Fixtures;
using System.Net.Http.Headers;
using System.Text;

namespace PrestaKit.IntegrationTests
{
    [Collection("PrestaShop")]
    [Trait("Category", "Integration")]
    public abstract class IntegrationTestBase
    {
        protected PrestaShopClient Client { get; }

        protected IntegrationTestBase(PrestaShopContainerFixture fixture)
        {
            var http = new HttpClient { BaseAddress = new Uri(fixture.BaseUrl) };
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{PrestaShopContainerFixture.ApiKey}:")));
            Client = new PrestaShopClient(http);
        }
    }
}
