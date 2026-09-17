using System.Xml.Serialization;

namespace PrestaKit.Entities.Associations
{
    public sealed class EntityRef
    {
        [XmlElement("id")]
        public long Id { get; set; }

        public static implicit operator EntityRef(long id)
         => new() { Id = id };

        public static implicit operator long(EntityRef entityRef)
            => entityRef.Id;
    }
}
