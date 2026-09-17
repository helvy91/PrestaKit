using PrestaKit.Entities.Common;
using PrestaKit.Errors;

namespace PrestaKit.Serialization
{
    internal interface IPrestaShopSerializer
    {
        public string ContentType { get; }

        string SerializeEntity<T>(T entity) where T : PrestaShopEntity;
        T DeserializeSingle<T>(string xml) where T : PrestaShopEntity;
        List<T> DeserializeList<T>(string xml) where T : PrestaShopEntity;
        List<long> DeserializeIds(string xml);
        List<PrestashopError> DeserializeErrors(string xml);
    }
}
