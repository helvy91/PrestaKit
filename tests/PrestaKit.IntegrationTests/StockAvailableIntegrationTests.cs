using PrestaKit.Entities;
using PrestaKit.Entities.Associations;
using PrestaKit.Exceptions;
using PrestaKit.IntegrationTests.Fixtures;
using PrestaKit.Querying;
using Shouldly;

namespace PrestaKit.IntegrationTests
{
    public class StockAvailableIntegrationTests : IntegrationTestBase
    {
        public StockAvailableIntegrationTests(PrestaShopContainerFixture fixture) : base(fixture) { }

        [Fact]
        public async Task CanGetStockForProduct()
        {
            var product = await CreateProductAsync();
            var stockId = product.Associations!.StockAvailables[0].Id;

            var stock = await Client.Resource<StockAvailable>().GetAsync(stockId);

            stock.Id.ShouldBe(stockId);
            stock.IdProduct.ShouldBe(product.Id!.Value);
        }

        [Fact]
        public async Task CanUpdateStockQuantity()
        {
            var product = await CreateProductAsync();
            var stockId = product.Associations!.StockAvailables[0].Id;

            var stock = await Client.Resource<StockAvailable>().GetAsync(stockId);
            stock.Quantity = 50;
            await Client.Resource<StockAvailable>().UpdateAsync(stock);

            var updated = await Client.Resource<StockAvailable>().GetAsync(stockId);
            updated.Quantity.ShouldBe(50);
        }

        [Fact]
        public async Task CanListStock()
        {
            await CreateProductAsync();  

            var results = await Client.Resource<StockAvailable>().ListAsync(new Query<StockAvailable>());

            results.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task GetThrowsWhenStockDoesNotExist()
        {
            await Should.ThrowAsync<PrestaShopNotFoundException>(
                () => Client.Resource<StockAvailable>().GetAsync(999999));
        }

        private async Task<Product> CreateProductAsync()
        {
            var product = new Product
            {
                Name = "Stock Test Product",
                Price = 9.99m,
                State = 1,
                ProductType = "standard",
                IdCategoryDefault = 2,
                Active = true,
                Associations = new ProductAssociations { Categories = [2] }
            };
            return await Client.Products.AddAsync(product);
        }
    }
}