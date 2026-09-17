using PrestaKit.Entities.Common;

namespace PrestaKit.Clients.Media.Images
{
    public interface IImageClient
    {
        Task<IReadOnlyList<long>> ListIdsAsync<T>(long resourceId, CancellationToken ct = default)
            where T : PrestaShopEntity, IHasImages;

        Task<byte[]> DownloadAsync<T>(long resourceId, long imageId, CancellationToken ct = default)
            where T : PrestaShopEntity, IHasImages;

        Task<long> UploadAsync<T>(long resourceId, ImageUpload image, CancellationToken ct = default)
            where T : PrestaShopEntity, IHasImages;

        Task DeleteAsync<T>(long resourceId, long imageId, CancellationToken ct = default)
            where T : PrestaShopEntity, IHasImages;

        Task<bool> HasImageAsync<T>(long resourceId, CancellationToken ct = default)
            where T : PrestaShopEntity, IHasImages;
    }
}
