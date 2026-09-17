using PrestaKit.Clients.Media.Images;
using PrestaKit.Entities;
using PrestaKit.Exceptions;
using PrestaKit.IntegrationTests.Fixtures;
using Shouldly;

namespace PrestaKit.IntegrationTests
{
    public class ImageIntegrationTests : IntegrationTestBase
    {

        public ImageIntegrationTests(PrestaShopContainerFixture fixture) : base(fixture) { }
        

        [Fact]
        public async Task CanUploadImage()
        {
            var product = new Product
            {
                Price = 9.99m,
                Name = "Chair",
            };

            using var stream = new MemoryStream(ValidPngBytes);
            var imageUpload = new ImageUpload()
            { 
                Content = stream,
                FileName = "test.png"
            };

            var created = await Client.Products.AddAsync(product);
            var id = await Client.Images.UploadAsync<Product>(created.Id!.Value, imageUpload);

            id.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task CanDeleteImage()
        {
            var product = new Product
            {
                Price = 9.99m,
                Name = "Chair",
            };

            using var stream = new MemoryStream(ValidPngBytes);
            var imageUpload = new ImageUpload()
            {
                Content = stream,
                FileName = "test.png"
            };

            var created = await Client.Products.AddAsync(product);
            var id = await Client.Images.UploadAsync<Product>(created.Id!.Value, imageUpload);
            await Client.Images.DeleteAsync<Product>(created.Id.Value, id);

            var hasImage = await Client.Images.HasImageAsync<Product>(created.Id.Value);
            hasImage.ShouldBeFalse();
        }

        [Fact]
        public async Task CanDownloadImage()
        {
            var product = new Product
            {
                Price = 9.99m,
                Name = "Chair",
            };

            using var stream = new MemoryStream(ValidPngBytes);
            var imageUpload = new ImageUpload()
            {
                Content = stream,
                FileName = "test.png"
            };

            var created = await Client.Products.AddAsync(product);
            var id = await Client.Images.UploadAsync<Product>(created.Id!.Value, imageUpload);

            var bytes = await Client.Images.DownloadAsync<Product>(created.Id.Value, id);

            bytes.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task CanListImageIds()
        {
            var product = new Product
            {
                Price = 9.99m,
                Name = "Chair",
            };

            using var stream = new MemoryStream(ValidPngBytes);
            var imageUpload = new ImageUpload()
            {
                Content = stream,
                FileName = "test.png"
            };

            var created = await Client.Products.AddAsync(product);
            var id = await Client.Images.UploadAsync<Product>(created.Id!.Value, imageUpload);

            var ids = await Client.Images.ListIdsAsync<Product>(created.Id.Value);

            ids.ShouldContain(id);
        }

        [Fact]
        public async Task ReturnsTrueWhenProductHasImage()
        {
            var product = new Product
            {
                Price = 9.99m,
                Name = "Chair",
            };

            using var stream = new MemoryStream(ValidPngBytes);
            var imageUpload = new ImageUpload()
            {
                Content = stream,
                FileName = "test.png"
            };

            var created = await Client.Products.AddAsync(product);
            await Client.Images.UploadAsync<Product>(created.Id!.Value, imageUpload);

            var hasImage = await Client.Images.HasImageAsync<Product>(created.Id.Value);

            hasImage.ShouldBeTrue();
        }

        [Fact]
        public async Task ReturnsFalseWhenProductHasNoImage()
        {
            var product = new Product
            {
                Price = 9.99m,
                Name = "Chair",
            };

            var created = await Client.Products.AddAsync(product);

            var hasImage = await Client.Images.HasImageAsync<Product>(created.Id!.Value);

            hasImage.ShouldBeFalse();
        }

        [Fact]
        public async Task ThrowsWhenImageDoesNotExist()
        {
            var product = new Product
            {
                Price = 9.99m,
                Name = "Chair",
            };

            var created = await Client.Products.AddAsync(product);

            var ex = await Should.ThrowAsync<PrestaShopApiException>(
                () => Client.Images.DownloadAsync<Product>(created.Id!.Value, 999999));
        }

        private static byte[] ValidPngBytes =>
        [
            0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, 0x00, 0x00, 0x00, 0x0D, 0x49, 0x48, 0x44, 0x52,
            0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x08, 0x06, 0x00, 0x00, 0x00, 0x1F, 0x15, 0xC4,
            0x89, 0x00, 0x00, 0x00, 0x0D, 0x49, 0x44, 0x41, 0x54, 0x78, 0xDA, 0x63, 0x64, 0xF8, 0xCF, 0x50,
            0x0F, 0x00, 0x03, 0x86, 0x01, 0x80, 0x5A, 0x34, 0x7D, 0x6B, 0x00, 0x00, 0x00, 0x00, 0x49, 0x45,
            0x4E, 0x44, 0xAE, 0x42, 0x60, 0x82
        ];
    }
}
