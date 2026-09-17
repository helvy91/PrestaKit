using PrestaKit.Entities.Attributes;
using PrestaKit.Entities.Common;
using System.Reflection;
using System.Xml.Serialization;

namespace PrestaKit.Clients.Resources
{
    public class ResourceMeta<T> 
        where T : PrestaShopEntity
    {
        public static readonly string Name = typeof(T).GetCustomAttribute<ApiResourceAttribute>()?.Name 
            ?? throw new InvalidOperationException($"{typeof(T).Name} is missing [ApiResource].");

        public static readonly XmlSerializer Serializer = new(typeof(T));
    }
}
